using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCVisualDesigner.Utility
{
    public abstract class StateMachine
    {
        private State m_curState;
        private State m_initState;
        private StateContext m_context;
        private Logger m_logger;
        public StateMachine()
        {
            m_context = new StateContext();
            m_logger = new Logger(m_context);

            // it's ok to call virtual method in ctor for C#
            initStates();
        }

        abstract protected void initStates();

        public List<ParserMessage> Messages { get { return Logger.MessageList; } }
        public bool HasError { get { return Logger.HasError; } }
        public bool HasMessage { get { return Logger.HasMessage; } }

        public Logger Logger { get { return m_logger; } }

        protected State createState(string name, bool initState = false)
        {
            State state = new State(name, m_context);
            if (initState) m_initState = state;
            return state;
        }

        protected string getStateToken(bool trim = true)
        {
            string s = m_context.StateToken.ToString();
            if (trim) s = s.Trim();
            return s;
        }

        private void resetState()
        {
            m_curState = m_initState;
            m_context.Reset();
        }

        private bool onChar(char ch)
        {
            bool bTransit = false;

            for (int i = 0; i < m_curState.Transitions.Count; i++)
            {
                Transition t = m_curState.Transitions[i];
                if (t.CanTransitOnChar(this.m_context, ch))
                {
                    if (m_curState == t.TargetState)
                    {
                        m_context.StateToken.Append(ch);

                        // run actions of transition
                        if (t.TransitionAction != null) t.TransitionAction(m_context, ch);
                    }
                    else
                    {
                        // transit state
                        m_curState = t.TargetState;

                        // run actions of transition
                        if (t.TransitionAction != null) t.TransitionAction(m_context, ch);

                        if (t.ReturnCharAfterTransition) m_context.ReturnChar();

                        // clear token after state transition
                        m_context.StateToken.Clear();
                    }

                    bTransit = true;
                    break;
                }
            }
            return bTransit;
        }

        virtual public void Run(string stringToParse)
        {
            // clear logger messages
            m_logger.MessageList.Clear();
            m_context.ParsingIndex = 0;

            this.resetState();

            bool isPrevCharSpaceSeparator = true;

            while (m_context.ParsingIndex < stringToParse.Length)
            {
                char ch = stringToParse[m_context.ParsingIndex];
                if (ch == '\\') // start of escape sequence
                {
                    m_context.ParsingIndex++;
                    m_context.LineCol++;
                    if (m_context.ParsingIndex < stringToParse.Length)
                    {
                        char nextChar = stringToParse[m_context.ParsingIndex];
                        if (nextChar == '"' || nextChar == '\\')
                        {
                            ch = nextChar;
                            //go on calling onChar() below
                        }
                        //else if (nextChar == '\r' || nextChar == '\n') // continue line
                        //{
                        //}
                        else // unknown escape char sequence, ignore it
                        {
                            m_context.ParsingIndex++;
                            m_context.LineCol++;
                            m_logger.Log(MessageSeverity.Error, string.Format("Unknown escape char sequence \\{0}", nextChar));
                            continue;
                        }
                    }
                    else // bad escape char sequence at EOF, ignore it
                    {
                        m_logger.Log(MessageSeverity.Error, "Bad escape char sequence");
                        continue;
                    }
                    isPrevCharSpaceSeparator = false;
                }
                else if (ch == '"')
                {
                    this.m_context.WithinQuote = !this.m_context.WithinQuote;
                    m_context.ParsingIndex++;
                    m_context.LineCol++;
                    continue;
                }
                else if (ch == '\r' || ch == '\n')
                {
                    m_context.IsEOL = true;

                    if (m_context.WithinQuote)
                    {
                        // error, quote is not closed
                        m_logger.Log(MessageSeverity.Error, "Quote is not closed.");
                    }
                    //
                    //todo: .. other error checking here
                    //
                    else if (!onChar('\n')) //EOL: end of line
                    {
                        //error, line is not complete
                        m_logger.Log(MessageSeverity.Error, "Command line is invalid or not complete.");
                    }

                    m_context.ParsingIndex += newLineCharCount(stringToParse);
                    m_context.LineNum++;
                    resetState();
                    isPrevCharSpaceSeparator = true;
                    continue;
                }
                else if ((ch == '\t' || ch == ' ') && !this.m_context.WithinQuote) // ignore space separator chars
                {
                    isPrevCharSpaceSeparator = true;
                    m_context.ParsingIndex++;
                    m_context.LineCol++; //todo: increase 4 for \t ?
                    continue;
                }

                m_context.IsFNSSChar = isPrevCharSpaceSeparator;

                if (onChar(ch))
                {
                    m_context.ParsingIndex++;
                    m_context.LineCol++;
                    isPrevCharSpaceSeparator = false;
                }
                else
                {
                    // failed to transit, log error and ignore this line
                    m_logger.Log(MessageSeverity.Error, "Command line is invalid or not complete.");
                    while (m_context.ParsingIndex < stringToParse.Length
                        && (stringToParse[m_context.ParsingIndex] != '\r' && stringToParse[m_context.ParsingIndex] != '\n'))
                    {
                        m_context.ParsingIndex++;
                    }

                    m_context.ParsingIndex += newLineCharCount(stringToParse);

                    m_context.LineNum++;
                    resetState();
                    isPrevCharSpaceSeparator = true;
                }
            }

            if (m_context.WithinQuote)
            {
                // error, quote is not closed
                m_logger.Log(MessageSeverity.Error, "Quote is not closed.");
            }

            // append a EOL
            m_context.IsEOL = true;
            onChar('\n');
        }

        private int newLineCharCount(string str)
        {
            if (m_context.ParsingIndex + 1 < str.Length)
            {
                if (str[m_context.ParsingIndex] == '\r' || str[m_context.ParsingIndex + 1] == '\n')
                    return 2;
                //else if (str[m_context.ParsingIndex] == '\n' || str[m_context.ParsingIndex + 1] == '\r') 
                //    return 2;
            }
            return 1;
        }
    }

    public class StateContext
    {
        public StateContext()
        {
            ParsingIndex = 0;
            LineNum = 1;
            m_token = new StringBuilder();
            Reset();
        }

        public void Reset()
        {
            LineCol = 1;
            WithinQuote = false;
            IsFNSSChar = true;
            IsEOL = false;
            m_token.Clear();
        }

        private StringBuilder m_token;
        public StringBuilder StateToken { get { return m_token; } }

        public bool WithinQuote { get; set; }
        public bool IsEOL { get; set; }
        //public char OpenQuoteChar { get; set; }
        //public int OpenQuoteLine { get; set; }
        //public int OpenQuoteColumn { get; set; }
        public bool IsFNSSChar { get; set; } // first non-space separator char

        public int LineNum { get; set; }
        public int LineCol { get; set; }
        public int ParsingIndex { get; set; }

        public void ReturnChar() // move index back to return the char to buffer
        {
            if (ParsingIndex >= 0)
            {
                ParsingIndex--;
                LineCol--;
            }
            IsFNSSChar = false;
        }

    }

    // State
    public class State
    {
        private string m_name;
        private StateContext m_context;
        public State(string name, StateContext context)
        {
            m_name = name;
            m_context = context;
        }

        public string Name { get { return m_name; } }

        public List<Transition> Transitions = new List<Transition>();

        public StateTransitionHelper To(State target)
        {
            return new StateTransitionHelper(this, target, m_context);
        }

        public class StateTransitionHelper
        {
            private State m_source;
            private State m_target;
            private StateContext m_context;

            public StateTransitionHelper(State source, State target, StateContext context)
            {
                m_source = source;
                m_target = target;
                m_context = context;
            }

            public Transition OnChar(char c, bool returnCharAfterTransition = false)
            {
                var t = new Transition(m_source, m_target, ch => ch == c, returnCharAfterTransition);
                m_source.Transitions.Add(t);
                return t;
            }

            public Transition OnChars(params char[] chs)
            {
                var t = new Transition(m_source, m_target, ch =>
                {
                    foreach (var c in chs) { if (ch == c) return true; }
                    return false;
                });
                m_source.Transitions.Add(t);
                return t;
            }

            public Transition OnFNSSChar(char c, bool returnCharAfterTransition = false)
            {
                var t = new Transition(m_source, m_target, ch => ch == c && m_context.IsFNSSChar, returnCharAfterTransition);
                m_source.Transitions.Add(t);
                return t;
            }

            public Transition OnAnyChar(bool returnCharAfterTransition = false)
            {
                var t = new Transition(m_source, m_target, ch => !m_context.IsEOL, returnCharAfterTransition);
                m_source.Transitions.Add(t);
                return t;
            }

            public Transition OnAnyFNSSChar(bool returnCharAfterTransition = false)
            {
                var t = new Transition(m_source, m_target, ch => m_context.IsFNSSChar && !m_context.IsEOL, returnCharAfterTransition);
                m_source.Transitions.Add(t);
                return t;
            }

            public Transition On(Transition.Condition condition, bool returnCharAfterTransition = false)
            {
                var t = new Transition(m_source, m_target, condition, returnCharAfterTransition);
                m_source.Transitions.Add(t);
                return t;
            }

            public Transition OnSpaceChar(bool returnCharAfterTransition = false)
            {
                var t = new Transition(m_source, m_target, ch => ch == ' ' || ch == '\t', returnCharAfterTransition);
                m_source.Transitions.Add(t);
                return t;
            }

            public Transition OnEOL()
            {
                var t = new Transition(m_source, m_target, ch => ch == '\n' && m_context.IsEOL);
                m_source.Transitions.Add(t);
                return t;
            }
        }
    }

    // Transition
    public class Transition
    {
        public delegate void Action(StateContext context, char curChar);
        public delegate bool Condition(char ch);

        private State m_sourceState;
        private State m_targetState;
        private Condition m_condition;
        private bool m_returnCharAfterTransition;

        public Transition(State srcState, State targetState, Condition condition,
            bool returnCharAfterTransition = false)
        {
            m_sourceState = srcState;
            m_targetState = targetState;
            m_returnCharAfterTransition = returnCharAfterTransition;
            m_condition = condition;
        }

        public State SourceState { get { return m_sourceState; } }
        public State TargetState { get { return m_targetState; } }
        public bool ReturnCharAfterTransition { get { return m_returnCharAfterTransition; } }
        public Action TransitionAction { get; set; }

        public Transition Do(Action act)
        {
            TransitionAction = act;
            return this;
        }

        // to ease debug
        private string m_desc;
        public string Description { get { return m_desc; } }
        public Transition Desc(string desc)
        {
            m_desc = desc;
            return this;
        }

        public virtual bool CanTransitOnChar(StateContext context, char c)
        {
            return m_condition(c);
        }
    }

    // Message
    public enum MessageSeverity
    {
        Info,
        Warning,
        Error
    }

    public class ParserMessage
    {
        public MessageSeverity Severity { get; set; }
        public int LineNum { get; set; }
        public int LineCol { get; set; }
        public string Message { get; set; }
    }

    public class Logger
    {
        private StateContext m_context;
        public Logger(StateContext context)
        {
            m_context = context;
        }

        private List<ParserMessage> m_msgs = new List<ParserMessage>();

        public List<ParserMessage> MessageList { get { return m_msgs; } }

        public void Log(MessageSeverity severity, string msg)
        {
            m_msgs.Add(new ParserMessage() { Severity = severity, Message = msg, LineNum = m_context.LineNum, LineCol = m_context.LineCol });
        }

        public bool HasError { get { return m_msgs.Find(m => m.Severity == MessageSeverity.Error) != null; } }

        public bool HasMessage { get { return m_msgs.Count > 0; } }

        public string MessageString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var m in m_msgs)
                {
                    sb.AppendFormat("{0}: {1} at line {2}. \r\n", m.Severity.ToString(), m.Message, m.LineNum);
                }
                return sb.ToString();
            }
        }
    }
}

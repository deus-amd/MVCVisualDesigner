using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    /// <summary>
    /// whether or not 2 elements can be merged
    /// merging is trigered when working on graphic designer
    /// !! the relationship initialized in code are not controlled by CanMerge() implementation
    /// </summary>
    class MergeManager
    {
        public MergeManager()
        {
            // view
            target(VDView.DomainClassId).AcceptAll();

            // form
            target(VDForm.DomainClassId).SameAs(VDView.DomainClassId);	
            target(VDRadio.DomainClassId).AcceptNone();
            target(VDCheckBox.DomainClassId).AcceptNone();
            target(VDSubmit.DomainClassId).AcceptNone();
            target(VDInput.DomainClassId).AcceptNone();
            target(VDSelect.DomainClassId).Accept(VDSelectOption.DomainClassId, VDCodeSnippet.DomainClassId);
            target(VDSelectOption.DomainClassId).Accept(VDCodeSnippet.DomainClassId, VDHTMLTag.DomainClassId, VDText.DomainClassId);

            // section
            target(VDSection.DomainClassId).AcceptNone();
            target(VDSectionHead.DomainClassId).SameAs(VDView.DomainClassId);
            target(VDSectionBody.DomainClassId).SameAs(VDView.DomainClassId);

            // html tag
            target(VDHTMLTag.DomainClassId).SameAs(VDView.DomainClassId);

            // code snippet
            target(VDCodeSnippet.DomainClassId).AcceptNone();
            target(VDCodeSnippetBody.DomainClassId).SameAs(VDView.DomainClassId);

            // tab
            target(VDTab.DomainClassId).AcceptNone();
            target(VDTabHead.DomainClassId).SameAs(VDView.DomainClassId);
            target(VDTabBody.DomainClassId).SameAs(VDView.DomainClassId);

            // table
            target(VDTable.DomainClassId).Accept(VDTableRow.DomainClassId, VDCodeSnippet.DomainClassId, VDHTMLTag.DomainClassId);
            target(VDTableRowWrapper.DomainClassId).Accept(VDTableRow.DomainClassId);
            target(VDTableRow.DomainClassId).AcceptNone();
            target(VDTableCell.DomainClassId).SameAs(VDView.DomainClassId);
            target(VDTableColTitle.DomainClassId).AcceptNone();	
            target(VDTableRowTitle.DomainClassId).AcceptNone();	

            // VDText
            target(VDText.DomainClassId).AcceptNone();

            // CanMerge() implementation of container classes are forwarded to their Parent object
            target(VDHoriContainer.DomainClassId).AcceptNone();
            target(VDVertContainer.DomainClassId).AcceptNone();
            target(VDFullFilledContainer.DomainClassId).AcceptNone();

            // Separators
            target(VDHoriSeparator.DomainClassId).AcceptNone();
            target(VDVertSeparator.DomainClassId).AcceptNone();
        }

        private static MergeManager s_instance = null;
        public static MergeManager Instance
        {
            get
            {
                if (s_instance == null) s_instance = new MergeManager();

                return s_instance;
            }
        }

        public bool CanMerge(Guid sourceElementID, Guid targetElementID)
        {
            HashSet<Guid> acceptedTypesByTarget = null;
            if (_acceptedDomainTypes.TryGetValue(targetElementID, out acceptedTypesByTarget) && acceptedTypesByTarget != null)
            {
                if (acceptedTypesByTarget.Contains(sourceElementID)) return true;
            }

            return false;
        }

        private MergeTarget target(Guid targetElemetID) 
        { 
            return new MergeTarget(_acceptedDomainTypes, targetElemetID); 
        }

        class MergeTarget
        {
            private Guid m_targetElementID;
            private Dictionary<Guid, HashSet<Guid>> m_acceptedDomainTypes = new Dictionary<Guid, HashSet<Guid>>();

            public MergeTarget(Dictionary<Guid, HashSet<Guid>> acceptedDomainTypes, Guid targetElementID)
            {
                m_targetElementID = targetElementID;
                m_acceptedDomainTypes = acceptedDomainTypes;
            }

            internal void Accept(Guid sourceElementID1, params Guid[] sourceElementIDs)
            {
                List<Guid> list = new List<Guid>();
                list.Add(sourceElementID1);
                list.AddRange(sourceElementIDs);
                this.Accept(list);
            }

            internal void Accept(IEnumerable<Guid> sourceElementIDs)
            {
                if (!m_acceptedDomainTypes.ContainsKey(m_targetElementID))
                    m_acceptedDomainTypes.Add(m_targetElementID, new HashSet<Guid>());

                foreach (Guid g in sourceElementIDs)
                {
                    if (!m_acceptedDomainTypes[m_targetElementID].Contains(g))
                        m_acceptedDomainTypes[m_targetElementID].Add(g);
                }
            }

            internal void NotAccept(Guid sourceElementID1, params Guid[] sourceElementIDs)
            {
                List<Guid> list = new List<Guid>();
                list.Add(sourceElementID1);
                list.AddRange(sourceElementIDs);
                this.NotAccept(list);
            }

            internal void NotAccept(IEnumerable<Guid> sourceElementIDs)
            {
                this.AcceptAll();
                foreach (Guid g in sourceElementIDs)
                {
                    if (m_acceptedDomainTypes[m_targetElementID].Contains(g))
                        m_acceptedDomainTypes[m_targetElementID].Remove(g);
                }
                if (m_acceptedDomainTypes[m_targetElementID].Count == 0)
                    m_acceptedDomainTypes.Remove(m_targetElementID);
            }

            internal MergeTarget AcceptAll()
            {

                if (m_acceptedDomainTypes.ContainsKey(m_targetElementID))
                {
                    m_acceptedDomainTypes.Remove(m_targetElementID);
                }
                m_acceptedDomainTypes.Add(m_targetElementID, MVCVisualDesignerDomainModel.AllClassDomainIDs);
                this.m_toInclude = false;
                return this;
            }

            internal MergeTarget AcceptNone()
            {
                m_acceptedDomainTypes.Remove(m_targetElementID);
                this.m_toInclude = true;
                return this;
            }

            internal void Except(params Guid[] sourceElementIDs)
            {
                if (m_toInclude) 
                    this.Accept(sourceElementIDs);
                else 
                    this.Except(sourceElementIDs);
            }

            internal void Except(IEnumerable<Guid> sourceElementIDs)
            {
                if (m_toInclude) 
                    this.Accept(sourceElementIDs);
                else 
                    this.Except(sourceElementIDs);
            }

            // include or exclude in Except methods
            private bool m_toInclude = true;

            internal void SameAs(Guid anotherTargetElemID)
            {
                if (m_acceptedDomainTypes.ContainsKey(anotherTargetElemID))
                {
                    m_acceptedDomainTypes.Add(m_targetElementID, m_acceptedDomainTypes[anotherTargetElemID]);
                }
            }
        }

        // target -*> source list
        private static Dictionary<Guid, HashSet<Guid>> _acceptedDomainTypes = new Dictionary<Guid, HashSet<Guid>>();
    }
}

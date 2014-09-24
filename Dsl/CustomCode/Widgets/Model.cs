using Microsoft.VisualStudio.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public partial class VDModelStore
    {
        private Dictionary<string, List<IEventInfo>> m_events;
        private Dictionary<string, List<IActionInfo>> m_actions;
        private Dictionary<string, List<IActionJointInfo>> m_joints;

        private void InitData()
        {
            m_events = new Dictionary<string,List<IEventInfo>>();
            m_events.Add("JS Event", new List<IEventInfo>() { 
                    new EventInfo("click", "JS Event"), 
                    new EventInfo("change", "JS Event"), 
                    new EventInfo("dddd", "JS Event") ,
                    new PseudoEventInfo("Pseudo", "AND") ,
                    new PseudoEventInfo("Pseudo", "THEN") });

            m_actions = new Dictionary<string, List<IActionInfo>>();
            m_actions.Add("JS Action", new List<IActionInfo>() { 
                    new ClientActionInfo("add", "JS Action"), 
                    new ClientActionInfo("delete", "JS Action"), 
                    new ClientActionInfo("replace", "JS Action") });

            m_joints = new Dictionary<string, List<IActionJointInfo>>();
            m_joints.Add("replace", new List<IActionJointInfo>() { 
                    new ActionJointInfo("replace", "of"), 
                    new ActionJointInfo("replace", "with") });

            m_actions["JS Action"][2].Joints.AddRange(m_joints["replace"]);
        }

        public List<IEventInfo> GetSupportedEventList(WidgetType wt)
        {
            if (m_events == null) InitData();

            List<IEventInfo> eventInfo = m_events["JS Event"];
            return eventInfo;
        }

        public List<IActionInfo> GetSupportedActionList(WidgetType wt)
        {
            if (m_events == null) InitData();

            List<IActionInfo> actionInfo = m_actions["JS Action"];
            return actionInfo;
        }

        public IEventInfo GetEventInfo(string category, string name)
        {
            if (m_events == null) InitData();

            if (m_events.ContainsKey(category))
                return m_events[category].Find(x => x.Name == name);
            else
                return null;
        }

        public IActionInfo GetActionInfo(string category, string name)
        {
            if (m_events == null) InitData();

            if (m_actions.ContainsKey(category))
                return m_actions[category].Find(x => x.Name == name);
            else
                return null;
        }

        public IActionJointInfo GetActionJointInfo(string category, string name)
        {
            if (m_events == null) InitData();

            if (m_joints.ContainsKey(category))
                return m_joints[category].Find(x => x.Name == name);
            else
                return null;
        }
    }

    ///
    public partial class VDModelStore
    {
        // todo: init it from settngs
        // <fullTypeName>
        private HashSet<string> m_predefinedTypes = new HashSet<string>();

        private HashSet<string> m_primitiveTypes = new HashSet<string>() 
        { 
            "int", "bool", "string", "long", "short", "uint", "ushort", "ulong", "DateTime", 
            "int?", "bool?", "string?", "long?", "short?", "uint?", "ushort?", "ulong?", "DateTime?", 
            "IPAddress", "Version"
        };

        public HashSet<string> PrimitiveTypes { get { return m_primitiveTypes; } }

        public bool IsPredefinedType(string fullName)
        {
            return m_predefinedTypes.Contains(fullName);
        }

        public bool IsPrimitiveType(string fullName)
        {
            return m_primitiveTypes.Contains(fullName);
        }

        // Meta Type
        public VDMetaType GetMetaType(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName)) return null;
            return GetMetaType(new ModelTypeInfo(typeName));
        }

        public VDMetaType GetMetaType(ModelTypeInfo typeInfo)
        {
            if (typeInfo == null) return null;

            if (typeInfo.CollectionType == E_CollectionType.Dictionary)
            {
                return this.MetaTypes.Find(t =>
                {
                    VDDictionaryType dt = t as VDDictionaryType;
                    return dt != null
                        && (dt.Key != null && dt.Key.Type != null && dt.Key.Type.FullName == typeInfo.KeyType)
                        && (dt.Value != null && dt.Value.Type != null && dt.Value.Type.FullName == typeInfo.ValueType);
                });
            }
            else if (typeInfo.CollectionType == E_CollectionType.List)
            {
                return this.MetaTypes.Find(t =>
                {
                    VDListType lt = t as VDListType;
                    return lt != null 
                        && lt.Value != null && lt.Value.Type != null && lt.Value.Type.FullName == typeInfo.ValueType;
                });
            }
            else
            {
                return this.MetaTypes.Find(t =>
                {
                    return !(t is VDDictionaryType) && !(t is VDListType) && t.FullName == typeInfo.ValueType;
                });
            }
        }

        public VDMetaType CreateMetaType(string typeName)
        {
            return CreateMetaType(new ModelTypeInfo(typeName));
        }

        public VDMetaType CreateMetaType(ModelTypeInfo typeInfo)
        {
            VDMetaType metaType = GetMetaType(typeInfo);
            if (metaType != null) return metaType;

            if (typeInfo.CollectionType == E_CollectionType.Dictionary)
            {
                VDDictionaryType dictType = new VDDictionaryType(this.Partition,
                    new PropertyAssignment(VDModelType.NameSpaceDomainPropertyId, string.Empty));
                this.MetaTypes.Add(dictType);
                dictType.Key = new VDBuiltInProperty(this.Partition, new PropertyAssignment(VDModelMember.NameDomainPropertyId, Utility.Constants.STR_KEY_MEMBER));
                dictType.Key.Type = CreateMetaType(typeInfo.KeyType);
                dictType.Members.Add(dictType.Key);
                dictType.Value = new VDBuiltInProperty(this.Partition, new PropertyAssignment(VDModelMember.NameDomainPropertyId, Utility.Constants.STR_VALUE_MEMBER));
                dictType.Value.Type = CreateMetaType(typeInfo.ValueType);
                dictType.Members.Add(dictType.Value);
                dictType.Name = string.Format(Utility.Constants.STR_TYPE_DICTIONARY_FORMAT, dictType.Key.Type.FullName, dictType.Value.Type.FullName);
                return dictType;
            }
            else  if (typeInfo.CollectionType == E_CollectionType.List)
            {
                VDListType listType = new VDListType(this.Partition,
                    new PropertyAssignment(VDModelType.NameSpaceDomainPropertyId, string.Empty));
                this.MetaTypes.Add(listType);
                listType.Value = new VDBuiltInProperty(this.Partition, new PropertyAssignment(VDModelMember.NameDomainPropertyId, Utility.Constants.STR_VALUE_MEMBER));
                listType.Value.Type = CreateMetaType(typeInfo.ValueType);
                listType.Members.Add(listType.Value);
                listType.Name = string.Format(Utility.Constants.STR_TYPE_LIST_FORMAT, listType.Value.Type.FullName);
                return listType;
            }
            else
            {
                if (this.m_primitiveTypes.Contains(typeInfo.ValueType)) // primitive type
                {
                    VDPrimitiveType pt = new VDPrimitiveType(this.Partition);
                    this.MetaTypes.Add(pt);
                    pt.Value = new VDBuiltInProperty(this.Partition, new PropertyAssignment(VDModelMember.NameDomainPropertyId, Utility.Constants.STR_VALUE_MEMBER));
                    pt.Value.Type = pt;
                    pt.Members.Add(pt.Value);
                    pt.SetFullName(typeInfo.ValueType);
                    return pt;
                }
                else if (this.m_predefinedTypes.Contains(typeInfo.ValueType))
                {
                    metaType = new VDPredefinedType(this.Partition);
                }
                else
                {
                    metaType = new VDCustomType(this.Partition);
                }
                metaType.SetFullName(typeInfo.ValueType);
                this.MetaTypes.Add(metaType);
                return metaType;
            }
        }

        // Concrete Type
        public T GetConcreteType<T>(string typeName) where T : VDConcreteType
        {
            if (String.IsNullOrWhiteSpace(typeName)) return null;
            ModelTypeInfo typeInfo = new ModelTypeInfo(typeName);
            return GetConcreteType<T>(typeInfo);
        }

        public T GetConcreteType<T>(ModelTypeInfo typeInfo) where T : VDConcreteType
        {
            if (typeInfo == null) return null;

            if (typeInfo.CollectionType == E_CollectionType.Dictionary)
            {
                return this.ConcreteTypes.Find(t =>
                {
                    VDDictionaryType dt = t.Meta as VDDictionaryType;
                    return t is T && dt != null
                        && (dt.Key != null && dt.Key.Type != null && dt.Key.Type.FullName == typeInfo.KeyType)
                        && (dt.Value != null && dt.Value.Type != null && dt.Value.Type.FullName == typeInfo.ValueType);
                }) as T;
            }
            else if (typeInfo.CollectionType == E_CollectionType.List)
            {
                return this.ConcreteTypes.Find(t =>
                {
                    VDListType lt = t.Meta as VDListType;
                    return t is T && lt != null
                        && lt.Value != null && lt.Value.Type != null && lt.Value.Type.FullName == typeInfo.ValueType;
                }) as T;
            }
            else
            {
                return this.ConcreteTypes.Find(t =>
                {
                    return t is T && !(t.Meta is VDDictionaryType) && !(t.Meta is VDListType) && t.Meta.FullName == typeInfo.ValueType;
                }) as T;
            }
        }

        public T CreateConcreteType<T>(string metaTypeFullName) where T : VDConcreteType
        {
            VDMetaType metaType = CreateMetaType(metaTypeFullName);
            return CreateConcreteType<T>(metaType);
        }

        public T CreateConcreteType<T>(ModelTypeInfo typeInfo) where T : VDConcreteType
        {
            VDMetaType metaType = CreateMetaType(typeInfo);
            return CreateConcreteType<T>(metaType);
        }

        public T CreateConcreteType<T>(VDMetaType metaType, VDConcreteMember memberOfThisType = null) where T : VDConcreteType
        {
            //T concreteType = GetConcreteType<T>(metaType.FullName);
            //if (concreteType != null) return concreteType;

            T concreteType = newConcreteType<T>();
            concreteType.Meta = metaType;
            this.ConcreteTypes.Add(concreteType);

            // avoid recursive infinitely
            if (memberOfThisType != null)
            {
                bool usedTypeInParent = false;
                VDConcreteType hostType = memberOfThisType.Host as VDConcreteType;
                while (hostType != null)
                {
                    if (hostType.FullName == concreteType.FullName)
                    {
                        usedTypeInParent = true;
                        break;
                    }
                    hostType = hostType.HostType;
                }
                if (usedTypeInParent) return concreteType;
            }

            // add members
            foreach (VDModelMember m in metaType.Members)
            {
                VDMetaMember metaMember = m as VDMetaMember;
                if (metaMember == null) continue;

                VDConcreteMember newMember = concreteType.newConcreteMember();
                newMember.Meta = metaMember;
                if (metaMember.Type is VDPrimitiveType)
                {
                    newMember.Type = GetPrimitiveMemberType(metaMember.Type.FullName);
                }
                else
                {
                    newMember.Type = this.CreateConcreteType<T>(metaMember.Type as VDMetaType, newMember);
                }
                concreteType.Members.Add(newMember);
            }

            return concreteType;
        }

        private T newConcreteType<T>() where T : VDConcreteType
        {
            if (typeof(T) == typeof(VDWidgetValue))
                return new VDWidgetValue(this.Partition) as T;
            else if (typeof(T) == typeof(VDActionData))
                return new VDActionData(this.Partition) as T;
            else if (typeof(T) == typeof(VDViewModel))
                return new VDViewModel(this.Partition) as T;
            else
                return default(T);
        }

        internal protected VDPrimitiveMemberType GetPrimitiveMemberType(string typeName)
        {
            if (string.IsNullOrEmpty(typeName)) 
                throw new ArgumentNullException("typename");

            if (!this.IsPrimitiveType(typeName))
                throw new Exception(string.Format("{0} is not primitive type", typeName));

            VDPrimitiveMemberType pmt = this.ConcreteTypes.Find(t => {
                return t is VDPrimitiveMemberType && t.Meta is VDPrimitiveType && t.Meta.FullName == typeName;
            }) as VDPrimitiveMemberType;

            if (pmt != null) return pmt;

            VDPrimitiveType metaType = CreateMetaType(typeName) as VDPrimitiveType;
            if (metaType == null)
                throw new Exception(string.Format("Failed to get primitive type {0}", typeName));

            pmt = new VDPrimitiveMemberType(this.Partition);
            pmt.Meta = metaType;
            this.ConcreteTypes.Add(pmt);

            return pmt;
        }
    }
}

namespace MVCVisualDesigner
{
    public enum E_CollectionType
    {
        Not_Collection = 0,
        Dictionary,
        List
    }

    public class ModelTypeInfo
    {
        public ModelTypeInfo()
        {
        }

        public ModelTypeInfo(string typeName)
        {
            if (string.IsNullOrEmpty(typeName)) return;
            //todo: regex to parse the typename
            string[] tokens = typeName.Split(new char[] { ' ', '<', ',', '>' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length > 0)
            {
                if (tokens.Length == 3 && tokens[0] == Utility.Constants.STR_TYPE_DICTIONARY)
                {
                    CollectionType = E_CollectionType.Dictionary;
                    KeyType = tokens[0];
                    ValueType = tokens[1];
                }
                else if (tokens.Length == 2 && tokens[0] == Utility.Constants.STR_TYPE_LIST)
                {
                    CollectionType = E_CollectionType.List;
                    ValueType = tokens[0];
                }
                else if (tokens.Length == 1)
                {
                    CollectionType = E_CollectionType.Not_Collection;
                    ValueType = tokens[0];
                }
            }
        }

        public E_CollectionType CollectionType { get; set; }

        private string m_key = string.Empty, m_value = string.Empty;
        public string KeyType
        {
            get { return m_key; }
            set { m_key = value ?? string.Empty; }
        }
        public string ValueType
        {
            get { return m_value; }
            set { m_value = value ?? string.Empty; }
        }

        public override string ToString()
        {
            string str = string.Empty;
            if (CollectionType == E_CollectionType.Not_Collection)
                str = ValueType;
            else if (CollectionType == E_CollectionType.Dictionary)
                str = string.Format(Utility.Constants.STR_TYPE_DICTIONARY_FORMAT, this.KeyType, this.ValueType);
            else if (CollectionType == E_CollectionType.List)
                str = string.Format(Utility.Constants.STR_TYPE_LIST_FORMAT, this.ValueType);
            return str;
        }

        public bool HasUnspecifiedValue
        {
            get
            {
                if (string.IsNullOrEmpty(this.ValueType) || string.Compare(this.ValueType, Utility.Constants.STR_NOT_SPECIFIED) == 0)
                    return true;

                if (CollectionType == E_CollectionType.Dictionary)
                {
                    if (string.IsNullOrEmpty(this.KeyType) || string.Compare(this.KeyType, Utility.Constants.STR_NOT_SPECIFIED) == 0)
                        return true;
                }
                return false;
            }
        }

        public bool IsValidName
        {
            get
            {
                return true;
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////
    // Base
    public abstract partial class VDModelType
    {
        // custom storage property
        protected string m_name;
        internal virtual string GetNameValue() { return m_name; }
        internal virtual void SetNameValue(string value) { m_name = value; }

        // custom storage property
        protected string m_nameSpace;
        internal virtual string GetNameSpaceValue() { return m_nameSpace; }
        internal virtual void SetNameSpaceValue(string value) { m_nameSpace = value; }

        // custom storage property
        protected string m_dispName;
        internal virtual string GetDispNameValue() { return m_dispName; }
        internal virtual void SetDispNameValue(string value) { m_dispName = value; }

        // calculated property
        internal string GetFullNameValue()
        {
            if (string.IsNullOrWhiteSpace(this.NameSpace))
                return this.Name;
            else
                return this.NameSpace + "." + this.Name;
        }

        // calculated property
        public void SetFullName(string fullName)
        {
            string name, nameSpace;
            Utility.Helper.SplitFullName(fullName, out nameSpace, out name);
            this.Name = name;
            this.NameSpace = nameSpace;
        }

        //
        public abstract void AddMember<T>(string memberMetaType, string name) where T : VDMetaMember;
        public abstract void AddMember<T>(VDMetaType memberMetaType, string name) where T : VDMetaMember;
        public abstract void DeleteMember(string name);
        public abstract void ChangeMemberType(string member, string newMetaType);
        public abstract void ChangeMemberType(VDMetaMember member, VDMetaType newType);

        protected static VDMetaMember newMetaMember<T>(Partition partition, string name) where T : VDMetaMember
        {
            VDMetaMember mem = null;
            if (typeof(T) == typeof(VDProperty))
                return new VDProperty(partition, new PropertyAssignment(VDModelMember.NameDomainPropertyId, name));
            else if (typeof(T) == typeof(VDMethod))
                return new VDMethod(partition, new PropertyAssignment(VDModelMember.NameDomainPropertyId, name));
            else if (typeof(T) == typeof(VDField))
                return new VDField(partition, new PropertyAssignment(VDModelMember.NameDomainPropertyId, name));
            else if (typeof(T) == typeof(VDBuiltInProperty))
                return new VDBuiltInProperty(partition, new PropertyAssignment(VDModelMember.NameDomainPropertyId, name));
            return mem;
        }
    }

    public abstract partial class VDModelMember
    {
        // custom storage property
        protected string m_name;
        internal virtual string GetNameValue() { return m_name; }
        internal virtual void SetNameValue(string value) { m_name = value; }

        // custom storage property
        protected string m_dispName;
        internal virtual string GetDispNameValue() { return m_dispName; }
        internal virtual void SetDispNameValue(string value) { m_dispName = value; }

        //
        public abstract void ChangeName(string newName);
        public abstract void ChangeDispName(string newDispName);
        public abstract void ChangeType(string newTypeName);
    }

    /////////////////////////////////////////////////////////////////////////////////////
    // meta type
    public partial class VDMetaType : VDModelType
    {
        public override void AddMember<T>(string memberMetaType, string name)
        {
            if (string.IsNullOrWhiteSpace(memberMetaType)) throw new ArgumentNullException("memberMetaType");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

            AddMember<T>(this.ModelStore.CreateMetaType(memberMetaType), name);
        }

        public override void AddMember<T>(VDMetaType memberMetaType, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

            VDMetaMember metaMember = this.Members.Find(m => m.Name == name) as VDMetaMember;
            if (metaMember != null)
            {
                if (metaMember.Type.FullName == memberMetaType.FullName)
                    return;
                else
                    throw new Exception(string.Format("There is alreay a member named {0} of type {1}", name, metaMember.Type.FullName));
            }

            metaMember = newMetaMember<T>(this.Partition, name);
            metaMember.Type = memberMetaType;
            this.Members.Add(metaMember);

            foreach(VDConcreteType t in this.ConcreteTypes)
            {
                t.AddMemberImpl(metaMember); 
            }
        }

        public override void DeleteMember(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

            VDMetaMember metaMember = Members.Find(m => m.Name == name) as VDMetaMember;
            if (metaMember == null) return;

            foreach(VDConcreteType t in this.ConcreteTypes)
            {
                t.DeleteMemberImpl(metaMember); 
            }
            metaMember.Delete();
        }

        public override void ChangeMemberType(string member, string newMetaType)
        {
            if (string.IsNullOrWhiteSpace(member)) throw new ArgumentNullException("member");
            if (string.IsNullOrWhiteSpace(newMetaType)) throw new ArgumentNullException("newMetaType");

            VDMetaMember metaMember = Members.Find(m => m.Name == member) as VDMetaMember;
            if (metaMember == null) return;

            VDMetaType metaType = this.ModelStore.CreateMetaType(newMetaType);
            ChangeMemberType(metaMember, metaType);
        }

        public override void ChangeMemberType(VDMetaMember metaMember, VDMetaType newMetaType)
        {
            if (metaMember == null) throw new ArgumentNullException("metaMember");
            if (newMetaType == null) throw new ArgumentNullException("newMetaType");

            metaMember.Type = newMetaType;
            foreach(VDConcreteMember m in metaMember.ConcreteMembers)
            {
                ((VDConcreteType)m.Host).ChangeMemberTypeImpl(metaMember, newMetaType, m);
            }
        }
    }

    public partial class VDDictionaryType : VDMetaType
    {
    }

    public partial class VDListType : VDMetaType
    {
    }

    public partial class VDReferenceType : VDMetaType
    {
    }

    /////////////////////////////////////////////////////////////////////////////////////
    // Meta Member
    public partial class VDMetaMember : VDModelMember
    {
        public override void ChangeName(string newName)
        {
            this.Name = newName;
        }

        public override void ChangeDispName(string newDispName)
        {
            this.DispName = newDispName;
        }

        public override void ChangeType(string newTypeName)
        {
            if (this.Host != null) this.Host.ChangeMemberType(this.Name, newTypeName);
        }
    }

    public partial class VDBuiltInProperty : VDMetaMember
    {
    }

    /////////////////////////////////////////////////////////////////////////////////////
    // concrete type
    public partial class VDConcreteTypeBase : VDModelType
    {
        internal override string GetNameValue()
        {
            return this.Meta != null ? this.Meta.GetNameValue() : base.GetNameValue();
        }

        internal override void SetNameValue(string value)
        {
            if (this.Meta != null) 
                this.Meta.SetNameValue(value);
            else 
                base.SetNameValue(value);
        }

        internal override string GetNameSpaceValue()
        {
            return this.Meta != null ? this.Meta.GetNameSpaceValue() : base.GetNameSpaceValue();
        }

        internal override void SetNameSpaceValue(string value)
        {
            if (this.Meta != null)
                this.Meta.SetNameSpaceValue(value);
            else
                base.SetNameSpaceValue(value);
        }

        internal override string GetDispNameValue()
        {
            return this.Meta != null ? this.Meta.GetDispNameValue() : base.GetDispNameValue();
        }

        internal override void SetDispNameValue(string value)
        {
            if (this.Meta != null) 
                this.Meta.SetDispNameValue(value); 
            else 
                base.SetDispNameValue(value);
        }

        // the Multiplicity of relationship MembersOfThisType is 0..1, but it's 0..* now
        // because it's derived from parent relationship
        public VDConcreteMember MemberOfThisType
        {
            get
            {
                if (this.MembersOfThisType != null && this.MembersOfThisType.Count > 0)
                {
                    System.Diagnostics.Debug.Assert(this.MembersOfThisType.Count <= 1);
                    return this.MembersOfThisType.FirstOrDefault() as VDConcreteMember;
                }

                return null;
            }
        }

        public VDConcreteType HostType
        {
            get
            {
                VDConcreteMember m = this.MemberOfThisType;
                if (m != null)
                    return m.Host as VDConcreteType;
                else
                    return null;
            }
        }
    }

    public partial class VDConcreteType : VDConcreteTypeBase
    {
        // delegate to Meta type
        public override void AddMember<T>(string memberMetaType, string name)
        {
            if (this.Meta != null) this.Meta.AddMember<T>(memberMetaType, name);
        }

        public override void AddMember<T>(VDMetaType memberMetaType, string name)
        {
            if (this.Meta != null) this.Meta.AddMember<T>(memberMetaType, name);
        }

        public override void DeleteMember(string name)
        {
            if (this.Meta != null) this.Meta.DeleteMember(name);
        }

        public override void ChangeMemberType(string member, string newMetaType)
        {
            if (this.Meta != null) this.Meta.ChangeMemberType(member, newMetaType);
        }

        public override void ChangeMemberType(VDMetaMember member, VDMetaType newMetaType)
        {
            if (this.Meta != null) this.Meta.ChangeMemberType(member, newMetaType);
        }

        //
        internal virtual void AddMemberImpl(VDMetaMember metaMember)
        {
            VDConcreteMember newMember = newConcreteMember();
            newMember.Meta = metaMember;
            if (metaMember.Type is VDPrimitiveType)
                newMember.Type = this.ModelStore.GetPrimitiveMemberType(metaMember.Type.FullName);
            else
                newMember.Type = this.newInstance(metaMember.Type as VDMetaType);
            this.Members.Add(newMember);
        }

        internal virtual void DeleteMemberImpl(VDMetaMember metaMember)
        {
            VDModelMember member = this.Members.Find(m => m.Name == metaMember.Name);
            if (member != null)
            {
                member.Delete();
            }
        }

        internal virtual void ChangeMemberTypeImpl(VDMetaMember metaMember, VDMetaType memberMetaType, VDConcreteMember member)
        {
            if (member.Type != null && member.Type.MembersOfThisType.Count <= 1 && !(member.Type is VDPrimitiveMemberType))
            {
                member.Type.Delete();
            }
            if (memberMetaType is VDPrimitiveType)
                member.Type = this.ModelStore.GetPrimitiveMemberType(memberMetaType.FullName);
            else
                member.Type = this.newInstance(memberMetaType);
        }

        // if has external reference except VDWidget.WidgetValue/VDActionBase.ActionData/VDView.Model
        public bool HasExternalReference
        {
            get
            {
                if (this.MembersOfThisType.Count <= 0 && this.Members.Count <= 0)
                    return false;

                if (this.Members.Count < this.MembersOfThisType.Count)
                    return true;

                HashSet<Guid> members = new HashSet<Guid>();
                this.Members.ForEach(m => members.Add(m.Id));

                foreach (var m in this.MembersOfThisType)
                {
                    if (!members.Contains(m.Id)) return true;
                }

                return false;
            }
        }

        virtual internal VDConcreteMember newConcreteMember() { return null; }
        virtual internal VDConcreteType newInstance(VDMetaType metaType) { return null; }
    }

    public partial class VDWidgetValue : VDConcreteType
    {
        internal override VDConcreteMember newConcreteMember()
        {
            return new VDWidgetValueMember(this.Partition);
        }

        internal override VDConcreteType newInstance(VDMetaType metaType)
        {
            return this.ModelStore.CreateConcreteType<VDWidgetValue>(metaType);
        }
    }

    public partial class VDActionData : VDConcreteType
    {
        internal override VDConcreteMember newConcreteMember()
        {
            return new VDActionDataMember(this.Partition);
        }

        internal override VDConcreteType newInstance(VDMetaType metaType)
        {
            return this.ModelStore.CreateConcreteType<VDActionData>(metaType);
        }
    }

    public partial class VDViewModel : VDConcreteType
    {
        internal override VDConcreteMember newConcreteMember()
        {
            return new VDViewModelMember(this.Partition);
        }

        internal override VDConcreteType newInstance(VDMetaType metaType)
        {
            return this.ModelStore.CreateConcreteType<VDViewModel>(metaType);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////
    // Concrete Member
    public partial class VDConcreteMemberBase : VDModelMember
    {
        internal override string GetNameValue()
        {
            return this.Meta != null ? this.Meta.GetNameValue() : base.GetNameValue();
        }

        internal override void SetNameValue(string value)
        {
            if (this.Meta != null)
                this.Meta.SetNameValue(value);
            else
                base.SetNameValue(value);
        }

        internal override string GetDispNameValue()
        {
            return this.Meta != null ? this.Meta.GetDispNameValue() : base.GetDispNameValue();
        }

        internal override void SetDispNameValue(string value)
        {
            if (this.Meta != null)
                this.Meta.SetDispNameValue(value);
            else
                base.SetDispNameValue(value);
        }
    }

    public partial class VDConcreteMember : VDConcreteMemberBase
    {
        public override void ChangeName(string newName)
        {
            if (this.Meta != null) this.Meta.ChangeName(newName);
        }

        public override void ChangeDispName(string newDispName)
        {
            if (this.Meta != null) this.Meta.ChangeDispName(newDispName);
        }

        public override void ChangeType(string newTypeName)
        {
            if (this.Host != null) this.Host.ChangeMemberType(this.Name, newTypeName);
        }
    }
}
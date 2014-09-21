using Microsoft.VisualStudio.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if todel
namespace MVCVisualDesigner
{
    public partial class VDModelStore
    {
        public VDModelType2 GetModelType(string fullTypeName)
        {
            if (string.IsNullOrEmpty(fullTypeName)) return null;

            foreach(var modelType in this.ModelTypes)
            {
                if (string.Compare(modelType.FullName, fullTypeName) == 0)
                    return modelType;
            }
            return null;
        }

        public VDModelType2 GetModelType(string typeName, string nameSpace)
        {
            string fullTypeName;
            if (string.IsNullOrEmpty(nameSpace))
            {
                fullTypeName = typeName;
            }
            else
            {
                fullTypeName = string.Format("{0}.{1}", nameSpace, typeName);
            }
            return GetModelType(fullTypeName);
        }

        public VDModelType2 CreateModelType(ModelTypeValue modelType, HashSet<string> predefinedTypes)
        {
            if (modelType.CollectionType == E_CollectionType.Not_Collection)
                return CreateModelType(modelType.ValueType, predefinedTypes);
            else if (modelType.CollectionType == E_CollectionType.Dictionary)
                return CreateDictionaryModelType(modelType.KeyType, modelType.ValueType, predefinedTypes);
            else if (modelType.CollectionType == E_CollectionType.List)
                return CreateListModelType(modelType.ValueType, predefinedTypes);

            return null;
        }

        public VDModelType2 CreateModelType(string fullTypeName, HashSet<string> predefinedTypes)
        {
            VDModelType2 type = GetModelType(fullTypeName);
            if (type != null) return type;

            string typeName = null;
            string nameSpace = null;
            Utility.Helper.SplitFullName(fullTypeName, out nameSpace, out typeName);


            Guid modelTypeID;
            if (predefinedTypes != null && predefinedTypes.Contains(fullTypeName))
                modelTypeID = VDPredefinedType.DomainClassId;
            else
                modelTypeID = VDCustomType.DomainClassId;

            type = this.Store.ElementFactory.CreateElement(modelTypeID,
                new PropertyAssignment(VDModelType2.NameDomainPropertyId, typeName),
                new PropertyAssignment(VDModelType2.NameSpaceDomainPropertyId, nameSpace)) as VDModelType2;
            this.ModelTypes.Add(type);

            return type;
        }

        public VDModelType2 CreateModelType(string typeName, string nameSpace, HashSet<string> predefinedTypes)
        {
            string fullTypeName;
            if (string.IsNullOrEmpty(nameSpace))
            {
                fullTypeName = typeName;
            }
            else
            {
                fullTypeName = string.Format("{0}.{1}", nameSpace, typeName);
            }
            return CreateModelType(fullTypeName, predefinedTypes);
        }

        public VDDictionaryType CreateDictionaryModelType(string keyTypeFullName, string valueTypeFullName, HashSet<string> predefinedTypes)
        {
            string dictFullName = VDDictionaryType.GetDictionaryFullName(keyTypeFullName, valueTypeFullName);
            VDDictionaryType dictType = GetModelType(dictFullName) as VDDictionaryType;
            if (dictType != null) return dictType;

            dictType = this.Store.ElementFactory.CreateElement(VDDictionaryType.DomainClassId,
                new PropertyAssignment(VDModelType2.NameDomainPropertyId, dictFullName),
                new PropertyAssignment(VDModelType2.NameSpaceDomainPropertyId, string.Empty)) as VDDictionaryType;
            this.ModelTypes.Add(dictType);

            // key member info
            VDModelType2 keyType = CreateModelType(keyTypeFullName, predefinedTypes);
            VDPlaceholderInfo keyMemInfo = this.Store.ElementFactory.CreateElement(
                    VDPlaceholderInfo.DomainClassId,
                    new PropertyAssignment(VDModelMemberInfo.NameDomainPropertyId, "Key")) as VDPlaceholderInfo;
            dictType.ModelMembers.Add(keyMemInfo);
            dictType.KeyInfo = keyMemInfo;
            keyMemInfo.Type = keyType;

            // value member info
            VDModelType2 valueType = CreateModelType(valueTypeFullName, predefinedTypes);
            VDPlaceholderInfo valueMemInfo = this.Store.ElementFactory.CreateElement(
                    VDPlaceholderInfo.DomainClassId,
                    new PropertyAssignment(VDModelMemberInfo.NameDomainPropertyId, "Value")) as VDPlaceholderInfo;
            dictType.ModelMembers.Add(valueMemInfo);
            dictType.ValueInfo = valueMemInfo;
            valueMemInfo.Type = valueType;

            return dictType;
        }

        public VDListType CreateListModelType(string valueTypeFullName, HashSet<string> predefinedTypes)
        {
            string listFullName = VDListType.GetListFullName(valueTypeFullName);
            VDListType listType = GetModelType(listFullName) as VDListType;
            if (listType != null) return listType;

            listType = this.Store.ElementFactory.CreateElement(VDListType.DomainClassId,
                new PropertyAssignment(VDModelType2.NameDomainPropertyId, listFullName),
                new PropertyAssignment(VDModelType2.NameSpaceDomainPropertyId, string.Empty)) as VDListType;
            this.ModelTypes.Add(listType);

            // value member info
            VDModelType2 valueType = CreateModelType(valueTypeFullName, predefinedTypes);
            VDPlaceholderInfo valueMemInfo = this.Store.ElementFactory.CreateElement(
                    VDPlaceholderInfo.DomainClassId,
                    new PropertyAssignment(VDModelMemberInfo.NameDomainPropertyId, "Value")) as VDPlaceholderInfo;
            listType.ModelMembers.Add(valueMemInfo);
            listType.ValueInfo = valueMemInfo;
            valueMemInfo.Type = valueType;

            return listType;
        }

        public TModelMemberInstance CreateModelInstance<TModelMemberInstance>(VDModelType2 modelType, string modelName) 
            where TModelMemberInstance : VDModelMemberInstance
        {
            if (modelType == null) return null;

            // create model instance
            VDModelMemberInstance modelInstance = this.Store.ElementFactory.CreateElement(getDomainClassID<TModelMemberInstance>(),
                new PropertyAssignment(VDModelMemberInstance.NameDomainPropertyId, modelName)) as VDModelMemberInstance;
            modelInstance.ModelType = modelType;
            this.ModelMemberInstances.Add(modelInstance);
            this.ModelInstances.Add(modelInstance);

            // create model member instances according to model type definition
            Guid modelMemberInstanceDomainClassId = getDomainClassID<TModelMemberInstance>();
            createChildModelMemberInstances(modelInstance, modelMemberInstanceDomainClassId);

            return modelInstance as TModelMemberInstance;
        }

        private void createChildModelMemberInstances(VDModelMemberInstance parentModelMemberInstance, Guid modelMemberInstanceDomainClassId)
        {
            Stack<VDModelType2> superTypes = new Stack<VDModelType2>();
            var mmi = parentModelMemberInstance.ParentMemberInstance;
            while (mmi != null && mmi.ModelType != null)
            {
                superTypes.Push(mmi.ModelType);
                mmi = mmi.ParentMemberInstance;
            }

            createChildModelMemberInstances(parentModelMemberInstance, modelMemberInstanceDomainClassId, superTypes);
        }

        private void createChildModelMemberInstances(VDModelMemberInstance parentModelMemberInstance, Guid modelMemberInstanceDomainClassId, 
                Stack<VDModelType2> superTypes)
        {
            // to avoid infinite recursion, if type of a member is same as any super member's type, 
            // don't create child member instances for it
            VDModelType2 modelType = parentModelMemberInstance.ModelType;
            if (modelType == null || superTypes.Contains(modelType)) return;
            superTypes.Push(modelType);

            foreach (VDModelMemberInfo modelMemberInfo in modelType.ModelMembers)
            {
                var modelMemberInstance = this.Store.ElementFactory.CreateElement(modelMemberInstanceDomainClassId) as VDModelMemberInstance;
                modelMemberInstance.ModelMemberInfo = modelMemberInfo;
                modelMemberInstance.ModelType = modelMemberInfo.Type;
                this.ModelMemberInstances.Add(modelMemberInstance);
                parentModelMemberInstance.ChildMemberInstances.Add(modelMemberInstance);

                // create sub members recursivly
                createChildModelMemberInstances(modelMemberInstance, modelMemberInstanceDomainClassId, superTypes);
            }

            superTypes.Pop();
        }

        private void createModelMemberInstance(VDModelMemberInstance parentModelMemberInstance,
            VDModelMemberInfo newMemberInfo,
            Guid modelMemberInstanceDomainClassId)
        {
            Stack<VDModelType2> superTypes = new Stack<VDModelType2>(); //the order is reverse, but doesn't matter
            var mmi = parentModelMemberInstance.ParentMemberInstance;
            while (mmi != null && mmi.ModelType != null)
            {
                superTypes.Push(mmi.ModelType);
                mmi = mmi.ParentMemberInstance;
            }

            createModelMemberInstance(parentModelMemberInstance, newMemberInfo, modelMemberInstanceDomainClassId, superTypes);
        }

        private void createModelMemberInstance(VDModelMemberInstance parentModelMemberInstance, 
                    VDModelMemberInfo newMemberInfo,
                    Guid modelMemberInstanceDomainClassId, Stack<VDModelType2> superTypes)
        {
            VDModelType2 parentType = parentModelMemberInstance.ModelType;
            if (parentType == null || superTypes.Contains(parentType))
                return;
            superTypes.Push(parentType);

            VDModelMemberInstance newMemberInstance = this.Store.ElementFactory.CreateElement(
                        modelMemberInstanceDomainClassId) as VDModelMemberInstance;
            newMemberInstance.ModelMemberInfo = newMemberInfo;
            newMemberInstance.ModelType = newMemberInfo.Type;
            this.ModelMemberInstances.Add(newMemberInstance);
            parentModelMemberInstance.ChildMemberInstances.Add(newMemberInstance);

            createChildModelMemberInstances(newMemberInstance, modelMemberInstanceDomainClassId, superTypes);
        }

        // todo: check if all embedding and reference links are deleted
        public void DeleteModelInstance(VDModelMemberInstance modelInstance)
        {
            deleteModelMemberInstance(modelInstance);
        }

        private void deleteModelMemberInstance(VDModelMemberInstance parentModelMemberInstance)
        {
            List<VDModelMemberInstance> instListToDel = new List<VDModelMemberInstance>();
            foreach (VDModelMemberInstance childMember in parentModelMemberInstance.ChildMemberInstances)
            {
                instListToDel.Add(childMember);
            }

            foreach (var childMember in instListToDel)
            {
                deleteModelMemberInstance(childMember);
                childMember.Delete();
            }

            parentModelMemberInstance.Delete();
        }

        private void deleteChildModelMemberInstances(VDModelMemberInstance parentModelMemberInstance)
        {
            List<VDModelMemberInstance> instListToDel = new List<VDModelMemberInstance>();
            foreach (VDModelMemberInstance mmi in parentModelMemberInstance.ChildMemberInstances)
            {
                instListToDel.Add(mmi);
            }

            foreach (var mmi in instListToDel)
            {
                deleteModelMemberInstance(mmi);
            }
        }

        // add member to model type and all related mode instances and model instance members
        public void AddMemberToModelType<TModelMemberInfo, TModelMemberInstance>(
                VDModelType2 modelType, string memberName, string memberTypeName, HashSet<string> predefinedTypes)
                    where TModelMemberInfo : VDModelMemberInfo 
                    where TModelMemberInstance : VDModelMemberInstance
        {
            // not all type can add new member, such as primitive type, external type etc.
            if (modelType.IsReadOnly)
                throw new Exception(string.Format("Type {0}::{1} is readonly.", modelType.NameSpace ?? string.Empty, modelType.Name ?? string.Empty));

            // create new ModelMemberInfo 
            VDModelMemberInfo newMemberInfo = this.Store.ElementFactory.CreateElement(
                    getDomainClassID<TModelMemberInfo>(),
                    new PropertyAssignment(VDModelMemberInfo.NameDomainPropertyId, memberName)) as VDModelMemberInfo;
            modelType.ModelMembers.Add(newMemberInfo);
            //
            // set model member type
            VDModelType2 newMemberType = this.CreateModelType(memberTypeName, predefinedTypes);
            newMemberInfo.Type = newMemberType;

            //
            // add a new member instance to all model instances and model instance members of type "modelType", 
            Guid modelMemberInstanceDomainClassId = getDomainClassID<TModelMemberInstance>();
            List<VDModelMemberInstance> modelMemberInstances = getModelMemberInstancesOfType(modelType);
            foreach(var inst in modelMemberInstances)
            {
                createModelMemberInstance(inst, newMemberInfo, modelMemberInstanceDomainClassId);
            }
        }

        private List<VDModelMemberInstance> getModelMemberInstancesOfType(VDModelType2 modelType)
        {
            List<VDModelMemberInstance> mmi = new List<VDModelMemberInstance>();
            foreach(VDModelMemberInstance minst in modelType.ModelMemberInstancesOfThisType)
            {
                mmi.Add(minst);
            }
            return mmi;
        }

        // delete member from model type and all related mode instances and model instance members
        public void RemoveMemberFromModelType(VDModelType2 modelType, VDModelMemberInfo modelMemberInfoToRemove)
        {
            // not all type can add new member, such as primitive type, external type etc.
            if (modelType.IsReadOnly)
                throw new Exception(string.Format("Type {0}::{1} is readonly.", modelType.NameSpace ?? string.Empty, modelType.Name ?? string.Empty));
            
            // delete member instances
            List<VDModelMemberInstance> instancesToDel = new List<VDModelMemberInstance>(modelMemberInfoToRemove.MemberInstancesOfThisType);
            foreach(var memberInstance in instancesToDel)
            {
                if (!memberInstance.IsDeleted && !memberInstance.IsDeleting)
                {
                    deleteModelMemberInstance(memberInstance);
                }
            }

            // delete member info
            modelMemberInfoToRemove.Delete();
        }

        public void ChangeModelMemberType<TModelMemberInstance>(
                VDModelMemberInfo modelMemberInfoToChange,
                ModelTypeValue newModelType, HashSet<string> predefinedTypes)
            where TModelMemberInstance : VDModelMemberInstance
        {
            VDModelType2 newMemberType = CreateModelType(newModelType, predefinedTypes);
            modelMemberInfoToChange.Type = newMemberType;

            // delete the child member instances which is created based on old type definition
            foreach (VDModelMemberInstance mmi in modelMemberInfoToChange.MemberInstancesOfThisType)
            {
                deleteChildModelMemberInstances(mmi);
            }

            // create new child member instances according to the new type
            Guid modelMemberInstanceDomainClassId = getDomainClassID<TModelMemberInstance>();
            foreach(VDModelMemberInstance mmi in modelMemberInfoToChange.MemberInstancesOfThisType)
            {
                mmi.ModelType = newMemberType;
                createChildModelMemberInstances(mmi, modelMemberInstanceDomainClassId);
            }
        }


        private static Guid getDomainClassID<T>() where T : ModelElement
        {
            if (typeof(T) == typeof(VDModelMemberInstance))
                return VDModelMemberInstance.DomainClassId;
            else if (typeof(T) == typeof(VDViewModelMemberInstance))
                return VDViewModelMemberInstance.DomainClassId;
            else if (typeof(T) == typeof(VDWidgetModelMemberInstance))
                return VDWidgetModelMemberInstance.DomainClassId;
            else if (typeof(T) == typeof(VDActionModelMemberInstance))
                return VDActionModelMemberInstance.DomainClassId;

            else if (typeof(T) == typeof(VDModelMemberInfo))
                return VDModelMemberInfo.DomainClassId;
            else if (typeof(T) == typeof(VDPropertyInfo))
                return VDPropertyInfo.DomainClassId;
            else if (typeof(T) == typeof(VDFieldInfo))
                return VDFieldInfo.DomainClassId;
            else if (typeof(T) == typeof(VDMethodInfo))
                return VDMethodInfo.DomainClassId;
            else if (typeof(T) == typeof(VDPlaceholderInfo))
                return VDPlaceholderInfo.DomainClassId;
            else if (typeof(T) == typeof(VDModelInstanceInfo))
                return VDModelInstanceInfo.DomainClassId;

            else if (typeof(T) == typeof(VDModelType2))
                return VDModelType2.DomainClassId;
            else if (typeof(T) == typeof(VDPredefinedType))
                return VDPredefinedType.DomainClassId;
            else if (typeof(T) == typeof(VDExternalType))
                return VDExternalType.DomainClassId;
            else if (typeof(T) == typeof(VDCustomType))
                return VDCustomType.DomainClassId;
            else if (typeof(T) == typeof(VDDictionaryType))
                return VDDictionaryType.DomainClassId;
            else if (typeof(T) == typeof(VDListType))
                return VDListType.DomainClassId;

            return Guid.Empty;
        }
    }

#region "Model Instance"
    public partial class VDModelMemberInstance
    {
        // custom storage Name property
        private string m_name;
        internal virtual string GetNameValue()
        {
            if (this.ModelMemberInfo != null)
                return this.ModelMemberInfo.Name;
            else
                return m_name;
        }

        internal virtual void SetNameValue(string newName)
        {
            if (this.ModelMemberInfo == null)
                m_name = newName;
        }

        // calculated TypeName property
        internal virtual string GetTypeNameValue()
        {
            if (this.ModelType != null)
                return this.ModelType.FullName;
            else
                return string.Empty;
        }

        // if there is no model memeber, then its top level model member instance,
        // that is, it's model instance
        internal bool IsModelInstance 
        { 
            get 
            { 
                return this.ModelMemberInfo == null; 
                /* or this.Widget != null */
            } 
        }

        public VDModelMemberInstance HostModelInstance
        {
            get
            {
                VDModelMemberInstance parent = this;
                while (parent.ParentMemberInstance != null)
                {
                    parent = parent.ParentMemberInstance;
                }
                return parent;
            }
        }

        public List<VDModelMemberInstance> GetAllSubMemberInstances()
        {
            List<VDModelMemberInstance> instList = new List<VDModelMemberInstance>();
            getChildMemberLists(instList, this);
            return instList;
        }

        public List<VDModelMemberInstance> GetAllMemberInstances()
        {
            List<VDModelMemberInstance> instList = new List<VDModelMemberInstance>();
            instList.Add(this);
            getChildMemberLists(instList, this);
            return instList;
        }

        private void getChildMemberLists(List<VDModelMemberInstance> instList, VDModelMemberInstance parentInst)
        {
            foreach (var child in parentInst.ChildMemberInstances)
            {
                instList.Add(child);
                getChildMemberLists(instList, child);
            }
        }

        // to support object list view
        public Guid ParentID
        {
            get
            {
                if (this.ParentMemberInstance != null)
                {
                    // todo: remvoe it in the future, it only works for view model
                    if (this is VDViewModelMemberInstance && this.ParentMemberInstance.ParentMemberInstance == null) return Guid.Empty;

                    return this.ParentMemberInstance.Id;
                }
                else
                    return Guid.Empty;
            }
        }
    }

    public partial class VDViewModelMemberInstance
    {
        // custom storage property
        internal string GetDefaultValueValue()
        {
            if (this.ModelMemberInfo != null)
                return this.ModelMemberInfo.DefaultValue;
            else
                return string.Empty;
        }

        internal void SetDefaultValueValue(string newValue)
        {
            if (this.ModelMemberInfo != null)
                this.ModelMemberInfo.DefaultValue = newValue;
        }

        // to support Object List View data binding
        public bool IsJavaScriptModel_OLV
        {
            get { return this.IsJavaScriptModel; }
            set 
            { 
                using(var trans = this.Store.TransactionManager.BeginTransaction("Update IsJavaScriptModel property"))
                {
                    this.IsJavaScriptModel = value;
                    trans.Commit();
                }
            }
        }
    }

    public partial class VDWidgetModelMemberInstance
    {
        internal override string GetNameValue()
        {
//            if (this.Widget != null)
//                return this.Widget.WidgetName;
//            else
                return base.GetNameValue();
        }

        // referred model member instance full name
        public string ReferenceName
        {
            get
            {
                string name = string.Empty;
                if (this.Reference != null)
                {
                    name = this.Reference.Name;
                    VDModelMemberInstance mmInst = this.Reference.ParentMemberInstance;
                    while (mmInst != null)
                    {
                        name = mmInst.Name ?? string.Empty + "/" + name;
                        mmInst = mmInst.ParentMemberInstance;
                    }
                }
                return name;
            }
        }
    }

    public partial class VDActionModelMemberInstance
    {
        // Selector calculated property
        public string GetSelectorValue()
        {
            StringBuilder sb = new StringBuilder();
            if (SelectorAnchor != null)
                sb.AppendFormat("{0}[{1}].{2}", SelectorAnchor.WidgetName ?? "", SelectorAnchor.WidgetType.ToString(), this.SelectorFunction ?? "");
            else
                sb.AppendFormat("{0}", this.SelectorFunction ?? "");

            if (!string.IsNullOrEmpty(this.ResolvedTargetWidget))
                sb.AppendFormat(" => {0}", this.ResolvedTargetWidget);

            if (sb.Length > 0)
                return sb.ToString();
            else
                return Utility.Constants.STR_NOT_SPECIFIED;
        }
    }
#endregion


#region "Model Type"
    public partial class VDModelType2
    {
        // IsReadOnly property
        protected bool m_bIsReadOnly = false;
        internal virtual bool GetIsReadOnlyValue() { return m_bIsReadOnly; }
        internal virtual void SetIsReadOnlyValue(bool newValue) { m_bIsReadOnly = newValue; }
        internal virtual string GetFullNameValue() 
        {  
            return string.IsNullOrEmpty(this.NameSpace) ? this.Name : this.NameSpace + "." + this.Name;
        }

        //
        public override int GetHashCode()
        {
            int code = 0;
            if (this.NameSpace != null)
                code += this.NameSpace.GetHashCode() * 1000;
            if (this.Name != null)
                code += this.Name.GetHashCode();
            return code;
        }

        public override bool Equals(object obj)
        {
            if (obj is VDModelType2)
            {
                VDModelType2 other = (VDModelType2)obj;
                if (string.Compare(this.FullName, other.FullName) == 0)
                    return true;
            }
            return false;
        }
    }

#if todel
    public partial class VDPredefinedType
    {
        // always read only
        internal override bool GetIsReadOnlyValue() { return true; }
        internal override void SetIsReadOnlyValue(bool newValue) { }
    }
#endif

    public partial class VDDictionaryType
    {
        // always read only, can not add new members
        internal override bool GetIsReadOnlyValue() { return true; }
        internal override void SetIsReadOnlyValue(bool newValue) { }

        internal override string GetFullNameValue()
        {
            return GetDictionaryFullName(
                this.KeyInfo == null || this.KeyInfo.Type == null ? string.Empty : this.KeyInfo.Type.FullName,
                this.ValueInfo == null || this.ValueInfo.Type == null ? string.Empty : this.ValueInfo.Type.FullName);
        }

        public static string GetDictionaryFullName(string keyTypeFullName, string valueTypeFullName)
        {
            return string.Format(Utility.Constants.STR_TYPE_DICTIONARY, 
                keyTypeFullName ?? string.Empty, 
                valueTypeFullName ?? string.Empty);
        }
    }

    public partial class VDListType
    {
        // always read only, can not add new members
        internal override bool GetIsReadOnlyValue() { return true; }
        internal override void SetIsReadOnlyValue(bool newValue) { }

        internal override string GetFullNameValue()
        {
            return GetListFullName(
                this.ValueInfo == null || this.ValueInfo.Type == null ? string.Empty
                : this.ValueInfo.Type.FullName);
        }

        public static string GetListFullName(string valueTypeFullName)
        {
            return string.Format(Utility.Constants.STR_TYPE_LIST, valueTypeFullName ?? string.Empty);
        }
    }
#endregion

}
#endif

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
            //VDWidgetValue widgetValue = GetWidgetValue(metaType.FullName);
            //if (widgetValue != null) return widgetValue;

            T widgetValue = newConcreteType<T>();
            widgetValue.Meta = metaType;
            this.ConcreteTypes.Add(widgetValue);

            // avoid recursive infinitely
            if (memberOfThisType != null)
            {
                bool usedTypeInParent = false;
                VDConcreteType hostType = memberOfThisType.Host as VDConcreteType;
                while (hostType != null)
                {
                    if (hostType.FullName == widgetValue.FullName)
                    {
                        usedTypeInParent = true;
                        break;
                    }
                    hostType = hostType.HostType;
                }
                if (usedTypeInParent) return widgetValue;
            }

            // add members
            foreach (VDModelMember m in metaType.Members)
            {
                VDMetaMember metaMember = m as VDMetaMember;
                if (metaMember == null) continue;

                VDConcreteMember newMember = newConcreteMember<T>();
                newMember.Meta = metaMember;
                if (metaMember.Type is VDPrimitiveType)
                {
                    newMember.Type = GetPrimitiveMemberType(metaMember.Type.FullName);
                }
                else
                {
                    newMember.Type = this.CreateConcreteType<T>(metaMember.Type as VDMetaType, newMember);
                }
                widgetValue.Members.Add(newMember);
            }

            return widgetValue;
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

        private VDConcreteMember newConcreteMember<T>() where T : VDConcreteType
        {
            if (typeof(T) == typeof(VDWidgetValue))
                return new VDWidgetValueMember(this.Partition);
            else if (typeof(T) == typeof(VDActionData))
                return new VDActionDataMember(this.Partition);
            else if (typeof(T) == typeof(VDViewModel))
                return new VDViewModelMember(this.Partition);
            else
                return null;
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
                newMember.Type = this.ModelStore.CreateConcreteType<VDWidgetValue>(metaMember.Type as VDMetaType);
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
                member.Type = this.ModelStore.CreateConcreteType<VDWidgetValue>(memberMetaType);
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

        virtual protected internal VDConcreteMember newConcreteMember() { return null; }
    }

    public partial class VDWidgetValue : VDConcreteType
    {
        protected internal override VDConcreteMember newConcreteMember()
        {
            return new VDWidgetValueMember(this.Partition);
        }
    }

    public partial class VDActionData : VDConcreteType
    {
        protected internal override VDConcreteMember newConcreteMember()
        {
            return new VDActionDataMember(this.Partition);
        }
    }

    public partial class VDViewModel : VDConcreteType
    {
        protected internal override VDConcreteMember newConcreteMember()
        {
            return new VDViewModelMember(this.Partition);
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
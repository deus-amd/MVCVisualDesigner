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
        public VDModelType GetModelType(string fullTypeName)
        {
            if (string.IsNullOrEmpty(fullTypeName)) return null;

            foreach(var modelType in this.ModelTypes)
            {
                if (string.Compare(modelType.FullName, fullTypeName) == 0)
                    return modelType;
            }
            return null;
        }

        public VDModelType GetModelType(string typeName, string nameSpace)
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

        public VDModelType CreateModelType(ModelTypeValue modelType, HashSet<string> predefinedTypes)
        {
            if (modelType.CollectionType == E_CollectionType.Not_Collection)
                return CreateModelType(modelType.ValueType, predefinedTypes);
            else if (modelType.CollectionType == E_CollectionType.Dictionary)
                return CreateDictionaryModelType(modelType.KeyType, modelType.ValueType, predefinedTypes);
            else if (modelType.CollectionType == E_CollectionType.List)
                return CreateListModelType(modelType.ValueType, predefinedTypes);

            return null;
        }

        public VDModelType CreateModelType(string fullTypeName, HashSet<string> predefinedTypes)
        {
            VDModelType type = GetModelType(fullTypeName);
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
                new PropertyAssignment(VDModelType.NameDomainPropertyId, typeName),
                new PropertyAssignment(VDModelType.NameSpaceDomainPropertyId, nameSpace)) as VDModelType;
            this.ModelTypes.Add(type);

            return type;
        }

        public VDModelType CreateModelType(string typeName, string nameSpace, HashSet<string> predefinedTypes)
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
                new PropertyAssignment(VDModelType.NameDomainPropertyId, dictFullName),
                new PropertyAssignment(VDModelType.NameSpaceDomainPropertyId, string.Empty)) as VDDictionaryType;
            this.ModelTypes.Add(dictType);

            // key member info
            VDModelType keyType = CreateModelType(keyTypeFullName, predefinedTypes);
            VDPlaceholderInfo keyMemInfo = this.Store.ElementFactory.CreateElement(
                    VDPlaceholderInfo.DomainClassId,
                    new PropertyAssignment(VDModelMemberInfo.NameDomainPropertyId, "Key")) as VDPlaceholderInfo;
            dictType.ModelMembers.Add(keyMemInfo);
            dictType.KeyInfo = keyMemInfo;
            keyMemInfo.Type = keyType;

            // value member info
            VDModelType valueType = CreateModelType(valueTypeFullName, predefinedTypes);
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
                new PropertyAssignment(VDModelType.NameDomainPropertyId, listFullName),
                new PropertyAssignment(VDModelType.NameSpaceDomainPropertyId, string.Empty)) as VDListType;
            this.ModelTypes.Add(listType);

            // value member info
            VDModelType valueType = CreateModelType(valueTypeFullName, predefinedTypes);
            VDPlaceholderInfo valueMemInfo = this.Store.ElementFactory.CreateElement(
                    VDPlaceholderInfo.DomainClassId,
                    new PropertyAssignment(VDModelMemberInfo.NameDomainPropertyId, "Value")) as VDPlaceholderInfo;
            listType.ModelMembers.Add(valueMemInfo);
            listType.ValueInfo = valueMemInfo;
            valueMemInfo.Type = valueType;

            return listType;
        }

        public VDModelInstance CreateModelInstance<TModelMemberInstance>(VDModelType modelType, string modelName) 
            where TModelMemberInstance : VDModelMemberInstance
        {
            if (modelType == null) return null;

            // create model instance
            VDModelInstance modelInstance = new VDModelInstance(this.Partition,
                new PropertyAssignment(VDModelInstance.ModelNameDomainPropertyId, modelName)) { ModelType = modelType };
            this.ModelMemberInstances.Add(modelInstance);
            this.ModelInstances.Add(modelInstance);

            // create model member instances according to model type definition
            Guid modelMemberInstanceDomainClassId = getDomainClassID<TModelMemberInstance>();
            createChildModelMemberInstances(modelInstance, modelMemberInstanceDomainClassId);

            return modelInstance;
        }

        private void createChildModelMemberInstances(VDModelMemberInstance parentModelMemberInstance, Guid modelMemberInstanceDomainClassId)
        {
            Stack<VDModelType> superTypes = new Stack<VDModelType>();
            var mmi = parentModelMemberInstance.ParentMemberInstance;
            while (mmi != null && mmi.GetModelType() != null)
            {
                superTypes.Push(mmi.GetModelType());
                mmi = mmi.ParentMemberInstance;
            }

            createChildModelMemberInstances(parentModelMemberInstance, modelMemberInstanceDomainClassId, superTypes);
        }

        private void createChildModelMemberInstances(VDModelMemberInstance parentModelMemberInstance, Guid modelMemberInstanceDomainClassId, 
                Stack<VDModelType> superTypes)
        {
            // to avoid infinite recursion, if type of a member is same as any super member's type, 
            // don't create child member instances for it
            VDModelType modelType = parentModelMemberInstance.GetModelType();
            if (modelType == null || superTypes.Contains(modelType)) return;
            superTypes.Push(modelType);

            foreach (VDModelMemberInfo modelMemberInfo in modelType.ModelMembers)
            {
                var modelMemberInstance = this.Store.ElementFactory.CreateElement(modelMemberInstanceDomainClassId) as VDModelMemberInstance;
                modelMemberInstance.ModelMemberInfo = modelMemberInfo;
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
            Stack<VDModelType> superTypes = new Stack<VDModelType>(); //the order is reverse, but doesn't matter
            var mmi = parentModelMemberInstance.ParentMemberInstance;
            while (mmi != null && mmi.GetModelType() != null)
            {
                superTypes.Push(mmi.GetModelType());
                mmi = mmi.ParentMemberInstance;
            }

            createModelMemberInstance(parentModelMemberInstance, newMemberInfo, modelMemberInstanceDomainClassId, superTypes);
        }

        private void createModelMemberInstance(VDModelMemberInstance parentModelMemberInstance, 
                    VDModelMemberInfo newMemberInfo,
                    Guid modelMemberInstanceDomainClassId, Stack<VDModelType> superTypes)
        {
            VDModelType parentType = parentModelMemberInstance.GetModelType();
            if (parentType == null || superTypes.Contains(parentType))
                return;
            superTypes.Push(parentType);

            VDModelMemberInstance newMemberInstance = this.Store.ElementFactory.CreateElement(
                        modelMemberInstanceDomainClassId) as VDModelMemberInstance;
            newMemberInstance.ModelMemberInfo = newMemberInfo;
            this.ModelMemberInstances.Add(newMemberInstance);
            parentModelMemberInstance.ChildMemberInstances.Add(newMemberInstance);

            createChildModelMemberInstances(newMemberInstance, modelMemberInstanceDomainClassId, superTypes);
        }

        // todo: check if all embedding and reference links are deleted
        public void DeleteModelInstance(VDModelInstance modelInstance)
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
                VDModelType modelType, string memberName, string memberTypeName, HashSet<string> predefinedTypes)
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
            VDModelType newMemberType = this.CreateModelType(memberTypeName, predefinedTypes);
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

        private List<VDModelMemberInstance> getModelMemberInstancesOfType(VDModelType modelType)
        {
            List<VDModelMemberInstance> mmi = new List<VDModelMemberInstance>();
            foreach (VDModelInstance modelInst in modelType.ModelInstancesOfThisType)
            {
                mmi.Add(modelInst);
            }

            foreach(VDModelMemberInfo minfo in modelType.MembersOfThisType)
            {
                foreach(VDModelMemberInstance minst in minfo.MemberInstancesOfThisType)
                {
                    mmi.Add(minst);
                }
            }
            return mmi;
        }

        // delete member from model type and all related mode instances and model instance members
        public void RemoveMemberFromModelType(VDModelType modelType, VDModelMemberInfo modelMemberInfoToRemove)
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
            VDModelType newMemberType = CreateModelType(newModelType, predefinedTypes);
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
            else if (typeof(T) == typeof(VDModelInstance))
                return VDModelInstance.DomainClassId;

            else if (typeof(T) == typeof(VDModelMemberInfo))
                return VDModelMemberInfo.DomainClassId;
            else if (typeof(T) == typeof(VDPropertyInfo))
                return VDPropertyInfo.DomainClassId;
            else if (typeof(T) == typeof(VDFieldInfo))
                return VDFieldInfo.DomainClassId;
            else if (typeof(T) == typeof(VDMethodInfo))
                return VDMethodInfo.DomainClassId;

            else if (typeof(T) == typeof(VDModelType))
                return VDModelType.DomainClassId;
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
        // calculated Name property
        internal virtual string GetNameValue()
        {
            if (this.ModelMemberInfo != null)
                return this.ModelMemberInfo.Name;
            else
                return string.Empty;
        }

        // calculated TypeName property
        internal virtual string GetTypeNameValue()
        {
            VDModelType type = GetModelType();
            if (type != null)
                return type.FullName;
            else
                return string.Empty;
        }

        public virtual VDModelType GetModelType()
        {
            if (this.ModelMemberInfo != null)
                return this.ModelMemberInfo.Type;
            else
                return null;
        }

        public VDModelInstance HostModelInstance
        {
            get
            {
                VDModelMemberInstance parent = this;
                while (parent != null && !(parent is VDModelInstance))
                {
                    parent = parent.ParentMemberInstance;
                }
                return parent as VDModelInstance;
            }
        }

        // to support object list view
        public Guid ParentID
        {
            get
            {
                if (this.ParentMemberInstance != null && !(this.ParentMemberInstance is VDModelInstance))
                    return this.ParentMemberInstance.Id;
                else
                    return Guid.Empty;
            }
        }

        public string BindingTargetName
        {
            get
            {
                string name = string.Empty; 
                if (this.BindingTarget != null)
                {
                    name = this.BindingTarget.Name;
                    VDModelMemberInstance mmInst = this.BindingTarget.ParentMemberInstance;
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

    }

    public partial class VDModelInstance
    {
        // calculated Name property
        internal override string GetNameValue() { return this.ModelName; }

        // calculated TypeName property
        //internal override string GetTypeNameValue() {}
        public override VDModelType GetModelType() { return this.ModelType; }

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
    }
#endregion


#region "Model Type"
    public partial class VDModelType
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
            if (obj is VDModelType)
            {
                VDModelType other = (VDModelType)obj;
                if (string.Compare(this.FullName, other.FullName) == 0)
                    return true;
            }
            return false;
        }
    }

    public partial class VDPredefinedType
    {
        // always read only
        internal override bool GetIsReadOnlyValue() { return true; }
        internal override void SetIsReadOnlyValue(bool newValue) { }
    }

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

    public enum E_CollectionType
    {
        Not_Collection = 0,
        Dictionary,
        List
    }

    public class ModelTypeValue
    {
        public ModelTypeValue()
        {
        }

        public ModelTypeValue(string typeName)
        {
            if (string.IsNullOrEmpty(typeName)) return;
            //todo: regex to parse the typename
            string[] tokens = typeName.Split(new char[] {' ', '<', ',', '>' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length > 0)
            {
                if (tokens.Length == 3 && tokens[0] == "Dictionary")
                {
                    CollectionType = E_CollectionType.Dictionary;
                    KeyType = tokens[0];
                    ValueType = tokens[1];

                }
                else if (tokens.Length == 2 && tokens[0] == "List")
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
                str = string.Format(Utility.Constants.STR_TYPE_DICTIONARY, this.KeyType, this.ValueType);
            else if (CollectionType == E_CollectionType.List)
                str = string.Format(Utility.Constants.STR_TYPE_LIST, this.ValueType);
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
}

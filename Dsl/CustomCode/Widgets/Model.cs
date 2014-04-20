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
        public VDModelType GetModelType(string typeName, string nameSpace = null)
        {
            if (string.IsNullOrEmpty(nameSpace))
            {
                int idx = typeName.LastIndexOf('.');
                if (idx > 0)
                {
                    nameSpace = typeName.Substring(0, idx);
                    typeName = typeName.Substring(idx + 1);
                }
            }

            foreach(var modelType in this.ModelTypes)
            {
                if (string.IsNullOrEmpty(nameSpace))
                {
                    if (string.Compare(modelType.Name, typeName) == 0 && string.IsNullOrEmpty(modelType.NameSpace))
                        return modelType;
                }
                else
                {
                    if (string.Compare(modelType.Name, typeName) == 0 && string.Compare(modelType.NameSpace, nameSpace) == 0)
                        return modelType;
                }
            }
            return null;
        }

        public VDModelType CreateModelType<TModelType>(string typeName, string nameSpace = null)
            where TModelType : VDModelType
        {
            if (string.IsNullOrEmpty(nameSpace))
            {
                int idx = typeName.LastIndexOf('.');
                if (idx > 0)
                {
                    nameSpace = typeName.Substring(0, idx);
                    typeName = typeName.Substring(idx + 1);
                }
            }

            VDModelType type = GetModelType(typeName, nameSpace);
            if (type != null) return type;

            Guid modelTypeID = getDomainClassID<TModelType>();
            type = this.Store.ElementFactory.CreateElement(modelTypeID, 
                new PropertyAssignment(VDModelType.NameDomainPropertyId, typeName),
                new PropertyAssignment(VDModelType.NameSpaceDomainPropertyId, nameSpace)) as VDModelType;
            this.ModelTypes.Add(type);

            return type;
        }

        public VDModelInstance CreateModelInstance<TModelMemberInstance>(VDModelType modelType, string modelName) 
            where TModelMemberInstance : VDModelMemberInstance
        {
            if (modelType == null) return null;

            // create model instance
            VDModelInstance modelInstance = new VDModelInstance(this.Partition,
                new PropertyAssignment(VDModelInstance.m_ModelInstanceNameDomainPropertyId, modelName)) { ModelType = modelType };
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
        public void AddMemberToModelType<TModelMemberInfo, TModelMemberType, TModelMemberInstance>(
                VDModelType modelType, string memberName, string memberTypeName, string memberTypeNameSpace = null
            )   where TModelMemberInfo : VDModelMemberInfo 
                where TModelMemberType : VDModelType
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
            VDModelType newMemberType = this.CreateModelType<TModelMemberType>(memberTypeName, memberTypeNameSpace);
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

        public void ChangeModelMemberType<TNewModelType, TModelMemberInstance>(
                VDModelMemberInfo modelMemberInfoToChange, 
                string newTypeName )
            where TNewModelType : VDModelType
            where TModelMemberInstance : VDModelMemberInstance
        {
            VDModelType newMemberType = CreateModelType<TNewModelType>(newTypeName);
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


    public partial class VDModelInstance
    {
        internal override string GetNameValue() { return this.m_ModelInstanceName; }

        public override VDModelType GetModelType() { return this.ModelType; }

        public List<VDModelMemberInstance> GetAllSubMemberInstances()
        {
            List<VDModelMemberInstance> instList = new List<VDModelMemberInstance>();
            //instList.Add(this);
            getChildMemberLists(instList, this);
            return instList;
        }

        private void getChildMemberLists(List<VDModelMemberInstance> instList, VDModelMemberInstance parentInst)
        {
            foreach(var child in parentInst.ChildMemberInstances)
            {
                instList.Add(child);
                getChildMemberLists(instList, child);
            }
        }
    }


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
        internal string GetTypeNameValue()
        {
            VDModelType type = GetModelType();
            if (type != null)
                return type.Name;
            else
                return string.Empty;
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
    }


    public partial class VDViewModelMemberInstance
    {
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


    public partial class VDModelType
    {
        // IsReadOnly property
        protected bool m_bIsReadOnly = false;
        internal virtual bool GetIsReadOnlyValue() { return m_bIsReadOnly; }
        internal virtual void SetIsReadOnlyValue(bool newValue) { m_bIsReadOnly = newValue; }

        //
        public string FullName
        {
            get
            {
                return string.IsNullOrEmpty(this.NameSpace) ? this.Name : this.NameSpace + "." + this.Name;
            }
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
                if (string.Compare(this.Name, other.Name) == 0 && string.Compare(this.NameSpace, other.NameSpace) == 0)
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
}

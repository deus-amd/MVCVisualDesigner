using Microsoft.VisualStudio.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    internal partial class MVCVisualDesignerDocData : MVCVisualDesignerDocDataBase
    {
        protected override void Load(string fileName, bool isReload)
        {
            base.Load(fileName, isReload);

            VDView view = this.RootElement as VDView;
            if (view != null)
            {
                if (view.ModelStore == null)
                {
                    view.ModelStore = new VDModelStore(this.Store);
                    initModelTypeList(view.ModelStore); // todo: load types from options ??
                }
            }
        }

        private void initModelTypeList(VDModelStore modelStore)
        {
            // todo: be sure "string" type always exists

            // init the type list
            modelStore.ModelTypes.Add(new VDSimpleType(modelStore.Partition,
                new PropertyAssignment(VDModelType.NameDomainPropertyId, "long")));
            modelStore.ModelTypes.Add(new VDSimpleType(modelStore.Partition,
                new PropertyAssignment(VDModelType.NameDomainPropertyId, Utility.Constants.STR_TYPE_INT)));
            modelStore.ModelTypes.Add(new VDSimpleType(modelStore.Partition,
                new PropertyAssignment(VDModelType.NameDomainPropertyId, "short")));
            modelStore.ModelTypes.Add(new VDSimpleType(modelStore.Partition,
                new PropertyAssignment(VDModelType.NameDomainPropertyId, "ulong")));
            modelStore.ModelTypes.Add(new VDSimpleType(modelStore.Partition,
                new PropertyAssignment(VDModelType.NameDomainPropertyId, "uint")));
            modelStore.ModelTypes.Add(new VDSimpleType(modelStore.Partition,
                new PropertyAssignment(VDModelType.NameDomainPropertyId, "ushort")));
            modelStore.ModelTypes.Add(new VDSimpleType(modelStore.Partition,
                new PropertyAssignment(VDModelType.NameDomainPropertyId, "byte")));
            modelStore.ModelTypes.Add(new VDSimpleType(modelStore.Partition,
                new PropertyAssignment(VDModelType.NameDomainPropertyId, "char")));
            modelStore.ModelTypes.Add(new VDSimpleType(modelStore.Partition,
                new PropertyAssignment(VDModelType.NameDomainPropertyId, Utility.Constants.STR_TYPE_STRING)));
            modelStore.ModelTypes.Add(new VDSimpleType(modelStore.Partition,
                new PropertyAssignment(VDModelType.NameDomainPropertyId, "DateTime")));
            modelStore.ModelTypes.Add(new VDSimpleType(modelStore.Partition,
                new PropertyAssignment(VDModelType.NameDomainPropertyId, "TimeSpan")));
        }
    }
}

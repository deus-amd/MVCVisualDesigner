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
                    initModelTypeList(view.ModelStore);
                }

                if (view.WidgetInfoManager == null)
                {
                    //todo: read from options
                    view.WidgetInfoManager = new WidgetInfoManager();
                }
            }
        }

        private void initModelTypeList(VDModelStore modelStore)
        {
#if todel
            // init the type list, be sure "string" type always exists, it's default type
            modelStore.ModelTypes.Add(new VDPredefinedType(modelStore.Partition,
                new PropertyAssignment(VDModelType2.NameDomainPropertyId, "string")));
#endif
        }
    }
}

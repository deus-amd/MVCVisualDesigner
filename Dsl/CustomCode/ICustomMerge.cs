﻿using Microsoft.VisualStudio.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCVisualDesigner
{
    public interface ICustomMerge
    {
        void MergeTo(VDWidget targetWidget, ElementGroup elementGroup);
    }
}

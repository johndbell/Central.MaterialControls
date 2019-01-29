using Central.MaterialControls.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Central.MaterialControls.iOS
{
    public class RendererInitializer
    {
        public static void Init()
        {
            BorderlessDatePickerRenderer.Init();
            BorderlessEntryRenderer.Init();
            BorderlessTimePickerRenderer.Init();
            BorderlessPickerRenderer.Init();
            BorderlessEditorRenderer.Init();
            MaterialButtonRenderer.Initialize();
        }
    }
}

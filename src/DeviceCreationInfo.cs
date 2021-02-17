using System;
using System.Collections.Generic;
using System.Text;
using FLY.Graphics;

namespace FLY
{
    public struct DeviceCreationInfo
    {
        public bool DebugMode;
        public SurfaceFormat SurfaceFormat;
        public int BackBufferWidth, BackBufferHeight;
        public DepthFormat DepthStencilFormat;
        public RenderTargetUsage RenderTargetUsage;
    }
}

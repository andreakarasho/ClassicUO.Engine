using System;
using System.Collections.Generic;
using System.Text;
using FLY.Mathematics;

namespace FLY.Graphics
{
    public class GraphicsDevice : GraphicsResource
    {
        private FNA3D.FNA3D_PresentationParameters _options;

        internal GraphicsDevice(IntPtr wndHandle, ref FNA3D.FNA3D_PresentationParameters options, bool debug)
        {
            Handle = FNA3D.FNA3D_CreateDevice(ref options, (byte) (debug ? 1 : 0));

            _options = options;
        }

        public void Clear(Color color)
        {
            Vector4 vec = color.ToVector4();
            FNA3D.FNA3D_Clear(Handle, ClearOptions.DepthBuffer | ClearOptions.Stencil | ClearOptions.Target, ref vec, 0, 0);
        }
        
        public void SwapBuffers()
        {
            SwapBuffers(_options.deviceWindowHandle);
        }

        public void SwapBuffers(IntPtr wnd)
        {
            FNA3D.FNA3D_SwapBuffers(Handle, IntPtr.Zero, IntPtr.Zero, wnd);
        }
    }
}

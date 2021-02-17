using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FLY.Graphics.Buffers;
using FLY.Mathematics;

namespace FLY.Graphics
{
    public class GraphicsDevice : GraphicsResource
    {
        private FNA3D.FNA3D_PresentationParameters _options;
        private FNA3D.FNA3D_Viewport _viewport;
        private FNA3D.FNA3D_BlendState _blendState;
        private FNA3D.FNA3D_RasterizerState _rasterizerState;
        private FNA3D.FNA3D_DepthStencilState _depthStencilState;
        private FNA3D.FNA3D_SamplerState _samplerState;
        private Rectangle _scissor;

        private readonly FLYWindow _window;

        
        internal GraphicsDevice(FLYWindow window, ref FNA3D.FNA3D_PresentationParameters options, bool debug)
        {
            _window = window;

            Handle = FNA3D.FNA3D_CreateDevice(ref options, (byte) (debug ? 1 : 0));

            options.deviceWindowHandle = window.Handle;
            _options = options;


            window.OnResize += () =>
            {
                AdaptViewportAndScissorOnWindowSize();
                Reset();
            };

            window.OnClose += () =>
            {
                Dispose();
            };

            AdaptViewportAndScissorOnWindowSize();
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

        public void Reset()
        {
            FNA3D.FNA3D_ResetBackbuffer(Handle, ref _options);
        }

        public void SetSamplerState()
        {
            
        }

        public void SetBlendState()
        {
            FNA3D.FNA3D_SetBlendState(Handle, ref _blendState);
        }

        public void SetDepthStencilState()
        {
            FNA3D.FNA3D_SetDepthStencilState(Handle, ref _depthStencilState);
        }

        public void SetRasterizerState()
        {
            FNA3D.FNA3D_ApplyRasterizerState(Handle, ref _rasterizerState);
        }

        public void SetScissor(Rectangle rect) => SetScissor(rect.X, rect.Y, rect.Width, rect.Height);

        public void SetScissor(int x, int y, int width, int height)
        {
            _scissor.X = x;
            _scissor.Y = y;
            _scissor.Width = width;
            _scissor.Height = height;

            FNA3D.FNA3D_SetScissorRect(Handle, ref _scissor);
        }

        public void SetViewport(int x, int y, int width, int height)
        {
            _viewport.x = x;
            _viewport.y = y;
            _viewport.w = width;
            _viewport.h = height;

            FNA3D.FNA3D_SetViewport(Handle, ref _viewport);
        }

        public void SetVertexBuffer()
        {

        }

        public VertexBuffer CreateVertexBuffer(bool isDynamic, BufferUsage usage, int sizeInBytes)
        {
            byte dyn = (byte) (isDynamic ? 1 : 0);

            VertexBuffer vb = new VertexBuffer(FNA3D.FNA3D_GenVertexBuffer(Handle, dyn, usage, sizeInBytes), sizeInBytes);

            return vb;
        }

        public void SetVertexBufferData<T>
        (
            in VertexBuffer vb,
            T[] data,
            int offsetInBytes,
            int startIndex, 
            int elementCount,
            int stride,
            SetDataOptions options
        ) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
           
            int elementSizeInBytes =
#if NETSTANDARD2_0
                Marshal.SizeOf<T>()
#else
                -1
#endif
                ;

            SetVertexBufferData(vb, handle.AddrOfPinnedObject() + (startIndex * elementSizeInBytes), offsetInBytes, elementSizeInBytes, options);
            
            handle.Free();
        }

        public void SetVertexBufferData
        (
            in VertexBuffer vb,
            IntPtr data,
            int offset,
            int length,
            SetDataOptions options
        )
        {
            FNA3D.FNA3D_SetVertexBufferData
            (
                Handle,
                vb.Handle,
                offset,
                data,
                length,
                1,
                1,
                options
            );
        }

        public IndexBuffer CreateIndexBuffer(IndexElementSize indexElementSize, bool isDynamic, BufferUsage usage, int count)
        {
            byte dyn = (byte)(isDynamic ? 1 : 0);
            int stride = indexElementSize == IndexElementSize.ThirtyTwoBits ? 4 : 2;

            IndexBuffer ib = new IndexBuffer(FNA3D.FNA3D_GenIndexBuffer(Handle, dyn, usage, count * stride), count);

            return ib;
        }

        public void SetIndexBufferData<T>
        (
            in IndexBuffer ib,
            T[] data,
            int offsetInBytes,
            int startIndex,
            int elementCount,
            SetDataOptions options
        ) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);

            int elementSizeInBytes =
#if NETSTANDARD2_0
                    Marshal.SizeOf<T>()
#else
                -1
#endif
                ;

            SetIndexBufferData(ib, handle.AddrOfPinnedObject() + (startIndex * elementSizeInBytes), offsetInBytes, elementCount * elementSizeInBytes, options);
            
            handle.Free();
        }

        public void SetIndexBufferData
        (
            in IndexBuffer ib, 
            IntPtr data,
            int offset,
            int length,
            SetDataOptions options
        )
        {
            FNA3D.FNA3D_SetIndexBufferData
            (
                Handle,
                ib.Handle,
                offset,
                data,
                length,
                options
            );
        }


        private void AdaptViewportAndScissorOnWindowSize()
        {
            int width = _window.Bounds.Width;
            int height = _window.Bounds.Height;

            _options.backBufferWidth = width;
            _options.backBufferHeight = height;

            SetScissor(0, 0, width, height);
            SetViewport(0, 0, width, height);
        }
    }
}

using System;
using System.Threading;
using FLY;
using FLY.Graphics;
using FLY.Mathematics;


namespace TestApp
{

    unsafe struct VertexPositionColor
    {
        public Vector3 Position;
        public Color Color;

        public static readonly int SIZE = sizeof(float) * 3 + sizeof(uint);
    }


    class Program
    {
        static void Main(string[] args)
        {
            FLY.FLY.Configure(Backends.Detect);


            FLYWindow window = FLY.FLY.SpawnNewWindow("title", SDL2.SDL.SDL_WINDOWPOS_CENTERED, SDL2.SDL.SDL_WINDOWPOS_CENTERED, 500, 500);
            FLYWindow window_2 = FLY.FLY.SpawnNewWindow("title 2", SDL2.SDL.SDL_WINDOWPOS_CENTERED, SDL2.SDL.SDL_WINDOWPOS_CENTERED, 500, 500);

            window.AllowResizing = true;

            window.Show();
            window_2.Show();

            window.OnResize += () => { FLY.FLY.LogInfo("Window 1 resized!"); };
            window_2.OnResize += () => { FLY.FLY.LogInfo("Window 2 resized!"); };

            DeviceCreationInfo creationInfo = new DeviceCreationInfo()
            {
                DebugMode = true,
                BackBufferWidth = window.Bounds.Width,
                BackBufferHeight = window.Bounds.Height,
                DepthStencilFormat = DepthFormat.Depth24Stencil8,
                RenderTargetUsage = RenderTargetUsage.DiscardContents,
                SurfaceFormat = SurfaceFormat.Color
            };

            GraphicsDevice device = FLY.FLY.CreateDevice(window, creationInfo);
            GraphicsDevice device2 = FLY.FLY.CreateDevice(window_2, creationInfo);


            
            var buffer = device.CreateVertexBuffer(false, BufferUsage.WriteOnly, VertexPositionColor.SIZE);

            Color[] colors = { Color.Red };

            device.SetVertexBufferData(buffer, colors, 0, 0, colors.Length, 1, SetDataOptions.None);

            while (window.IsRunning)
            {
                FLY.FLY.PollEvent();


                device.Clear(Color.DarkRed);

                device.SetViewport(20, 20, 50, 70);
                
                device.SwapBuffers();


                device2.Clear(Color.LimeGreen);

                device2.SwapBuffers();



                Thread.Sleep(1);
            }

            FLY.FLY.Quit();

        }
    }
}

using System;
using System.Threading;
using FLY;
using FLY.Graphics;
using FLY.Mathematics;


namespace TestApp
{
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

            GraphicsDevice device = FLY.FLY.CreateDevice(window);
            GraphicsDevice device2 = FLY.FLY.CreateDevice(window_2);

            while (window.IsRunning)
            {
                FLY.FLY.PollEvent();

                device.Clear( Color.DarkRed);
                
                device.SwapBuffers();

                device2.Clear(Color.LimeGreen);

                device2.SwapBuffers();

                Thread.Sleep(1);
            }

            FLY.FLY.Quit();

        }
    }
}

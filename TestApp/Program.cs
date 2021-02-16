using System;
using System.Threading;
using FLY;


namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FLY.FLY.Configure(Backends.D3D11);


            FLYWindow window = FLY.FLY.SpawnNewWindow("title", SDL2.SDL.SDL_WINDOWPOS_CENTERED, SDL2.SDL.SDL_WINDOWPOS_CENTERED, 500, 500);
            FLYWindow window_2 = FLY.FLY.SpawnNewWindow("title 2", SDL2.SDL.SDL_WINDOWPOS_CENTERED, SDL2.SDL.SDL_WINDOWPOS_CENTERED, 500, 500);

            window.AllowResizing = true;

            window.Show();
            window_2.Show();

            window.OnResize += () => { FLY.FLY.LogInfo("Window 1 resized!"); };
            window_2.OnResize += () => { FLY.FLY.LogInfo("Window 2 resized!"); };

            while (window.IsRunning)
            {
                FLY.FLY.PollEvent();

                Thread.Sleep(1);
            }

            FLY.FLY.Quit();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ClassicUO.Engine.Mathematics;
using SDL2;

namespace ClassicUO.Engine
{
    public unsafe class Window
    {
        private Rectangle _bounds;

        public Window(string title, int x, int y, int width, int height, Backends backend)
        {
            SDL.SDL_WindowFlags flags = (
                SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN |
                SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS |
                SDL.SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS
            ) | (SDL.SDL_WindowFlags)FNA3D.FNA3D_PrepareWindowAttributes();


            Handle = SDL.SDL_CreateWindow(title, x, y, width, height, flags);

            _bounds.X = x;
            _bounds.Y = y;
            _bounds.Width = width;
            _bounds.Height = height;
        }

        public Window(IntPtr handle)
        {
            Handle = handle;
        }


        public IntPtr Handle { get; }

        public ref Rectangle Bounds => ref _bounds;

        public bool IsRunning { get; private set; }


        public void Close()
        {
            if (IsRunning)
            {
                SDL.SDL_DestroyWindow(Handle);
                IsRunning = false;
            }
        }


        internal int ProcessEvent(ref SDL.SDL_Event e)
        {
            if (IsRunning /*&& e.window.windowID == SDL.SDL_GetWindowID(Handle)*/)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:

                        Close();

                        break;

                    case SDL.SDL_EventType.SDL_TEXTINPUT:
                        break;
                    case SDL.SDL_EventType.SDL_TEXTEDITING:
                        break;

                    case SDL.SDL_EventType.SDL_WINDOWEVENT:

                        switch (e.window.windowEvent)
                        {
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:
                                
                                Close();
                                
                                break;

                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED:
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MOVED:
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_ENTER:

                                SDL.SDL_DisableScreenSaver();

                                break;

                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:

                                SDL.SDL_EnableScreenSaver();

                                break;
                        }

                        break;
                }
            }

            return 0;
        }


    }
}

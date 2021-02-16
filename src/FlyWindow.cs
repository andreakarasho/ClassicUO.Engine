using System;
using System.Collections.Generic;
using System.Text;
using FLY.Mathematics;
using SDL2;

namespace FLY
{
    public unsafe class FLYWindow
    {
        private Rectangle _bounds;

        internal FLYWindow(string title, int x, int y, int width, int height)
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

            IsRunning = Handle != IntPtr.Zero;
        }

        internal FLYWindow(IntPtr handle)
        {
            Handle = handle;
        }


        public IntPtr Handle { get; }

        public ref Rectangle Bounds => ref _bounds;

        public bool IsRunning { get; private set; }


        public void Show()
        {
            if (IsRunning)
            {
                SDL.SDL_ShowWindow(Handle);
            }
        }

        public void Close()
        {
            if (IsRunning)
            {
                IsRunning = false;

                SDL.SDL_DestroyWindow(Handle);
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

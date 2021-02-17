using System;
using System.Collections.Generic;
using System.Text;
using FLY.Mathematics;
using SDL2;

namespace FLY
{
    public unsafe class FLYWindow
    {
        private bool _allowResizing;
        private Rectangle _bounds;

        internal FLYWindow(string title, int x, int y, int width, int height)
        {
            SDL.SDL_WindowFlags flags = 
            (
                SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN |
                SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS |
                SDL.SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS
            ) 
            | (SDL.SDL_WindowFlags)FNA3D.FNA3D_PrepareWindowAttributes();

            
            Handle = SDL.SDL_CreateWindow(title, x, y, width, height, flags);

            _bounds.X = x;
            _bounds.Y = y;
            _bounds.Width = width;
            _bounds.Height = height;

            IsRunning = Handle != IntPtr.Zero;
            _allowResizing = (flags & SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE) != 0;
        }

        internal FLYWindow(IntPtr handle)
        {
            Handle = handle;
        }


        public IntPtr Handle { get; }

        public ref Rectangle Bounds => ref _bounds;

        public bool IsRunning { get; private set; }

        public bool AllowResizing
        {
            get => _allowResizing;
            set
            {
                if (_allowResizing != value)
                {
                    _allowResizing = value;

                    SDL.SDL_SetWindowResizable(Handle, value ? SDL.SDL_bool.SDL_TRUE : SDL.SDL_bool.SDL_FALSE);
                }
            }
        }

        public bool IsFocused { get; private set; }




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

                        int size = 0;

                        fixed (byte* buffer = e.text.text)
                        {
                            byte* ptr = buffer;

                            for (; ptr != null && *ptr != '\0'; ptr++, size++)
                            {

                            }

                            char* buffAlloc = stackalloc char[size];

                            Encoding.UTF8.GetChars(buffer, 0, buffAlloc, size);

                            for (int i = 0; i < size; ++i)
                            {
                                OnTextInput?.Invoke(buffAlloc[i]);
                            }
                        }

                        break;
                    case SDL.SDL_EventType.SDL_TEXTEDITING:

                        size = 0;

                        fixed (byte* buffer = e.edit.text)
                        {
                            byte* ptr = buffer;

                            for (; ptr != null && *ptr != '\0'; ptr++, size++)
                            {

                            }

                            TextEditingEventArg arg = new TextEditingEventArg();

                            if (size > 0)
                            {
                                char* buffAlloc = stackalloc char[size];

                                Encoding.UTF8.GetChars(buffer, 0, buffAlloc, size);

                                string str = new string(buffAlloc, 0, size);

                                arg.Text = str;
                                arg.Start = e.edit.start;
                                arg.Length = e.edit.length;
                            }

                            OnTextEditing?.Invoke(arg);
                        }

                        break;

                    case SDL.SDL_EventType.SDL_WINDOWEVENT:

                        switch (e.window.windowEvent)
                        {
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:

                                OnClose?.Invoke();

                                Close();
                                
                                break;

                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:

                                IsFocused = true;

                                SDL.SDL_DisableScreenSaver();

                                OnFocusGain?.Invoke();

                                break;

                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:

                                IsFocused = false;

                                SDL.SDL_EnableScreenSaver();

                                OnFocusLost?.Invoke();

                                break;

                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:

                                _bounds.Width = e.window.data1;
                                _bounds.Height = e.window.data2;

                                break;

                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:

                                OnResize?.Invoke();

                                break;

                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED:
                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MOVED:

                                _bounds.X = e.window.data1;
                                _bounds.Y = e.window.data2;

                                OnMove?.Invoke();

                                break;
                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_ENTER:

                                SDL.SDL_DisableScreenSaver();

                                OnEnter?.Invoke();

                                break;

                            case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:

                                SDL.SDL_EnableScreenSaver();

                                OnLeave?.Invoke();

                                break;
                        }

                        break;
                }
            }

            return 0;
        }


        public event Action OnResize;
        public event Action OnShown;
        public event Action OnHide;
        public event Action OnMove;
        public event Action OnClose;
        public event Action OnFocusGain, OnFocusLost;
        public event Action OnEnter, OnLeave;
        public event Action<char> OnTextInput;
        public event Action<TextEditingEventArg> OnTextEditing;
        public event Action<DropEventArg> OnDataDrop;
    }


    public struct DropEventArg
    {

    }

    public struct TextEditingEventArg
    {
        public int Start, Length;
        public string Text;
    }
}

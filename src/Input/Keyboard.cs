using System;
using System.Collections.Generic;
using System.Text;

namespace FLY.Input
{
    public static class Keyboard
    {
        // TODO: osx behaviour?

        public static bool Ctrl => (SDL2.SDL.SDL_GetModState() & SDL2.SDL.SDL_Keymod.KMOD_CTRL) != 0;
        public static bool Alt => (SDL2.SDL.SDL_GetModState() & SDL2.SDL.SDL_Keymod.KMOD_ALT) != 0;
        public static bool Shift => (SDL2.SDL.SDL_GetModState() & SDL2.SDL.SDL_Keymod.KMOD_SHIFT) != 0;


        public static bool IsKeyPressed(SDL2.SDL.SDL_Keycode code)
        {
            return false;
        }
    }
}

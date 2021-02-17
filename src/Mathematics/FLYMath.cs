using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FLY.Mathematics
{
    public static class FLYMath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float min, float max)
        {
            value = (value > max) ? max : value;
            value = (value < min) ? min : value;

            return value;
        }
    }
}

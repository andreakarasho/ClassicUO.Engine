using System;
using System.Collections.Generic;
using System.Text;

namespace ClassicUO.Engine.Mathematics
{
    public struct Vector4
    {
        public float X, Y, Z, W;

        public Vector4(float x, float y, float z, float w) => (X, Y, Z, W) = (x, y, z, w);
    }
}

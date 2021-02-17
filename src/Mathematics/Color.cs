using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FLY.Mathematics
{
    public struct Color : IEquatable<Color>, IEquatable<uint>
    {
        public byte R, G, B, A;


        public Color(uint rgba)
        {
            R = (byte)rgba;
            G = (byte)(rgba >> 8);
            B = (byte)(rgba >> 16);
            A = (byte)(rgba >> 24);
        }

        public Color(byte r, byte g, byte b, byte a = 0xFF)
        {
            (R, G, B, A) = (r, g, b, a);
        }

        public Color(Vector3 vec) : this(vec.X, vec.Y, vec.Z, 1f)
        {

        }

        public Color(Vector4 vec) : this(vec.X, vec.Y, vec.Z, vec.W)
        {

        }

        public Color(float r, float g, float b, float a = 1f) 
        {
            R = (byte) FLYMath.Clamp(r * 255, byte.MinValue, byte.MaxValue);
            G = (byte) FLYMath.Clamp(g * 255, byte.MinValue, byte.MaxValue);
            B = (byte) FLYMath.Clamp(b * 255, byte.MinValue, byte.MaxValue);
            A = (byte) FLYMath.Clamp(a * 255, byte.MinValue, byte.MaxValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValue(uint rgba)
        {
            R = (byte) (rgba & 0xFF);
            G = (byte) ((rgba >> 8) & 0xFF);
            B = (byte) ((rgba >> 16) & 0xFF);
            A = (byte) ((rgba >> 24) & 0xFF);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetValue() => (uint) (R | (G << 8) | (B << 16) | (A << 24));


        public Vector4 ToVector4() => new Vector4(R / 255.0f, G / 255.0f, B / 255.0f, A / 255.0f);


        public bool Equals(Color other)
        {
            return (R, G, B, A) == (other.R, G, B, A);
        }

        public bool Equals(uint other)
        {
            return GetValue() == other;
        }


        public override string ToString()
        {
            return $"R: {R}, G: {G}, B: {B}, A: {A} - {GetValue()}";
        }


        public static implicit operator Color(uint val)
        {
            return new Color(val);
        }


        public static readonly Color Transparent    = 0x00_00_00_00;
        public static readonly Color White          = 0xFF_FF_FF_FF;
        public static readonly Color Black          = 0xFF_00_00_00;
        public static readonly Color AliceBlue = new Color(240, 248, 255);
        public static readonly Color CornflowerBlue = new Color(100, 149, 237);
        public static readonly Color Red = new Color(1f, 0f, 0f);
        public static readonly Color LimeGreen = 0xff32cd32;
        public static readonly Color DarkRed = 0xff00008b;
    }
}

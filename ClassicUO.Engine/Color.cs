using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ClassicUO.Engine.Graphics
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

        public Color(float r, float g, float b, float a) : this((byte) (r / 255.0f), (byte) (g / 255.0f), (byte) (b / 255.0f), (byte) (a = 255.0f))
        {

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValue(uint rgba)
        {
            R = (byte) rgba;
            G = (byte) (rgba >> 8);
            B = (byte) (rgba >> 16);
            A = (byte) (rgba >> 24);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetValue() => (uint) (R | (G << 8) | (B << 16) | (A << 24));



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
    }
}

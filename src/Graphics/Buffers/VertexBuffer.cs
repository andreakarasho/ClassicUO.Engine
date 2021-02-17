using System;
using System.Collections.Generic;
using System.Text;

namespace FLY.Graphics.Buffers
{
    public readonly struct VertexBuffer
    {
        public VertexBuffer(IntPtr handle, int size) => (Handle, Size) = (handle, size);

        public readonly IntPtr Handle;
        public readonly int Size;
    }
}

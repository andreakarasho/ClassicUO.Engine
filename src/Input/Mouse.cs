using System;
using System.Collections.Generic;
using System.Text;

namespace FLY.Input
{
    public static class Mouse
    {
        public const uint DOUBLE_CLICK_DELAY = 350;

        public static bool IsLeftPressed { get; private set; }
        public static bool IsMiddlePressed { get; private set; }
        public static bool IsRightPressed { get; private set; }
        public static bool IsDragging => IsLeftPressed || IsMiddlePressed || IsRightPressed;
    }
}

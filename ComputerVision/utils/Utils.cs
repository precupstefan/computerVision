﻿using System.Runtime;

namespace ComputerVision.utils
{
    public class Utils
    {
        public static int ClampByte(int value)
        {
            if (value < 0) return 0;
            if (value > 255) return 255;
            return value;
        }
    }
}
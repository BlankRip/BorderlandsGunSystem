using System.Collections;
using System.Collections.Generic;

namespace Blank
{
    public static class MyExtentions
    {
        //Sum up float array
        public static float Sum(this float[] arr)
        {
            float value = 0;
            foreach (float item in arr)
                value += item;
            return value;
        }
    }
}
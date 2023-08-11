using System.Collections;
using System.Collections.Generic;

namespace Blank
{
    public static class MyExtentions
    {
        /// Adds all values in the float array
        public static float Sum(this float[] arr)
        {
            float value = 0;
            foreach (float item in arr)
                value += item;
            return value;
        }

        /// Puts the string into the Clipboard.
        public static void CopyToClipboard(this string str) {
            UnityEngine.GUIUtility.systemCopyBuffer = str;
        }
    }
}
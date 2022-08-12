using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SEditor
{
    public static class HelperSE
    {
        public static int ToIntval(this string val)
        {
            if (val == null) return 0;
            if (val == "") return 0;
            val = val.Replace("&nbsp;", "");
            val = val.Replace(" ", "");
            val = val.Replace(" ", "");
            val = Regex.Replace(val, @"[^\d]+", "");

            if (val == "") return 0;

            return int.Parse(val);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MCoder.Libary;


namespace MCoder
{
    public static class MC_Colors
    {
        public static Color32 GetColorByType(MC_ArgumentTypeEnum eType)
        {
            Color32 col = new Color32(22, 33, 22, 255);

            if (eType == MC_ArgumentTypeEnum._int) col = new Color32(31, 80, 206, 255);
            if (eType == MC_ArgumentTypeEnum._player) col = new Color32(16, 115, 128, 255);
            if (eType == MC_ArgumentTypeEnum._body) col = new Color32(128, 124, 16, 255);
            if (eType == MC_ArgumentTypeEnum._string) col = new Color32(119, 31, 206, 255); 
            if (eType == MC_ArgumentTypeEnum._any) col = new Color32(119, 81, 116, 255); 

            return col;
        }

        public static Color32 GetColorByLinkType(MC_Value_LinkType linkType)
        {
            Color32 col = new Color32(31, 180, 46, 1);

            if (linkType == MC_Value_LinkType._event) col = new Color32(31, 80, 206, 255);
            if (linkType == MC_Value_LinkType._input) col = new Color32(31, 80, 206, 255);
            if (linkType == MC_Value_LinkType._custom) col = new Color32(31, 80, 206, 255);

            return col;
        }
    }
}
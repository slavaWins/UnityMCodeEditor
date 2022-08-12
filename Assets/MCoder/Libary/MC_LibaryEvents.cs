using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MCoder.Libary
{


    public class MC_Base_Event 
    {

        public List<MC_Argument> arguments = new List<MC_Argument>();
        public List<object> values = new List<object>();

        /// <summary>��������� ���� ����, ��� ����� �������</summary>
        public BodyTypeEnum bodyType { get; set; }
         
        public string title { get; set; }
        public string descr { get; set; }

        public string GetEventInd()
        {
         

            return this.GetType().ToString();
        }
        public virtual bool CallEvent(string _ind, object arg0=null, object arg1 = null)
        {
            if ((GetEventInd() != _ind)) return false;

            values[0] = arg0;
            values[1] = arg1;

            return true;
        }

    }


    public class MC_Event_InteractClick : MC_Base_Event
    {
        public MC_Event_InteractClick()
        {
            title = "����";
            descr = "����� ����� �� ���� ��� ����. ��������.";

            arguments = new List<MC_Argument>()
                {
                    new MC_Argument(){myType=MC_ArgumentTypeEnum._player, name = "Player"},
                };
        }
      
    }


    public class MC_Event_Hit : MC_Base_Event
    {
        public MC_Event_Hit()
        {
            title = "������������";
            descr = "����� ��� ��� ����� ���� ������";

            arguments = new List<MC_Argument>()
                {
                    new MC_Argument(){myType=MC_ArgumentTypeEnum._body, name = "BodyPlayerOrMob"},
                };
        }

    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MCoder.Libary
{


    public class ConsoleWrite_ActionNode : MC_BaseNodeElement, IMCoder_Function, IMCoder_NodeElement
    {
        public ConsoleWrite_ActionNode()
        {
            name = "Console.log";
            iconText = "LOG";
            descr = "�������� ��������� � �������";

            arguments = new List<MC_Argument>()
                {
                    new MC_Argument(){myType=MC_ArgumentTypeEnum._string, name = "debugText"},
                };
        }

        public override bool Call(object arg0 = null, object arg1 = null)
        {
            //  Console.WriteLine("ConsoleWrite_ActionNode");
            Debug.Log("~~~" + GetValueAsString(0));
            return true;
        }
    }



}
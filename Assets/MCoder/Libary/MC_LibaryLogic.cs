using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MCoder.Libary
{

    public class MC_NodeIfEnd : MC_BaseNodeElement, IMCoder_NodeElement
    {
        public MC_NodeIfEnd()
        {
            name = "END";
            iconText = "�����";
            descr = "����� ���������� �������� ����";
        }

        public bool Check()
        {
            return true;
        }
    }



}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using MCoder;
using MCoder.Libary;

namespace MCoder
{
    public static class MC_BD_Nodes
    {



        public static List<MC_Base_Event> GetEventsList()
        {
            List<MC_Base_Event> nodesList2 = new List<MC_Base_Event>();
            nodesList2.Add(new MC_Event_InteractClick()); 
            nodesList2.Add(new MC_Event_Hit()); 

            return nodesList2;
        }


        public static List<MC_BaseNodeElement> GetAllNodesList()
        {
            List<MC_BaseNodeElement> nodesList2 = new List<MC_BaseNodeElement>();
            nodesList2.Add(new MC_TrigerIfNoDie());
            nodesList2.Add(new DamageSelf_ActionNode());
            nodesList2.Add(new ConsoleWrite_ActionNode());
            nodesList2.Add(new MC_NodeIfEnd());

             
 
            return nodesList2;
        }

    }
}
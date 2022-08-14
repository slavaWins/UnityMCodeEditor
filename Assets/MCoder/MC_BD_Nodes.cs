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


        public static MC_BaseNodeElement GetLineByInd(string ind)
        {
            foreach (MC_BaseNodeElement item in GetAllNodesList())
            {
                if (item.GetType().ToString() == ind) return item;
            }
            return null;
        }


        //Создает НОВЫЙ класс по инд
        public static MC_Base_Event GetEventByInd(string ind)
        {
            foreach (MC_Base_Event item in GetEventsList())
            {
                if (item.GetEventInd() == ind) return item;
            }
            return null;
        }

        public static List<MC_Base_Event> GetEventsList()
        {
            List<MC_Base_Event> nodesList2 = new List<MC_Base_Event>();
            nodesList2.Add(new MC_Event_InteractClick()); 
            nodesList2.Add(new MC_Event_Hit()); 
            nodesList2.Add(new MC_Event_Spawn()); 
            nodesList2.Add(new MC_Event_Die()); 

            return nodesList2;
        }


        public static List<MC_BaseNodeElement> GetAllNodesList()
        {
            List<MC_BaseNodeElement> nodesList2 = new List<MC_BaseNodeElement>();
            nodesList2.Add(new MC_TwoVaribleEqual()); 
            nodesList2.Add(new MC_If_VaribleEquals_Any()); 
            nodesList2.Add(new MC_If_ItemBlock());
            nodesList2.Add(new MC_TrigerIfNoDie());
            nodesList2.Add(new DamageSelf_ActionNode());
            nodesList2.Add(new ConsoleWrite_ActionNode());
            nodesList2.Add(new MC_NodeIfEnd());

             
 
            return nodesList2;
        }

    }
}
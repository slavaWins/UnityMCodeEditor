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
    public class ExampleInstanceDamageIfClick : MC_BaseInstance
    {

        public ExampleInstanceDamageIfClick()
        {


            MC_NodeEventModule mC_Node = new MC_NodeEventModule(bodyType);

            mC_Node.myEvent = new MC_Event_InteractClick();



            //Добавляем ноду что моб ещё жив
            MC_TrigerIfNoDie nodeIfNodie = new MC_TrigerIfNoDie();
            mC_Node.AddNodesPackLogic(nodeIfNodie);


            //Добавляем ноду на дамаг себя
            DamageSelf_ActionNode nodeDamage = new DamageSelf_ActionNode();
            nodeDamage.values.Add(2);
            mC_Node.AddNodesPackLogic(nodeDamage);


            //Пишем в консоль что ударен
            ConsoleWrite_ActionNode nodeLog = new ConsoleWrite_ActionNode();
            nodeLog.values.Add("kick my asss");
            mC_Node.AddNodesPackLogic(nodeLog);


            //end
            MC_NodeIfEnd endIf = new MC_NodeIfEnd();
            mC_Node.AddNodesPackLogic(endIf);


            //Пишем в консоль что код законче
            nodeLog = new ConsoleWrite_ActionNode();
            //nodeLog.values.Add("last code log code!!");
            mC_Node.AddNodesPackLogic(nodeLog);


            nodesForEvents.Add(mC_Node);
        }

    }


    public class TestCode : MonoBehaviour
    {



        private void Start()
        {
            ExampleInstanceDamageIfClick testCodeForBody = new ExampleInstanceDamageIfClick();

            testCodeForBody.exampleBody = new ExampleBody();
            testCodeForBody.exampleBody.hp = 3;

 
            testCodeForBody.Init();

            if (testCodeForBody.issetError) return;

            Debug.Log("HP: " + testCodeForBody.exampleBody.hp);

            testCodeForBody.CallEvent("Click");
            Debug.Log("HP: " + testCodeForBody.exampleBody.hp);



            testCodeForBody.CallEvent("Click");
            Debug.Log("HP: " + testCodeForBody.exampleBody.hp);


            testCodeForBody.CallEvent("Click");
            Debug.Log("HP: " + testCodeForBody.exampleBody.hp);


        }
    }
}
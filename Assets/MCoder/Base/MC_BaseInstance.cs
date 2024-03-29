﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MCoder.Libary;


namespace MCoder
{
    [System.Serializable]
    public class MC_Value_SaveExport
    {

        public object val;
        public MC_Value_LinkType linkType;
        public int linkId;
    }

    [System.Serializable]
    public class MC_Save_EventModule_Node
    {
        public string ind;
        public List<MC_Value_SaveExport> values = new List<MC_Value_SaveExport>();
    }

    [System.Serializable]
    public class MC_Save_EventModule
    {
        
        public List<MC_Save_EventModule_Node> nodes = new List<MC_Save_EventModule_Node>();
    }


    [System.Serializable]
    public class MC_Save_Instance
    {
        public List<MC_Argument> argumentsSave = new List<MC_Argument>();
        public List<MC_Argument> argumentsCustoms = new List<MC_Argument>();
        public List<MC_Argument> argumentsInputs = new List<MC_Argument>();
        public Dictionary<string, MC_Save_EventModule> nodesForEvents = new Dictionary<string, MC_Save_EventModule>();

    }
    
    public static class MCoderExport
    {
        public static bool SetData(this MC_BaseInstance self, MC_Save_Instance toData)
        {

            self.argumentsSave = toData.argumentsSave;
            self.argumentsCustoms = toData.argumentsCustoms;
            self.argumentsInputs = toData.argumentsInputs;
            foreach (var modulesSave in toData.nodesForEvents)
            {
                MC_NodeEventModule moduleNode = new MC_NodeEventModule(BodyTypeEnum.block);
                moduleNode.myEvent = MC_BD_Nodes.GetEventByInd(modulesSave.Key);

                if (moduleNode.myEvent == null)
                {
                    Debug.Log("Не получилсоь подгрузить евент " + modulesSave.Key);
                    continue;
                }


                moduleNode.logicnodes = new List<MC_BaseNodeElement>();
                foreach (MC_Save_EventModule_Node nodesSave in modulesSave.Value.nodes)
                {
                    MC_BaseNodeElement node = MC_BD_Nodes.GetLineByInd(nodesSave.ind);
                    
                    foreach(var item in nodesSave.values)
                    {
                        MC_Value VALUE = new MC_Value();
                        VALUE.FromExport(item);
                        node.values.Add(VALUE);
                    }
                    moduleNode.logicnodes.Add(node);
                }


                self.nodesForEvents.Add(moduleNode);

            }

            return true;
        }

        public static MC_Save_Instance GetData(this MC_BaseInstance obj)
        {
            MC_Save_Instance save = new MC_Save_Instance();

            save.argumentsSave = obj.argumentsSave;
            save.argumentsCustoms = obj.argumentsCustoms;
            save.argumentsInputs = obj.argumentsInputs;

            foreach (MC_NodeEventModule moduleNode in obj.nodesForEvents)
            {
                MC_Save_EventModule module = new MC_Save_EventModule();
                

                for (int L = 0; L < moduleNode.logicnodes.Count; L++)
                {

                    MC_BaseNodeElement lnd = moduleNode.logicnodes[L];

                    MC_Save_EventModule_Node saveNode =  new MC_Save_EventModule_Node();
                    
                    saveNode.ind = lnd.GetType().ToString();
                    

                    foreach (var item in lnd.values)
                    {
                        saveNode.values.Add(item.ToExport());
                    }


                    module.nodes.Add(saveNode);
                }

                Debug.Log(module);
                save.nodesForEvents.Add(moduleNode.myEvent.GetEventInd(), module);
            }

            return save;
        }

    }



    /// 
    /// <summary>
    /// Это базовый инстанс. Это может быть блок, моб, игрок. В этом инстансе перечислины события. К каждому событию логический нод, ну скрипты кароч.
    /// </summary>
    public class MC_BaseInstance : IMCoder_Instance

    {
        public bool issetError = false;

        public ExampleBody exampleBody;
        public BodyTypeEnum bodyType { get; set; }


        public List<MC_Argument> argumentsCustoms = new List<MC_Argument>();
        public List<MC_Argument> argumentsInputs = new List<MC_Argument>();
        public List<MC_Argument> argumentsSave = new List<MC_Argument>();

        /// <summary>Список модулей инстанса. Модуль это скрипты из нодов, и событие при котором это всё вызывается</summary>
        public List<MC_NodeEventModule> nodesForEvents = new List<MC_NodeEventModule>();


        public bool IssetEvent(string eventInd)
        {
            foreach (MC_NodeEventModule moduleNode in nodesForEvents)
            {
                if (moduleNode.myEvent.GetEventInd() == eventInd) return true;
            }
            return false;
        }

        /// <summary>Вызываем какое-то событие, например Клик. И из списка nodesForEvents если есть клик, выполнится набор нодов </summary>
        public void CallEvent(string eventInd, object arg0 = null, object arg1 = null)
        {
            if (issetError) return;

            Debug.Log("=====");
            Debug.Log("e " + eventInd);
            // Debug.Log("nodeList count " + nodeList.Count);


            int l = 0;
            foreach (MC_NodeEventModule moduleNode in nodesForEvents)
            {
                l++;


                if (moduleNode.myEvent.CallEvent(eventInd))
                {
                    //  Debug.Log("call");
                    moduleNode.Call();

                }
            }
        }

        public class MC_Error
        {
            public string text = "xz";
            public int eventLin = -1;
            public int lineLogic = -1;
            public int agrumentNumber =-1;
            public MC_Error SelectArgument(int id)
            {
                agrumentNumber = id;
                return this;
            }
            public MC_Error(string t, int evLine=-1, int l=-1)
            {
                text = t;
                eventLin = evLine;
                lineLogic = l;
                agrumentNumber = -1;
            }
            public MC_Error()
            {
              
            }
        }

        public virtual MC_Error Validate()
        {

            if (exampleBody == null)
            {
                return new MC_Error("Не укзано тело для этого скрипта!", -1,-1);
            }


            MC_Error error = new MC_Error();


            

            int eventLine = -1;
            foreach (MC_NodeEventModule moduleNode in nodesForEvents)
            {
                eventLine++;

                string errrorModule = "\n In module " + moduleNode.GetType().ToString();

                string res = moduleNode.Validate();

                if (res != null)
                {
                    issetError = true;
                    return new MC_Error(res + errrorModule, eventLine, -1);
                }

                int L = -1;
                foreach (MC_BaseNodeElement lgn in moduleNode.logicnodes)
                {
                    L++;

                    string lineErorror = errrorModule + " \n In line " + L + "\n In node: " + lgn.name;


                    if (!lgn.IsSupportBodyType(bodyType))
                    {
                        return new MC_Error("Не полходит боди тайп" + lineErorror, eventLine, L); 
                    }


                    MC_Error _lineError  = lgn.Validate();
                    if (_lineError != null)
                    {
                        issetError = true;
                        error =   new MC_Error(_lineError.text + " " + res + lineErorror, eventLine, L);
                        error.agrumentNumber = _lineError.agrumentNumber;
                        return error;
                    }
                }
            }

            return null;
        }

        public virtual void Init()
        {
            foreach (MC_NodeEventModule moduleNode in nodesForEvents)
            {

                int j = 0;

                moduleNode.body = exampleBody;
                foreach (MC_BaseNodeElement lgn in moduleNode.logicnodes)
                {


                    j++;
                    lgn.parentModule = moduleNode;
                    lgn.mC_BaseInstance = this;
                    lgn.bodyType = bodyType;
                    lgn.exampleBody = exampleBody;
                    // Debug.Log(lgn.Validate());
                }

            }

            MC_Error res = Validate();


            if (res != null)
            {
                issetError = true;
                Debug.Log("~~~~"+res.text);
                return;
            }

        }

       
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MCoder.Libary;


namespace MCoder
{

    /// 
    /// <summary>
    /// Это базовый инстанс. Это может быть блок, моб, игрок. В этом инстансе перечислины события. К каждому событию логический нод, ну скрипты кароч.
    /// </summary>
    public class MC_BaseInstance : IMCoder_Instance

    {
        public bool issetError = false;

        public ExampleBody exampleBody;
        public BodyTypeEnum bodyType { get; set; }

        /// <summary>Список модулей инстанса. Модуль это скрипты из нодов, и событие при котором это всё вызывается</summary>
        public List<MC_NodeEventModule> nodesForEvents = new List<MC_NodeEventModule>();


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

        public virtual string Validate()
        {
            foreach (MC_NodeEventModule moduleNode in nodesForEvents)
            {
                string errrorModule = "\n In module " + moduleNode.GetType().ToString();

                string res = moduleNode.Validate();

                if (res != null)
                {
                    issetError = true;
                    return res + errrorModule;
                }

                int L = 0;
                foreach (IMCoder_NodeElement lgn in moduleNode.logicnodes)
                {
                    L++;

                    string lineErorror = errrorModule + " \n In line " + L + "\n In node: " + lgn.name;


                    if (lgn.bodyType != bodyType)
                    {
                        return "Не полходит боди тайп" + lineErorror;
                    }

                    res = lgn.Validate();
                    if (res != null)
                    {
                        issetError = true;
                        return res + lineErorror;
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


                foreach (IMCoder_NodeElement lgn in moduleNode.logicnodes)
                {


                    j++;

                    lgn.exampleBody = exampleBody;
                    // Debug.Log(lgn.Validate());
                }

            }

            string res = Validate();

            if (res != null)
            {
                issetError = true;
                Debug.LogError(res);
                return;
            }

        }

       
    }
}

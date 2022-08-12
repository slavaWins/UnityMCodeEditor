using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MCoder.Libary;
using System.Linq;

namespace MCoder
{
     
     
  

    public class MC_BaseNodeElement :  IMCoder_Function, IMCoder_NodeElement
    {

        public List<object> values { get; set; } = new List<object>();
        public List<MC_Argument> arguments { get; set; } = new List<MC_Argument>();
        public string name { get; set; } = null;   
        public string iconText { get; set; } = null;   
        public string descr { get; set; } = null;   
        public List<string> gavno { get; set; }
        public ExampleBody exampleBody { get; set; }
        public BodyTypeEnum bodyType { get; set; }

        public int GetValueAsInt(int argNumber)
        {

            if (arguments.Count - 1 < argNumber)
            {
                Debug.LogError(" Нет аргумента " + argNumber);
                return 0;
            }


            object val = values[argNumber];

            if (val is int)
            {
                return (int)val;
            }

            Debug.LogError(" GetValueAsInt не int");
            return 0;
        }

        public string GetValueAsString(int argNumber)
        {

            if (arguments.Count - 1 < argNumber)
            {
                Debug.LogError(" Нет аргумента " + argNumber);
                return "";
            }


            object val = values[argNumber];

            if (val is string)
            {
                return (string)val;
            }

            Debug.LogError(" GetValueAsInt не int");
            return "";
        }

        public virtual bool Call(object arg0 = null, object arg1 = null)
        {
           // Debug.Log("DEF CALL");
            return true;
        }

        public bool isType_END()
        {
            return (this is MC_NodeIfEnd);
        }

        public bool isType_IF()
        {
            return this.GetType().GetInterfaces().Contains(typeof(IMCoder_If));
        }

        public virtual string Validate()
        {
            if (name == null)return "У компонета нода нет названия! ComponentClass: " + this.GetType();
            if (iconText == null)return "У компонета нода нет текстовой иконки! ComponentClass: " + this.GetType();
            if (descr == null)return "У компонета нет описания! ComponentClass: " + this.GetType();
           

            
            if (arguments.Count > 0)
            {
                int i = 0;
                foreach(MC_Argument arg in arguments)
                {
                    i++;
                    if(values.Count<i)  return "У "+ name + "("+ this.GetType()+ ")" +" Не указан аргумент " + arg.name;
                }
            }

            if (arguments.Count > values.Count)
            {
                return "Не указан какой-то аргумент";
            }

            return null;
        }

    }

      


     

}
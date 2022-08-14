using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MCoder.Libary;
using System.Linq;
using static MCoder.MC_BaseInstance;

namespace MCoder
{

    public enum MC_Value_LinkType
    {
        _none,
        _event,
        _input,
        _custom

    }
    public class MC_Value
    {
        public object val;
        public MC_Value_LinkType linkType;

        /// <summary>
        /// Ид аргумент от нуля
        /// </summary>
        public int linkId;

        public MC_Value (object _val = null)
        {
            val = _val;
        }
        public MC_Value SetVal(object _val)
        {
            val = _val;
            return this;
        }
    }

    
  public interface IMC_SupportBodyType
    {

        public List<BodyTypeEnum> supportBodyType { get; set; }
        public bool IsSupportBodyType(BodyTypeEnum val);
    }

    public class MC_BaseNodeElement :  IMCoder_Function, IMC_SupportBodyType
    {
        public List<BodyTypeEnum> supportBodyType { get; set; } = new List<BodyTypeEnum>();

        public MC_NodeEventModule parentModule;
        public MC_BaseInstance mC_BaseInstance;
        public List<MC_Value> values { get; set; } = new List<MC_Value>();
        public List<MC_Argument> arguments { get; set; } = new List<MC_Argument>();
        public string name { get; set; } = null;   
        public string iconText { get; set; } = null;   
        public string descr { get; set; } = null;    
        public ExampleBody exampleBody { get; set; }
        public BodyTypeEnum bodyType { get; set; }


        public virtual bool Check() { return true; }

        public bool IsSupportBodyType(BodyTypeEnum val)
        {
            return supportBodyType.Contains(val);
        }

        public object GetValueAsObject(int argNumber)
        {

            if (arguments.Count - 1 < argNumber)
            {
                Debug.LogError(" Нет аргумента " + argNumber);
                return null;
            }

            object val = values[argNumber].val;

             
            return val;
        }

        public int GetValueAsInt(int argNumber)
        {

            if (arguments.Count - 1 < argNumber)
            {
                Debug.LogError(" Нет аргумента " + argNumber);
                return int.MinValue;
            }

            object val = values[argNumber].val;

            if (val is string)
            {
                int _valToint = -1;
                if (int.TryParse(val.ToString(), out _valToint))
                {
                    return _valToint;
                }
            }
             


            if (val is int)
            {
                return (int)val;
            }

            Debug.LogError(" GetValueAsInt не int a " + val.GetType().ToString());
            return int.MinValue;
        }

        public string GetValueAsString(int argNumber)
        {

            if (arguments.Count - 1 < argNumber)
            {
                Debug.LogError(" Нет аргумента " + argNumber);
                return "";
            }


            object val = values[argNumber].val;

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

        

        public virtual MC_Error Validate()
        {
            MC_Error error = new MC_Error();

            if (supportBodyType == null)return new MC_Error("У компонета не указаны поддерживаемые типы боди ! ComponentClass: " + this.GetType());
            if (name == null)return new MC_Error("У компонета нода нет названия! ComponentClass: " + this.GetType());
            if (iconText == null)return new MC_Error("У компонета нода нет текстовой иконки! ComponentClass: " + this.GetType());
            if (descr == null)return new MC_Error("У компонета нет описания! ComponentClass: " + this.GetType());
           

            
            if (arguments.Count > 0)
            {
                int i = -1;
                foreach (MC_Argument arg in arguments)
                {
                    i++;

                    if (values.Count - 1 < i)
                    {
                        error.text = "У " + name + "(" + this.GetType() + ")" + " Не указан аргумент " + arg.name;
                        error.agrumentNumber = i;
                        return error;
                    }

                    if (values[i].val.ToString() == null || values[i].val.ToString() == "")
                    {
                        if (values[i].linkType == MC_Value_LinkType._none)
                        {
                            return new MC_Error("Не указан аргумент, null в  " + arg.name).SelectArgument(i);
                        }
                    }

                    if (values[i].linkType != MC_Value_LinkType._none)
                    {
                        MC_Argument _ma = null;
                        if (values[i].linkType == MC_Value_LinkType._custom) _ma = mC_BaseInstance.argumentsCustoms[values[i].linkId];
                        if (values[i].linkType == MC_Value_LinkType._input) _ma = mC_BaseInstance.argumentsInputs[values[i].linkId];
                        if (values[i].linkType == MC_Value_LinkType._event) _ma = parentModule.myEvent.arguments[values[i].linkId];


                        if (_ma.myType != arg.myType && _ma.myType != MC_ArgumentTypeEnum._any && arg.myType != MC_ArgumentTypeEnum._any)
                        {
                            return new MC_Error("Не сходится тип " + values[i].linkType.ToString() + " переменной в  " + arg.name + ""
                                + ". Приходит " + _ma.myType.ToString() + " а нужно " + arg.myType).SelectArgument(i);
                        }
                    }

 
                    

                    if (arg.myType == MC_ArgumentTypeEnum._int)
                    {
                        if(GetValueAsInt(i)== int.MinValue)
                        {
                            return new MC_Error("Не подходит " + arg.name+ "! Должно быть целое число int!").SelectArgument(i);
                        }
                    }


                }
            }

            if (arguments.Count > values.Count)
            {
                return new MC_Error("Не указан какой-то аргумент");
            }

            return null;
        }

    }

      


     

}
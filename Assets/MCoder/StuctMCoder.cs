using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MCoder.Libary;
 

namespace MCoder
{
    public enum BodyTypeEnum
    {
        block,
        item,
        mob,
    }
    public enum MC_ArgumentTypeEnum
    {
        _int,
        _string,
        _body,
        _player,
    }

    public class MC_Argument
    {
        public string name = "varible";
        public MC_ArgumentTypeEnum myType;
    }


    public interface IMCoder_NodeElement
    {
        public List<MC_Argument> arguments { get; set; }
        public List<object> values { get; set; }

        public string iconText { get; set; } 
        public string descr { get; set; } 
        public string name { get; set; }


        public ExampleBody exampleBody { get; set; }
        public BodyTypeEnum bodyType { get; set; }
        public string Validate() { return null; }

        public bool isType_IF();
        public bool isType_END();

        public bool Check() { return true; }
        public bool Call(object arg0 = null, object arg1 = null);
    }


    public interface IMCoder_If
    { 
        public bool Check()
        {
            return false;
        }
    }

    public interface IMCoder_Instance
    {
        public BodyTypeEnum bodyType { get; set; }
        public void CallEvent(string eventInd, object arg0=null, object arg1=null);

    }

    public interface IMCoder_Function
    {
        public ExampleBody exampleBody { get; set; }
        public BodyTypeEnum bodyType { get; set; }
    }

    


    public class MC_NodeEventModule
    {
        public bool DEBUG_ALL_LINES_ENABLED = false;
        public ExampleBody body { get; set; }
        public BodyTypeEnum bodyType { get; set; }


        /// <summary>��� ������ �������. � ���� ������ ���� ��������� ���������</summary>
        public MC_Base_Event myEvent = new MC_Event_InteractClick();

        public List<IMCoder_NodeElement> logicnodes = new List< IMCoder_NodeElement>();

        public void AddNodesPackLogic(IMCoder_NodeElement element)
        {
            //Debug.Log(logicnodes.Count); 
          //  Debug.Log(logicnodes.Count + "] " + element.myCompName);
            logicnodes.Add(element);
        }

        public void Call()
        {

            //������� IF ������ ������ ������� false
            int blockedIfCount = 0;

            for(int L=0; L< logicnodes.Count; L++)
            { 

                IMCoder_NodeElement lnd = logicnodes[L];

                string _lineDebug = "LINE  " + L + " IMCoder_NodeElement:" + lnd.name;
                


                // END block
                //if (lnd is MC_NodeIfEnd)
                if (lnd.isType_END())
                { 
                   if(DEBUG_ALL_LINES_ENABLED) Debug.Log("END | " + _lineDebug);
                    if (blockedIfCount > 0)
                    {
                        blockedIfCount -= 1;
                        continue;
                    }
                }
                

                if (blockedIfCount > 0)
                {
                    if (DEBUG_ALL_LINES_ENABLED) Debug.Log("continue | " + _lineDebug);
                    continue;
                }

                //IF BLOCK
                //if (lnd.GetType().GetInterfaces().Contains(typeof(IMCoder_If)))
                if (lnd.isType_IF())
                {
                    if (!lnd.Check())
                    {
                        blockedIfCount++;
                        if (DEBUG_ALL_LINES_ENABLED) Debug.Log("IF not checked  | " + _lineDebug);
                        continue;
                    }

                    if (DEBUG_ALL_LINES_ENABLED) Debug.Log("IF ok | " + _lineDebug);
                    continue;
                }

                // IF block    
                if (!lnd.Check())
                {
                    if (DEBUG_ALL_LINES_ENABLED) Debug.Log("no check  | " + _lineDebug);
                    continue;
                }

                if (DEBUG_ALL_LINES_ENABLED) Debug.Log("call  | " + _lineDebug);
                lnd.Call();
            }
        }

        public string Validate()
        {
            int countEndIfOpen=0;
            int countIfOpen=0;
            int countelse=0;

            int i = 0;
            foreach(IMCoder_NodeElement lg in logicnodes)
            { 
                i++;

                if (lg.GetType().GetInterfaces().Contains(typeof(IMCoder_If))) countIfOpen += 1;

                if (lg is MC_NodeIfEnd)
                {
                    if (countIfOpen == 0)
                    {
                        return "� ������� " +i + " ������ ������ ����������� ������� end!"; 
                    }
                    countIfOpen -= 1;
                }
            }

            if (countEndIfOpen != countIfOpen) return "�� �������� ���� ���� if("+ countIfOpen+ ") � end(" + countEndIfOpen + ")";

            
           // Debug.Log("countEndIf " + countEndIfOpen);
           // Debug.Log("countIf " + countIfOpen);

            return null;
        }

        public MC_NodeEventModule(BodyTypeEnum bt)
        {
            if (bt != BodyTypeEnum.block)
            {
                Debug.LogError("�� �������� ��� ����");
            }
        }

    }

    /// <summary>��� ����� ����. ��������� ���� ���������. ���� �����, ����, ��������. ��� ������ �������� �� ��� ��������. </summary>
    public class ExampleBody
    {

        /// <summary>��� ��, ���� ���� ��������</summary>
        public int hp = 4;


        /// <summary>��� ��������� ����</summary>
        public Vector3 pos = new Vector3(0,0,0);
         


        /// <summary>������� ����</summary>
        public void TakeDamage(int val)
        {
            hp -= val;
            if (hp < 0) hp = 0;
        }

        /// <summary>������� ���������� ��������</summary>
        public void TakeCount(int val) { TakeDamage(val); }

    }





    

     

    public class MC_Logicnode
    {
        public IMCoder_If myIf;
        public List<IMCoder_NodeElement> logicnodes = new List<IMCoder_NodeElement>();

        public bool  Call()
        {
            foreach (IMCoder_NodeElement lnd in logicnodes)
            {
                if (!lnd.Check()) return false;

                bool _val=  lnd.Call();
                if (!_val) return false;
            }
            return false;
        }
    }

     
}
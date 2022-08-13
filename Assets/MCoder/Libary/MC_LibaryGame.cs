using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MCoder.MC_BaseInstance;

namespace MCoder.Libary
{



     
    public class DamageSelf_ActionNode : MC_BaseNodeElement, IMCoder_Function, IMCoder_NodeElement
    {
        public DamageSelf_ActionNode()
        {
            name = "Damage";
            iconText = "DMG";
            descr = "Нанести урон";

            supportBodyType = new List<BodyTypeEnum>() { BodyTypeEnum.item, BodyTypeEnum.mob, BodyTypeEnum.block };

            arguments = new List<MC_Argument>()
                {
                    new MC_Argument(){myType=MC_ArgumentTypeEnum._int, name = "damageAmount"},
                };
        }


        public override MC_Error Validate()
        {
            MC_Error res = base.Validate();
            if (res != null) return res;

            if (exampleBody == null) return new MC_Error("Не указано тело");
            return null;
        }

        public override bool Call(object arg0 = null, object arg1 = null)
        {
            if (exampleBody.hp <= 0)
            {
                Debug.Log("HP 0!!!!!");
                return false;
            }

            exampleBody.hp -= GetValueAsInt(0);
            return true;
        }
    }




    //Если есть хп
    public class MC_TrigerIfNoDie : MC_BaseNodeElement, IMCoder_If, IMCoder_NodeElement
    {
        
        public MC_TrigerIfNoDie()
        {
            name = "IF";
            iconText = "ЕСЛИ";
            descr = "Если блок не умер. Ещё есть ХП";
            supportBodyType = new List<BodyTypeEnum>() { BodyTypeEnum.item, BodyTypeEnum.mob, BodyTypeEnum.block };
        }

        public bool Check()
        {
            return exampleBody.hp > 0;
        }
    }

}
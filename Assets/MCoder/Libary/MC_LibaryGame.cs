using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MCoder.MC_BaseInstance;

namespace MCoder.Libary
{



     
    public class DamageSelf_ActionNode : MC_BaseNodeElement, IMCoder_Function
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




 
    public class MC_If_ItemBlock : MC_BaseNodeElement, IMCoder_If
    {
        
        public MC_If_ItemBlock()
        {
            name = "Если предмет является блоком";
            iconText = "ЕСЛИ";
            descr = "Если предмет является блоком";
            supportBodyType = new List<BodyTypeEnum>() { BodyTypeEnum.item };
        }

        public bool Check()
        {
            return exampleBody.isBlock ;
        }
    }


    //Если есть хп
    public class MC_TrigerIfNoDie : MC_BaseNodeElement, IMCoder_If
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

    public class MC_If_VaribleEquals_Any : MC_BaseNodeElement, IMCoder_If
    {

        public MC_If_VaribleEquals_Any()
        {
            arguments = new List<MC_Argument>()
                {
                    new MC_Argument(){myType=MC_ArgumentTypeEnum._any, name = "One"},
                    new MC_Argument(){myType=MC_ArgumentTypeEnum._any, name = "Two"},
                };

            name = "Если";
            iconText = "==";
            descr = "Если переменнная ANY равна чему-то";
            supportBodyType = new List<BodyTypeEnum>() { BodyTypeEnum.item, BodyTypeEnum.mob, BodyTypeEnum.block };
        }

        public bool Check()
        {
            return GetValueAsObject(0) == GetValueAsObject(1);
        }
    }
    public class MC_TwoVaribleEqual : MC_BaseNodeElement, IMCoder_If
    {

        public MC_TwoVaribleEqual()
        {
            arguments = new List<MC_Argument>()
                {
                    new MC_Argument(){myType=MC_ArgumentTypeEnum._any, name = "One"},
                    new MC_Argument(){myType=MC_ArgumentTypeEnum._any, name = "Two"},
                };

            name = "=";
            iconText = "=";
            descr = "Приравнять переменные";
            supportBodyType = new List<BodyTypeEnum>() { BodyTypeEnum.item, BodyTypeEnum.mob, BodyTypeEnum.block };
        }
        public override MC_Error Validate()
        {
            MC_Error error =  base.Validate();
            if (error != null) return error;

            if (values[0].linkType != MC_Value_LinkType._custom && values[0].linkType != MC_Value_LinkType._save) return new MC_Error("Можно менять только custom и save переменные!").SelectArgument(0);
            return null;
        }

        public override bool Check()
        {
            if (values[0].linkType != MC_Value_LinkType._custom && values[0].linkType != MC_Value_LinkType._save) return false;
            return true;
        }

        public override bool Call(object arg0 = null, object arg1 = null)
        {
            Debug.Log("это не точно! Возможно хуйню спорол");
            values[0].val = GetValueAsObject(1);
            return true;
        }
    }
     

}
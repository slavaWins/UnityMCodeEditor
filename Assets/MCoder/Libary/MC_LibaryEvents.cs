using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MCoder.Libary
{


    public class MC_Base_Event : IMC_SupportBodyType
    {
        public List<BodyTypeEnum> supportBodyType { get; set; } = new List<BodyTypeEnum>();

        public List<MC_Argument> arguments = new List<MC_Argument>();
        public List<object> values = new List<object>();

        /// <summary>Требуемый боди тайп, для этого события</summary>
        public BodyTypeEnum bodyType { get; set; }
         
        public string title { get; set; }
        public string descr { get; set; }


        public bool IsSupportBodyType(BodyTypeEnum val)
        {
            return supportBodyType.Contains(val);
        }

        public string GetEventInd()
        {
         

            return this.GetType().ToString();
        }
        public virtual bool CallEvent(string _ind, object arg0=null, object arg1 = null)
        {
            if ((GetEventInd() != _ind)) return false;

            values[0] = arg0;
            values[1] = arg1;

            return true;
        }

    }


    public class MC_Event_InteractClick : MC_Base_Event
    {
        public MC_Event_InteractClick()
        {
            title = "Клик";
            descr = "Игрок нажал на блок или моба. Интеракт.";

            supportBodyType = new List<BodyTypeEnum>() { BodyTypeEnum.item, BodyTypeEnum.mob, BodyTypeEnum.block };
            arguments = new List<MC_Argument>()
                {
                    new MC_Argument(){myType=MC_ArgumentTypeEnum._player, name = "Player"},
                };
        }
      
    }


    public class MC_Event_Hit : MC_Base_Event
    {
        public MC_Event_Hit()
        {
            title = "Столкновение";
            descr = "Игрок или моб задел этот объект";

            supportBodyType = new List<BodyTypeEnum>() { BodyTypeEnum.mob, BodyTypeEnum.block };
            arguments = new List<MC_Argument>()
                {
                    new MC_Argument(){myType=MC_ArgumentTypeEnum._body, name = "BodyPlayerOrMob"},
                };
        }

    }


    public class MC_Event_Spawn : MC_Base_Event
    {
        public MC_Event_Spawn()
        {
            title = "Spawn.";
            descr = "Объект только что заспавнился на карте";

            supportBodyType = new List<BodyTypeEnum>() { BodyTypeEnum.mob, BodyTypeEnum.block, BodyTypeEnum.item };

        }

    }


    public class MC_Event_Die : MC_Base_Event
    {
        public MC_Event_Die()
        {
            title = "Объект умер";
            descr = "После смерти, перед самым удалением вызывается это события. Когда сдох кароч.";

            supportBodyType = new List<BodyTypeEnum>() { BodyTypeEnum.mob, BodyTypeEnum.block, BodyTypeEnum.item };

        }

    }

 

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SEditor;


namespace SEditor
{
    public class ExampleGavnoStruct
    {
        public string ind = "xztext";
        public int ival = 76;
        public double ifloat = 76.35f;
        public bool vkl = true;
        public bool vikl = false;
        public int dropx = 2;
        public string textureid = "low";
    }

    public class ExampleForm : FormBuilder
    {

        public Sprite spriteExample;

        public ExampleGavnoStruct FromTemplate()
        {
            ExampleGavnoStruct data = new ExampleGavnoStruct();
            data.vkl = template["vkl"].valueBool;
            data.vikl = template["vikl"].valueBool;
            data.ival = template["ival"].valueInt;
            data.ifloat = template["ifloat"].valueDouble;
            data.ind = template["ind"].valueString;
            data.dropx = template["dropx"].valueInt;
            data.textureid = template["textureid"].valueString;
            return data;
        }


        public void ToTemplate(ExampleGavnoStruct data)
        {
            template.Clear();

            AddTemplateElement("x", new RowElementType()
            {
                label = "Заголовок 1",
                mytype = InputDataTypeEnum.Title,
            });


            AddTemplateElement("ind", new RowElementType()
            {
                label = "ind stte",
                mytype = InputDataTypeEnum._string,
                valueString = data.ind
            });

            AddTemplateElement("ival", new RowElementType()
            {
                label = "ival valasf ",
                mytype = InputDataTypeEnum._int,
                valueInt = data.ival
            });

            AddTemplateElement("ifloat", new RowElementType()
            {
                label = "ФЛоут состо valasf ",
                mytype = InputDataTypeEnum._float,
                valueDouble = data.ifloat

            });


            AddTemplateElement("vkl", new RowElementType()
            {
                label = "vkl booool ",
                mytype = InputDataTypeEnum._bool,
                valueBool = data.vkl

            });


            AddTemplateElement("vikl", new RowElementType()
            {
                label = "Блок может быть уничтожен ",
                mytype = InputDataTypeEnum._bool,
                valueBool = data.vikl

            });

            AddTemplateElement("x2", new RowElementType()
            {
                label = "Заголовок 2",
                mytype = InputDataTypeEnum.Title,
            });

            AddTemplateElement("dropx", new RowElementType()
            {
                label = "Блок может быть уничтожен ",
                mytype = InputDataTypeEnum._dropdown,
                valueInt = data.dropx,
                //options = {"xz","twoSelect","all"}
                options = new List<string>() { "xz", "twoSelect", "all" }

            });


            Dictionary<string, Sprite> listImgs = new Dictionary<string, Sprite>();
            listImgs.Add("good", null);
            listImgs.Add("low", spriteExample);
            listImgs.Add("top", spriteExample);
            listImgs.Add("ultra top", spriteExample);
            AddTemplateElement("textureid", new RowElementType()
            {
                label = "Текстура блока",
                mytype = InputDataTypeEnum._selectBig,
                valueString = data.textureid,
                optionsSelectBig = listImgs
            });


        }

        void Start()
        {
            Init();
            ClearAllChildren();
            ToTemplate(new ExampleGavnoStruct());
            TemplateCreate();
        }


        void Update()
        {
            TemplateRead();
            ExampleGavnoStruct data = FromTemplate();
            Debug.Log("=====");
            Debug.Log("ind:" + data.ind);
            Debug.Log("ival:" + data.ival);
            Debug.Log("ifloat:" + data.ifloat);
            Debug.Log("vkl:" + data.vkl.ToString());
            Debug.Log("vikl:" + data.vikl.ToString());
            Debug.Log("drop:" + data.dropx.ToString());
            Debug.Log("textureid:" + data.textureid.ToString());
        }
    }
}
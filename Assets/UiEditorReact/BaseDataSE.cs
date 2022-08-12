using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SEditor;

namespace SEditor
{
    [CreateAssetMenu(fileName = "SE_Base", menuName = "Se GUI/New SE Base")]
    public class BaseDataSE : ScriptableObject
    {

        public static BaseDataSE Get()
        {
            BaseDataSE _bd = Resources.Load<BaseDataSE>("SE_Base");
            if (_bd == null)
            {
                Debug.Log("Ошибка поиска компонента!");
                return null;
            }
            return _bd;
        }

        public CheckboxComponent_SE checkboxComponent;
        public InputTextComponent_SE inputComponent;
        public InputVextor2Component_SE inputVector2;
        public InputDropdownComponent_SE dropdownComponent;
        public Transform rowComponent;
        public WindowSelectBigCanvas_SE windowSelectBigCanvasPrefab;
        public inputBigSelect_SE inputBigSelect;
        public GameObject titleComponent;
        public PanelSeController panelSeController;
        public CanvasEditor_SE canvasEditor;
        public TabRow_SE tabRow_SE;
        public ElementTabRow_SE elementTabRow_SE;

        private WindowSelectBigCanvas_SE _curentCanvas;
        public WindowSelectBigCanvas_SE GetWindowSeletor()
        {
            if (_curentCanvas != null) return _curentCanvas;

            GameObject go = GameObject.Find("CanvasInputSelect");
            if (!go)
            {
                go = Instantiate(windowSelectBigCanvasPrefab.gameObject);
                go.gameObject.SetActive(false);
            }
            _curentCanvas = go.GetComponent<WindowSelectBigCanvas_SE>();
            return _curentCanvas;
        }
    }

}
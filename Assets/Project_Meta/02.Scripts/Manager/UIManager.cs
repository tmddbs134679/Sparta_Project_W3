using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Meta
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
       private Dictionary<EUISTATE, UIBase> uiDict = new Dictionary<EUISTATE, UIBase>();

        private void Awake()
        {
            Instance = this;

            UIBase[] uis = GetComponentsInChildren<UIBase>(true);
            foreach (var ui in uis)
            {
                uiDict.Add(ui.UIType, ui);
            }
        }

        public T GetUI<T>(EUISTATE type) where T : UIBase
        {
            return uiDict[type] as T;
        }

        public void ShowUI(EUISTATE type)
        {
            uiDict[type].Show();
        }

        public void HideUI(EUISTATE type)
        {
            uiDict[type].Hide();
        }
    }


}

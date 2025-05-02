using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Meta
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        private Dictionary<EUIState, UIBase> uiDict = new Dictionary<EUIState, UIBase>();

        private void Awake()
        {
            Instance = this;

            UIBase[] uis = GetComponentsInChildren<UIBase>(true);
            foreach (var ui in uis)
            {
                uiDict.Add(ui.UIType, ui);
            }
        }

        public T GetUI<T>(EUIState type) where T : UIBase
        {
            return uiDict[type] as T;
        }

        public void ShowUI(EUIState type)
        {
            uiDict[type].Show();
        }

        public void HideUI(EUIState type)
        {
            uiDict[type].Hide();
        }
    }


}

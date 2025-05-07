using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta
{
    public abstract class UIBase : MonoBehaviour
    {
        public abstract EUISTATE UIType { get; }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }

}

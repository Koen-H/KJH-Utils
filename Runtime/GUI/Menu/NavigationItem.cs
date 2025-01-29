using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KJH.Utils.GUINS.MenuNS
{
    public class NavigationItem : MonoBehaviour, ISelectHandler
    {
        public event Action OnCellNavigationItemSelected;

        public void OnSelect(BaseEventData eventData)
        {
            OnCellNavigationItemSelected?.Invoke();
        }
    }
}

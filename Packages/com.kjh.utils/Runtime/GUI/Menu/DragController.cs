using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KJH.Utils.GUINS.MenuNS
{
    public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Action OnBeginDragAction;
        public Action OnDragAction;
        public Action OnEndDragAction;

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnBeginDragAction?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragAction?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnEndDragAction?.Invoke();
        }
    }
}

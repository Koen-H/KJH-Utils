using UnityEngine;

namespace KJH.Utils.GUINS.MenuNS
{
    public abstract class MenuCell<T> : MonoBehaviour where T : MenuCellData
    {
        public T Data { get; private set; }

        public void SetData(T newData)
        {
            Data = newData;

            Refresh();
        }

        protected abstract void Refresh();
    }
}

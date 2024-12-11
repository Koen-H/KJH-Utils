using UnityEngine;

namespace KJH.Utils.GUINS.ViewController
{
    public abstract class ViewController : MonoBehaviour
    {
        public abstract void ViewWillAppear();
        public abstract void ViewAppeared();
        public abstract void ViewWillDisappear();
        public abstract void ViewDisappeared();

        protected virtual void OnDestroy()
        {
            ViewWillDisappear();
            ViewDisappeared();
        }
    }
}

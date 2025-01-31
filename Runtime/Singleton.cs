using UnityEngine;

namespace KJH.Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError($"Tried getting singleton of type {typeof(T)}, but it was null!");
                return _instance;
            }
        }

        protected bool SetInstance(T instance, bool dontDestroyOnLoad = true, bool overrideExisting = false)
        {
            if (overrideExisting && _instance != null)
                Destroy(_instance.gameObject);

            if (_instance == null)
            {
                _instance = this as T;
                if (dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);

            return _instance == this;
        }
    }
}

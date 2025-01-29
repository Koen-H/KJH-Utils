using KJH.Utils.SerializeableNS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KJH.Utils.GUINS.ViewController
{
    public abstract class GUIController<T> : MonoBehaviour
    {
        [SerializeField] protected SerializedDictionary<T, ViewController> viewControllers = new SerializedDictionary<T, ViewController>();

        protected Dictionary<T, ViewController> activeViewControllers = new Dictionary<T, ViewController>();

        public void ShowViewController(T controllerToShow, bool closeActiveViewcontrollers)
        {
            StartCoroutine(BeginShowViewCo(controllerToShow, closeActiveViewcontrollers));
        }

        private IEnumerator BeginShowViewCo(T controllerToShow, bool closeActiveViewcontrollers)
        {
            if (closeActiveViewcontrollers)
            {
                foreach (var activeViewController in activeViewControllers)
                {
                    yield return CloseViewCo(activeViewController.Value);
                    activeViewControllers.Remove(activeViewController.Key);
                }
            }

            ViewController viewToShow = GetViewController(controllerToShow);

            yield return ShowViewCo(viewToShow);

            activeViewControllers.Add(controllerToShow, viewToShow);
        }

        public void CloseViewController(T controllerToClose)
        {
            bool isViewActive = activeViewControllers.ContainsKey(controllerToClose);

            if (!isViewActive)
                return;

            ViewController viewToClose = activeViewControllers[controllerToClose];

            StartCoroutine(CloseViewCo(viewToClose));
            activeViewControllers.Remove(controllerToClose);
        }

        private IEnumerator ShowViewCo(ViewController viewToShow)
        {
            bool waitOneFrame = false;

            viewToShow.ViewWillAppear();

            if (waitOneFrame)
                yield return null;

            viewToShow.gameObject.SetActive(true);
            viewToShow.ViewAppeared();
        }

        private IEnumerator CloseViewCo(ViewController viewToClose)
        {
            bool waitOneFrame = false;

            viewToClose.ViewWillDisappear();

            if (waitOneFrame)
                yield return null;

            viewToClose.gameObject.SetActive(false);
            viewToClose.ViewDisappeared();
        }

        protected ViewController GetViewController(T relatedEnum)
        {
            return viewControllers[relatedEnum];
        }
    }
}

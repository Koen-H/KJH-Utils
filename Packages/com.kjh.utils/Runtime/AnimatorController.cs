using UnityEngine;

namespace KJH.Utils
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] protected Animator animator;

        public Animator Animator => animator;

        private System.Action<string> onAnimationEventTriggered;

        public event System.Action<string> OnAnimationEventTriggered
        {
            add { onAnimationEventTriggered += value; }
            remove { onAnimationEventTriggered -= value; }
        }

        /// <summary>
        /// Invoked by the animation event in the animator.
        /// To use, create an animation event in the animator and give the function the name 'OnAnimationEvent' with a string parameter.
        /// </summary>
        /// <param name="eventName"></param>
        public void OnAnimationEvent(string eventName)
        {
            onAnimationEventTriggered?.Invoke(eventName);
        }

        public void SetBool(string boolName, bool newValue)
        {
            animator.SetBool(boolName, newValue);
        }

        public void SetTrigger(string triggerName)
        {
            animator.SetTrigger(triggerName);
        }
    }
}

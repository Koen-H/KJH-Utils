using System;
using System.Collections.Generic;
using UnityEngine;

namespace KJH.Utils
{
    [Serializable]
    public class EventVariable<T>
    {
        [SerializeField] private T value;

        public T Value
        {
            get => value;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(this.value, value))
                {
                    T oldValue = this.value;
                    this.value = value;

                    OnValueChanged?.Invoke(oldValue, value);
                }
            }
        }

        /// <summary>
        /// Called when the value changes, old and new value are passed as arguments.
        /// </summary>
        public event Action<T, T> OnValueChanged;

        public EventVariable() { }

        public EventVariable(T initialValue)
        {
            Value = initialValue;
        }
    }
}

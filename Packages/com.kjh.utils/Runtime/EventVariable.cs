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
                    this.value = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }

        public event Action<T> OnValueChanged;

        public EventVariable() { }

        public EventVariable(T initialValue)
        {
            Value = initialValue;
        }
    }
}

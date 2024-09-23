using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KJH.Utils
{
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();

        [SerializeField] private List<TValue> values = new List<TValue>();

        private Dictionary<TKey, TValue> dictionary;

        public TValue this[TKey key]
        {
            get
            {
                InitializeDictionary();
                return dictionary[key];
            }
            set
            {
                InitializeDictionary();
                dictionary[key] = value;
            }
        }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            InitializeDictionary();
            return dictionary;
        }

        public void Add(TKey key, TValue value)
        {
            InitializeDictionary();
            dictionary.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            InitializeDictionary();
            return dictionary.Remove(key);
        }

        public bool ContainsKey(TKey key)
        {
            InitializeDictionary();
            return dictionary.ContainsKey(key);
        }

        public void Clear()
        {
            InitializeDictionary();
            dictionary.Clear();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            InitializeDictionary();
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            InitializeDictionary();
            return dictionary.GetEnumerator();
        }

        private void InitializeDictionary()
        {
            if (dictionary == null)
            {
                dictionary = new Dictionary<TKey, TValue>();
                for (int i = 0; i < keys.Count; i++)
                {
                    dictionary.Add(keys[i], values[i]);
                }
            }
        }
    }
}

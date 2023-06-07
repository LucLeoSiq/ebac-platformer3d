using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.Core.Singleton
{

    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        private bool _persistent = false;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = GetComponent<T>();
                if (_persistent)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
                Destroy(gameObject);
        }


        public Singleton(bool persistent = false)
        {
            _persistent = persistent;
        }
    }
}

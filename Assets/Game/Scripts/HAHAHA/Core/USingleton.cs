using UnityEngine;

namespace HAHAHA
{
    public abstract class USingleton<T> : GameBehaviour where T : Component
    {
    #region Fields
        private static T _instance;

        #endregion

        #region Properties
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        var obj = new GameObject { name = typeof(T).Name };
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Methods
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
        }

        #endregion

    }
}
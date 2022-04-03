using UnityEngine;

namespace Ren.Base
{

    public abstract class BaseSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        private static object locker = new object();
        private static T instance;
        public static T Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();
                    }
                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (Instance.GetHashCode() != GetHashCode())
            {
                Destroy(this);
            }
        }

    }

}
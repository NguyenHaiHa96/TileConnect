using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    var singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";

                }

            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        DontDestroyOnLoad((this as T).gameObject);
    }
}

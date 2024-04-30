using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;
    public static bool destroyOnLoad = false;
    public static GameObject monoSingleton;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                // ��������в�����ʵ�����򴴽�һ���µ���Ϸ���󲢽���������ӵ�������
                if (instance == null)
                {
                    monoSingleton = new GameObject(typeof(T).Name);
                    instance = monoSingleton.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    public virtual T Init(bool needDestroy = false)
    {
        destroyOnLoad = needDestroy;
        if (needDestroy)
        {
            AddSceneChangedEvent();
        }
        return Instance;
    }

    public void AddSceneChangedEvent()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        if (destroyOnLoad)
        {
            if (!instance)
            {
                DestroyImmediate(instance);
            }
        }
    }
}

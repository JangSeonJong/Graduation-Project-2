using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    [SerializeField]
    GameObject go;

    Stack<GameObject> poolStack = new Stack<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            Init();
        }
        else
            Destroy(gameObject);
    }

    void Init()
    {
        Push(go, 5);
    }

    public void Push(GameObject original, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(original);
            go.name = original.name;
            go.transform.parent = instance.transform;
            go.SetActive(false);
            poolStack.Push(go);
        }
    }

    public GameObject Pop()
    {
        if (poolStack.Count > 0)
        {
            GameObject go = poolStack.Pop();
            go.SetActive(true);
            return go;
        }
        else
            return null;
    }
}

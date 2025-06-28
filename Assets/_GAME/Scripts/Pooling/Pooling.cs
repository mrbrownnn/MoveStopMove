using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public static Pooling instance;

    Dictionary<string, Queue<GameObject>> _Everythings = new Dictionary<string, Queue<GameObject>>();
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void _Push(string key,GameObject gob)
    {
        gob.SetActive(false);
        if (!_Everythings.ContainsKey(key))
        {
            _Everythings.Add(key, new Queue<GameObject>());
        }
        _Everythings[key].Enqueue(gob);
    }

    public GameObject _Pull(string key,string path)
    {
        if (_Everythings.ContainsKey(key))
        {
            if (_Everythings[key].Count > 0)
            {
                GameObject gobCopy =  _Everythings[key].Dequeue();
                gobCopy.SetActive(true);
                return gobCopy;
            }
            else
            {
                GameObject gobCopy = Instantiate(Resources.Load<GameObject>(path));
                gobCopy.SetActive(true);
                return gobCopy;
            }
        }
        else
        {
            GameObject gobCopy = Instantiate(Resources.Load<GameObject>(path));
            gobCopy.SetActive(true);
            return gobCopy;
        }
    }
}

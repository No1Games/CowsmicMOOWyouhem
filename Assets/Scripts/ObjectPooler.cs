using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public class Pool
    {
        public GameObject prefabGO;
        public string tag;
        public int poolSize;
    }
    
    public Dictionary<string, Queue<GameObject>> poolDictionary;  //тут будуть зберігатися різні паки того що ми хочемо спавнити
    public List<Pool> poolList; // це список шаблонів з яких будуть збиратись паки

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>(); 


        foreach( Pool pool in poolList )
        {
            Queue<GameObject> poolObject = new Queue<GameObject>();
            
            for( int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefabGO);
                obj.SetActive(false);
                poolObject.Enqueue(obj);
            }
        }

    }

    
}

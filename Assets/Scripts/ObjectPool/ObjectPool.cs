using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
    }


    public List<Pool> PoolsList;
    public Dictionary<string, Queue<GameObject>> PoolDict;

    private void Awake()
    {
        PoolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in PoolsList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.Prefab, transform.position, Quaternion.identity);
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                obj.name = pool.Tag + " " + i.ToString();
                obj.transform.rotation = pool.Prefab.transform.rotation;
                objectPool.Enqueue(obj);
            }
            PoolDict.Add(pool.Tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        GameObject objectToSpawn = PoolDict[tag].Dequeue();

        objectToSpawn.SetActive(true);

        PoolDict[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public GameObject RandomizeObject()
    {
        int randomInt = Random.Range(0, PoolDict.Count);

        string key = PoolDict.ElementAt(randomInt).Key;
        GameObject objectSpawned = SpawnFromPool(key);

        return objectSpawned;
    }
}
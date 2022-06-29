using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] GameObject[] pooledObjects;
    [SerializeField] int indexBullet;
    [SerializeField] int currentBullet;


    public GameObject GetPooledObjects()
    {
        currentBullet = indexBullet;
        indexBullet = (indexBullet + 1) % pooledObjects.Length;
        Debug.Log(indexBullet);
        return pooledObjects[currentBullet];
    }
}

using System.Collections;
using System.Collections.Generic;
using team28;
using UnityEngine;

namespace team28
{
    public class ItemPoolManager : MicrogameEvents
    {
        [Header("Player Controller Script")]
        public ScanPlayerController controller;

        [Header("Groceries")]
        public GameObject[] easyObjects;
        public GameObject[] mediumObjects;
        public GameObject[] difficultObjects;

        [Header("Grocery Spawn Point")]
        public Transform spawnPoint;

        [HideInInspector] public int poolID = 0;


        // Start is called before the first frame update
        void Awake()
        {
            poolID = 0;
        }

        // Update is called once per frame
        void Update()
        {
        }

        //this returns the object pools that we will have
        public GameObject[] ReturnObjectPool()
        {
            switch (poolID)
            {
                case 0:
                    return easyObjects;
                case 1:
                    return mediumObjects;
                case 2:
                    return difficultObjects;
                default:
                    return null;
            }
        }

        //this will get us a random object from our pool
        public GameObject RandomItemToSpawn(GameObject[] objectPool)
        {
            int randomInt = Random.Range(0, objectPool.Length);
            GameObject objectToSpawn = objectPool[randomInt];
            return objectToSpawn;
        }

        public void SpawnNewItem()
        {
            GameObject spawnedItem = RandomItemToSpawn(ReturnObjectPool());
            GameObject newGrocery = Instantiate(spawnedItem, spawnPoint.position, Quaternion.identity);
            controller.ActiveItem = newGrocery;
        }
    }
}

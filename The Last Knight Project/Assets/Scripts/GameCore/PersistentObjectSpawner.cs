using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameCore
{
    //the idea of this class is to take care of spawning the persistent objects in our case the fader
    //and set it to don't destroy on load

    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        //This is a static so every instance of this class can remember if this was set befor

        static bool hasSpawned = false;

        private void Awake() 
        {
            //in case that has the object get out
            if (hasSpawned) return;

            //if the object does not exist will call this method to create one
            SpawnPersistentObjects();

            //set the static value to true so now all the future instaces of this class will remember
            //that the object has spawned.
            hasSpawned = true;    
        }

        private void SpawnPersistentObjects()
        {
            //instantiate the object set as serializedField
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            //set the object to dontdestroy that way being persistent trhu scenes
            DontDestroyOnLoad(persistentObject);
        }
    }
}

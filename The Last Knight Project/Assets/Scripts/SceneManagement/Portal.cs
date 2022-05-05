using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        //references on portals
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneTolad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeOutTime =1f;
        [SerializeField] float waitFadeTime = 0.5f;

        //trigger the loading the next scene
        private void OnTriggerEnter(Collider other) 
        {
            if (other.tag == "Player")
            {
                //using the Coroutine feature and Ienumerator to make the code wait the scene to load
                StartCoroutine(Transition());
            } 
        }

        private IEnumerator Transition()
        {
            //preventing wrong number to load scene
            if (sceneTolad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeOutTime);

            DontDestroyOnLoad(this.gameObject);
            yield return SceneManager.LoadSceneAsync(sceneTolad);

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(waitFadeTime);
            yield return fader.FadeIn(fadeInTime);

            
            Destroy(this.gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            //change made to avoid navmesh flicking with the teleporting
            //player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            //using the FindObjectsOfType will return a list of the objects
            //so we loop
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
    }
}

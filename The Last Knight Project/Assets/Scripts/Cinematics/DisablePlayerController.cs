using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.GameCore;
using RPG.Controller;

namespace RPG.Cinematics
{
    public class DisablePlayerController : MonoBehaviour
    {
        GameObject player;

        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnebleControl;

            //Getting the gameobject player so we can his action scheduler and cancel any action
            player = GameObject.FindWithTag("Player");

        }

        void DisableControl(PlayableDirector pb)
        {
           
           player.GetComponent<ActionScheduler>().CancelCurrentAction();
           //However this does not stop the player moving to the location that the target was
           //for that we need to change the cancel method on figher so when we cancel the attack
           //we also cancel the movement to attack.

           //disabling the playerController
           player.GetComponent<PlayerController>().enabled = false;

        }

        void EnebleControl(PlayableDirector pb)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movements;
using RPG.Combat;
using System;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            //if false will skip this and go to InteractWithMovement
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;

        }

        private bool InteractWithCombat()
        {
            //Storing the objects that the ray hits an a array "hits"
            //Using the GetRay method
            RaycastHit[] hits =Physics.RaycastAll(GetRay());

            //Iterating to all the hits from Ray
            foreach (RaycastHit hit in hits)
            {
                //I'll use the transform as the thing that the foreach is looking for
                //Store in a CombatTarget type called "target"
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                //if there is no target on the object hit by the ray will continue to the next one
                if (target == null) continue;
                
                //in the case there is a target and the mouse button was triggered the Attack is called from the Fighter.cs
                //target will be passed to the Fighter also, so the fighter can interact with the target
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                //this will allow change the mouse cursor if is hoovered on a enemy
                return true;
            }
            //means there is no combat target to interact with
            return false;
        }

        //Method that call the movement according to player's input.
        //Returns a boolean so we can change the mouse cursor if is hoovering on
        //somewhere that can or can't go
        private bool InteractWithMovement()
        {
            //Getting the hit on the terrain
            RaycastHit hit;

            //The raycasthit is a boolean so we store as it so we can do something if there is a hit on the
            // terrain. The method GetRay will get the hit
            bool hasHit = Physics.Raycast(GetRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;

        }

        private static Ray GetRay()
        {
            //Getting the ray
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }   
}

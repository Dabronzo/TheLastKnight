using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movements;
using RPG.Combat;
using System;
using RPG.GameCore;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        
        void Start()
        {
            health = GetComponent<Health>();
        
        }

        void Update()
        {
            //statement that if the player is dead everything will be disable
            if (health.IsDead()) return;

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
                //This will check for the CombatTarget component, since that only enemies have it
                //a if statement can be made to avoid player attacking itself
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                //Here we check calling the Fighter if the target hit by the raycast is null or is dead
                //means that we can not attack, returning false will satisfy the if bellow and continue the
                //foreach to the next itens
                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) { continue;}
                
                //in the case there is a target and the mouse button was triggered the Attack is called from the Fighter.cs
                //target will be passed to the Fighter also, so the fighter can interact with the target
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
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

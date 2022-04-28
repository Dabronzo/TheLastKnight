using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.GameCore;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        //Varible of chaise distance
        [SerializeField] float chaiseDistance = 5f;

        Fighter fighterComponent;
        Health health;
        GameObject player;

        private void Start()
        {
            fighterComponent = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
        }


        private void Update() 
        {
            //Cancelling everything if the character is dead
            if (health.IsDead()) return;
            
            if(InRangeChaise() && fighterComponent.CanAttack(player))
            {
               fighterComponent.Attack(player);
                
            }
            else
            {
                fighterComponent.Cancel();
            }
        }



        public bool InRangeChaise()
        {
            //calculate the distance and returning a bool
            return Vector3.Distance(player.transform.position, transform.position) <= chaiseDistance;
        }
    
    }
}

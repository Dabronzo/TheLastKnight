using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.GameCore;
using RPG.Movements;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        //Varible of chaise distance
        [SerializeField] float chaiseDistance = 5f;
        [SerializeField] float suspiciusTime = 5f;

        Fighter fighterComponent;
        Health health;
        GameObject player;
        Mover mover;

        //to make some sort of state where the AI can do some behaivours
        //The guard behaivour is just when the npc remembers the initial position
        //and after the chaise will return to guard there
        Vector3 guardingPosition;

        //to make the suspicius behaivour we gonna set a varible to the npc remembers when was the last
        //time that the player was in his interest area
        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start()
        {
            fighterComponent = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
            mover = GetComponent<Mover>();

            guardingPosition = transform.position;
        }


        private void Update() 
        {
            //Cancelling everything if the character is dead
            if (health.IsDead()) return;
            
            if(InRangeChaise() && fighterComponent.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaivour();

            }
            //to check the suspicius behaivour
            else if (timeSinceLastSawPlayer < suspiciusTime)
            {
                SuspiciusBehauvour();

            }
            else
            {
                GuardingBehaivour();
            }
            //to update the timeSinceLastSawPlayer
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardingBehaivour()
        {
            //fighterComponent.Cancel();
            //since the StartMoveAction calls the action scheduler that will cancel
            //the previous behave so don't need to cancel the attack
            mover.StartMoveAction(guardingPosition);
        }

        private void SuspiciusBehauvour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaivour()
        {
            fighterComponent.Attack(player);
        }

        public bool InRangeChaise()
        {
            //calculate the distance and returning a bool
            return Vector3.Distance(player.transform.position, transform.position) <= chaiseDistance;
        }

        //Draw Gizmos method called by Unity to visualize the chaise are of the NPC
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaiseDistance);
            
        }
    
    }
}

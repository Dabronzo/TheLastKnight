using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.GameCore;
using RPG.Movements;
using System;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        //Varible of chaise distance
        [SerializeField] float chaiseDistance = 5f;

        [SerializeField] float suspiciusTime = 5f;

        //to hold the patrolPath and need to be set on Unity because the patrol only
        //exist on the scene
        [SerializeField] PatrolPath patrolPath;

        //tolerance of the distance to waypoint
        [SerializeField] float waypointTollerance = 0.5f;

        Fighter fighterComponent;
        Health health;
        GameObject player;
        Mover mover;

        //varible so the NPC remeber the index of the waypoints
        int currentWaypointIndex = 0;

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
                PatrolBehaviour();
            }
            //to update the timeSinceLastSawPlayer
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        //this method should first create a nestPosition, so the AI can know where to go
        //by default will be the guardingPosition
        //then the following logic: if is AtTheWaypoint(), CycleWaypoint() wlese the NextPosition is GetCurrentWaypoint
        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardingPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();

            }
            //fighterComponent.Cancel();
            //since the StartMoveAction calls the action scheduler that will cancel
            //the previous behave so don't need to cancel the attack
            mover.StartMoveAction(nextPosition);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypont(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
            
        }

        private bool AtWaypoint()
        {
           float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
           return distanceToWaypoint < waypointTollerance;
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

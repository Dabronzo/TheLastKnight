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

        //Dwelling time on the patrol waypoints
        [SerializeField] float waypointDwellingTime = 2f;

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

        float timeSinceArrivedWaypoint = Mathf.Infinity;

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

            if (InRangeChaise() && fighterComponent.CanAttack(player))
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
            //to update the timeSinceLastSawPlayer and timeSinceArrivedWaypoint
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedWaypoint += Time.deltaTime;
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
                    timeSinceArrivedWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();

            }
            //statement to check if the NPC is already enough time at the waypoint
            if (timeSinceArrivedWaypoint > waypointDwellingTime)
            {
                //fighterComponent.Cancel();
                //since the StartMoveAction calls the action scheduler that will cancel
                //the previous behave so don't need to cancel the attack
                mover.StartMoveAction(nextPosition);
            }
            
        }

        //Uses the GetWaypoint of patrol path to get which waypoint is it
        //using the curremnt index known by the NPC
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypont(currentWaypointIndex);
        }

        //Using the GetNextIndex from patrol path to update the waypoint index to the next one
        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
            
        }

        //boolean to check if the NPC is in a tollerable distance to the waypoint
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

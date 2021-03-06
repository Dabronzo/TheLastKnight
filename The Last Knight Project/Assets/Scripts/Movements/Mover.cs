using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.GameCore;

namespace RPG.Movements
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent naveMeshAgent;
        Health health;
        [SerializeField] float maxSpeed = 5.6f;

        void Start()
        {
            //Getting the NaveMeshAgent
            naveMeshAgent = GetComponent<NavMeshAgent>();

            //Getting the Health
            health = GetComponent<Health>();
        
        }

        // Update is called once per frame
        void Update()
        {
            //disable the navMesh in case of death
            naveMeshAgent.enabled = !health.IsDead();
            
            UpdateAnimator();
        
        }
        
        //This method is used to cancel the Combat action and start moving to the new destination
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            //Starting the Action on the ActionScheduler
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
            
        }

   
        //the actual move on the navmesh public to be called from the PlayerController
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            

            //because the NavMesh could be set to Stopped by the Stop method
            //first is set as not stopped
            naveMeshAgent.isStopped = false;
            naveMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            naveMeshAgent.destination = destination;
        }

        //This function will take the global velocity from the navmesh and transfor to local
        //of this gameobject and set the animation.
        private void UpdateAnimator()
        {
            //Gettin the global velocity from the navMesh
            Vector3 velocity = naveMeshAgent.velocity;

            //converting to a local varible
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            //creating the speed
            float speed = localVelocity.z;

            //Passing to the Animator the speed
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);

        }

        //Method created to stop the character on walking, when is in range of the combat
        //Also is used as part of interface IAction to be used by the ActionScheduler to
        //Cancel the movement action
        public void Cancel()
        {
            naveMeshAgent.isStopped = true;

        }
    }
}


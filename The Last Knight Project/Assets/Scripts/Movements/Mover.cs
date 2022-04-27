using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movements
{
    public class Mover : MonoBehaviour
    {
        NavMeshAgent naveMeshAgent;

        void Start()
        {
            //Getting the NaveMeshAgent
            naveMeshAgent = GetComponent<NavMeshAgent>();
        
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        
        }

   
        //the actual move on the navmesh public to be called from the PlayerController
        public void MoveTo(Vector3 destination)
        {
            //because the NavMesh could be set to Stopped by the Stop method
            //first is set as not stopped
            naveMeshAgent.isStopped = false;

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
        public void Stop()
        {
            naveMeshAgent.isStopped = true;

        }
    }
}


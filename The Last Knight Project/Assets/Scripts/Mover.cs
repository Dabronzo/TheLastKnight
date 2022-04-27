using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

        UpdateAnimator();
        
    }

    private void MoveToCursor()
    {
        //Getting the ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Getting the hit on the terrain
        RaycastHit hit;

        //The raycasthit is a boolean so we store as it so we can do something if there is a hit on the
        // terrain
        bool hasHit = Physics.Raycast(ray, out hit);
        
        if(hasHit)
        {
            naveMeshAgent.destination = hit.point;
        }

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
}

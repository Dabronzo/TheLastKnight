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
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
        
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
}

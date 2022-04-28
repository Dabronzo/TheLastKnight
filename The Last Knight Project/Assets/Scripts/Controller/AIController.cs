using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        //Varible of chaise distance
        [SerializeField] float chaiseDistance = 5f;

        Fighter fighterComponent;
        GameObject player;

        private void Start()
        {
            fighterComponent = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }


        private void Update() 
        {
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

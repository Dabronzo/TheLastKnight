using UnityEngine;
using RPG.Movements;

namespace RPG.Combat
{   
    public class Fighter : MonoBehaviour 
    {
        [SerializeField] float weaponRange = 2f;

        //Using a Transform to hold the position of a target
        Transform target;

        private void Update()
        {
            //if the target is not set by the PlayerController will be null and will skip everything
            if (target == null) return;

            //to call MoveTo and goes in direction of the target
            if (!GetInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }

            //in case that the range is reached or the target is gone, stop moving
            else
            {
                GetComponent<Mover>().Stop();
            }

        }

        //Method to check if is in range
        private bool GetInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        //Attack method takes the target varible and set as the combatTarget sent by the PlayerController
        public void Attack(CombatTarget combatTarget)
        {
           target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
        
    }
}
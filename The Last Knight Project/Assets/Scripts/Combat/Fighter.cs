using UnityEngine;
using RPG.Movements;
using RPG.GameCore;

namespace RPG.Combat
{   
    public class Fighter : MonoBehaviour, IAction
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
                GetComponent<Mover>().Cancel();
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
            // here the script calls for the ActionScheduler to start the Attack Action
            GetComponent<ActionScheduler>().StartAction(this);

            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
        
    }
}
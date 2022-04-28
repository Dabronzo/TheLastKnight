using UnityEngine;
using RPG.Movements;
using RPG.GameCore;

namespace RPG.Combat
{   
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        
        [SerializeField] float timeBetweenAttacks = 1f;

        //Using the Health component as a target, since that if the player is fighting something
        //it should have a health component
        Health target;

        //this will be updated in every frame so the game will know always when was the last attack
        float timeSinceLastAttack = 0;

        private void Update()
        {
            //updating the last attack
            timeSinceLastAttack += Time.deltaTime;
            //if the target is not set by the PlayerController will be null and will skip everything
            if (target == null) return;

            //checking if the target is dead and stopping the attack
            if (target.IsDead()) return;

            if(!GetInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }

            //in case that the range is reached or the target is gone, stop moving
            else
            {
                GetComponent<Mover>().Cancel();
                AttackAnimationTrigger();
            }

        }

        //Set the trigger to start the attack animation
        private void AttackAnimationTrigger()
        {
            //cooldown effect between attacks
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //Damage is taken on the Hit()
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
            
        }

        //Method to check if is in range
        private bool GetInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        //Attack method takes the target varible and set as the combatTarget sent by the PlayerController
        public void Attack(CombatTarget combatTarget)
        {
            // here the script calls for the ActionScheduler to start the Attack Action
            GetComponent<ActionScheduler>().StartAction(this);

            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            //Stopping the animation attack when is cancelled
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

        //Animation event to handle the hit and cause damage on the right time in the animation
        void Hit()
        {
           
            target.TakeDamage(5);  

        }
        
    }
}
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

            //if the player is dead we don't continue the attack
            if (target.IsDead()) return;
            
            //if the player is not in range the combat will move the player until the range is reached
            if(!GetInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }

            //in case that the range is reached or the target is gone, stop moving
            //and activate the attackAnimation
            else
            {
                GetComponent<Mover>().Cancel();
                AttackAnimationTrigger();
            }

        }

        //Set the trigger to start the attack animation
        private void AttackAnimationTrigger()
        {
            //making the character turn to the direction to the target
            transform.LookAt(target.transform);

            //cooldown effect between attacks
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //reseting the stopAttack
                GetComponent<Animator>().ResetTrigger("stopAttack");
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
        //this is the first thing that happens when a player click on a enemy to attack
        //by changing the "target" from null to something we activate part of the update logics
        public void Attack(CombatTarget combatTarget)
        {
            // here the script calls for the ActionScheduler to start the Attack Action
            GetComponent<ActionScheduler>().StartAction(this);

            target = combatTarget.GetComponent<Health>();
        }

        //Method to tell the Player Controller if there is a target and
        //if is not dead returning true in this case.
        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();

            //short circuit when if the targetToTest is null, so don't even try ti check
            //if is dead
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Cancel()
        {
            //Reseting the attack triger
            GetComponent<Animator>().ResetTrigger("attack");
            //Stopping the animation attack when is cancelled
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

        //Animation event to handle the hit and cause damage on the right time in the animation
        void Hit()
        {
           if(target == null) return;
            target.TakeDamage(5);  

        }
        
    }
}
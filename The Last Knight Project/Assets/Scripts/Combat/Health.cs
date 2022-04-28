using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        //Heatlh varible
        [SerializeField] float health = 100f;

        //varible to check if is dead
        bool isDead = false;

        //creating a method to other classes can check if the object is dead
        public bool IsDead()
        {
            return isDead;
        }

        //Method to take damage
        public void TakeDamage(float damage)
        {
            //this line reads: health minus damage and if the health goes bellow zero, zero is the higher so will mantain
            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                if(isDead) return;
                DeathTrigger();
            }

        }

        private void DeathTrigger()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("death");
        }
    }

}

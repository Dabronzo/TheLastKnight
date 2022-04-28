using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        //Heatlh varible
        [SerializeField] float health = 100f;

        //Method to take damage
        public void TakeDamage(float damage)
        {
            //this line reads: health minus damage and if the health goes bellow zero, zero is the higher so will mantain
            health = Mathf.Max(health - damage, 0);
            print(health);
        }

    }

}

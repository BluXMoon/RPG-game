using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        [SerializeField] Animator animator;
        bool isDead;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if(health == 0 && !isDead)
            {
                if (!this.gameObject.CompareTag("Player"))
                {
                    GetComponent<CapsuleCollider>().enabled = false;
                }
                
                animator.SetTrigger("Death");
                isDead = true;
            }
        }
    }
}


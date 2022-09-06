using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        [SerializeField] Animator animator;
        [SerializeField] NavMeshAgent agent;
        [SerializeField] Slider healthSlider;
        bool isDead;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            healthSlider.value = health;
            
            if (health == 0 && !isDead)
            {
                if (!this.gameObject.CompareTag("Player"))
                {
                    GetComponent<CapsuleCollider>().enabled = false;
                    healthSlider.gameObject.SetActive(false);
                    agent.enabled = false;
                }
               
                animator.SetTrigger("Death");
                isDead = true;
            }
        }
    }
}


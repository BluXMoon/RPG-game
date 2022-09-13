using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using RPG.Saving;

namespace RPG.Combat
{
    public class Health : MonoBehaviour, ISaveable
    {
        public float health = 100f;
        [SerializeField] Animator animator;
        [SerializeField] NavMeshAgent agent;
        [SerializeField] Slider healthSlider;
        bool isDead;

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            TakeDamage(0);
            if(transform.tag == "Player")
            {
                print(health);
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            UpdateHealthSlider();
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

        public void UpdateHealthSlider()
        {
            healthSlider.value = health;
        }
    }
}


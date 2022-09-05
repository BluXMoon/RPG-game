using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Player;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private Mover mover;
        [SerializeField] private ActionScheduler actionScheduler;
        [SerializeField] private float weaponRange;
        [SerializeField] private Animator animator;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 1f;

        Transform target;
        Health targetHealth;
        float timeSinceLastAttack = 0f;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (object.ReferenceEquals(null, target)) return;
            if (!IsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                if(timeSinceLastAttack > timeBetweenAttacks)
                {
                    AttackBehaviour();
                    timeSinceLastAttack = 0f;
                }
                
                mover.Cancel();
            }
        }

        private void AttackBehaviour()
        {
            animator.SetTrigger("Attack");
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget _target)
        {
            actionScheduler.StartAction(this);
            target = _target.transform;
        }

        void Hit() // Animation event
        {
            targetHealth = target.GetComponent("Health") as Health;
            targetHealth.TakeDamage(weaponDamage);
        }

        public void Cancel()
        {
            target = null;
        }
    }
}


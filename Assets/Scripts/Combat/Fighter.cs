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
        [SerializeField] private Health health;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 1f;

        Health target;
        float timeSinceLastAttack = 0f;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) { return; }
            if (health.IsDead()) { return; }

            if (!IsInRange())
            {
                mover.MoveTo(target.transform.position);
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
            transform.LookAt(target.transform.position, Vector3.up);
            animator.SetTrigger("Attack");
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(GameObject _target)
        {
            animator.ResetTrigger("CancelAttack");
            actionScheduler.StartAction(this);
            target = _target.GetComponent<Health>();
        }

        void Hit() // Animation event
        {
            if(target != null) { target.TakeDamage(weaponDamage); }
        }

        public void Cancel()
        {
            animator.SetTrigger("CancelAttack");
            target = null;
        }
    }
}


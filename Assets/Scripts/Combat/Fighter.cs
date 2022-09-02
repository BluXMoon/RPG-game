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
        Transform target;
        bool isInRange;
        private void Update()
        {
            if (object.ReferenceEquals(null, target)) return;
            if (!IsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                AttackBehaviour();
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

        public void Cancel()
        {
            target = null;
        }

        void Hit() // Animation event
        {

        }
    }
}


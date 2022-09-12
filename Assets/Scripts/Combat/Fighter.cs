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
        [SerializeField] private Animator animator;
        [SerializeField] private Health health;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 1f;
        [SerializeField] private float weaponRange;

        Health target;
        float timeSinceLastAttack = 0f;
        int layerMask;

        private void Start()
        {
            if (transform.CompareTag("Enemy"))
            {
                layerMask = LayerMask.GetMask("Player", "Enviroment");
            }
            else {
                layerMask = LayerMask.GetMask("Enemy", "Enviroment");
            }
           
        }
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
                transform.LookAt(target.transform.position, Vector3.up);
                RaycastHit hit;
                Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;
                Vector3 pos = transform.position;
                pos.y += 1;
                Debug.DrawRay(pos, forward, Color.green);
                if (timeSinceLastAttack > timeBetweenAttacks)
                {
                    if (Physics.Raycast(pos, forward, out hit, 100, layerMask))
                    {
                        if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Enemy"))
                        {
                            AttackBehaviour();
                            timeSinceLastAttack = 0f;
                            mover.Cancel();
                        }
                        else
                        {
                            mover.MoveTo(target.transform.position);
                        }
                    }
                }
            }
        }

        private void AttackBehaviour()
        {
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
            mover.Cancel(); 
        }
    }
}


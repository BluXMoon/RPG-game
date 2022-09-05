using RPG.Combat;
using RPG.Core;
using RPG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] Fighter fighter;
        [SerializeField] Mover mover;
        [SerializeField] ActionScheduler actionScheduler;

        private GameObject player;
        private Health playerHealth;
        Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();

            guardPosition = transform.position;
        }

        private void Update()
        {
            if (InChaseRangeOfPlayer() && !playerHealth.IsDead())
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < 3)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InChaseRangeOfPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}


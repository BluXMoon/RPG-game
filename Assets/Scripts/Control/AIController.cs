using RPG.Combat;
using RPG.Core;
using RPG.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float timeToWaitUntillMove = 2f;
        [SerializeField] Fighter fighter;
        [SerializeField] Mover mover;
        [SerializeField] ActionScheduler actionScheduler;
        [SerializeField] PatrolPath patrolPath;

        private GameObject player;
        private Health playerHealth, enemyHealth;
        Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceLastWaypointMoved = Mathf.Infinity;
        int currentWaypointIndex = 0;
        private bool isMoving;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();
            enemyHealth = gameObject.GetComponent<Health>();

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
                PatrolBehaviour();
            }

            UpdateTime();
        }

        private void UpdateTime()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceLastWaypointMoved += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            if (enemyHealth.IsDead()) { return; }
           
            Vector3 nextPosition = guardPosition;

            if(patrolPath != null)
            {
                if (AtWaypoint())
                {
                    isMoving = false;
                    timeSinceLastWaypointMoved = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            else
            {
                mover.StartMoveAction(guardPosition);
            }
           
            if (timeSinceLastWaypointMoved > timeToWaitUntillMove && !isMoving)
            {
                isMoving = true;
                mover.StartMoveAction(nextPosition);
            }
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
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


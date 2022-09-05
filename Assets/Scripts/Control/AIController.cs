using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] Fighter fighter;

        private GameObject player;
        private Health playerHealth;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Health>();
        }

        private void Update()
        {
            if(InAttackRangeOfPlayer() && !playerHealth.IsDead())
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(transform.position, player.transform.position) < chaseDistance;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Player;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private Mover mover;
        [SerializeField] private float weaponRange;
        Transform target;
        bool isInRange;
        private void Update()
        {
            isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;
            if (!object.ReferenceEquals(null, target) && !isInRange)
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Stop();
            }

        }
        public void Attack(CombatTarget _target)
        {
            target = _target.transform;
        }
    }
}


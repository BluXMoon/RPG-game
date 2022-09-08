
namespace RPG.Player
{
    using UnityEngine;
    using RPG.Combat;
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Fighter fighter;
        [SerializeField] private Health health;
        public Mover myMover;
        public LayerMask terrain;
        private int counter = 0;

        void Update()
        {
            if (health.IsDead()) { return; }
            if (Input.GetMouseButton(1))
            {
                if (InteractWithCombat()) return;
                if (InteractWithMovement()) return;
            }
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            if (counter < hits.Length)
            {
                CombatTarget target = hits[counter].transform.GetComponent<CombatTarget>();
                if (target != null)
                {
                    fighter.Attack(target.gameObject);
                    return true;
                }

                if (counter < hits.Length - 1) { counter++; InteractWithCombat(); } else { counter = 0; }
            }
            return false;
        }
      
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit, 200, terrain);
            if (hasHit)
            {
                myMover.StartMoveAction(hit.point);
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}


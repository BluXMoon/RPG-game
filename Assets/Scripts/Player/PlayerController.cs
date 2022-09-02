
namespace RPG.Player
{
    using UnityEngine;
    using RPG.Combat;
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Fighter fighter;
        private int counter = 0;
        void Update()
        {
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            if (counter < hits.Length)
            {
                CombatTarget target = hits[counter].transform.GetComponent<CombatTarget>();
                if (target != null)
                {
                    if (Input.GetMouseButton(0))
                    {
                        fighter.Attack(target);
                    }
                    return true;
                }

                if (counter < hits.Length - 1) { counter++; InteractWithCombat(); } else { counter = 0; }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    Mover.instance.StartMoveAction(hit.point);
                }
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


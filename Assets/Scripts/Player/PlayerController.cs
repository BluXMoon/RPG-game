
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
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void InteractWithCombat()
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
                }

                if (counter < hits.Length - 1) { counter++; InteractWithCombat(); } else { counter = 0; }
            }
        }

        private void MoveToCursor()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                Mover.instance.MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}


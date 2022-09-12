using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
        public bool played;

        public object CaptureState()
        {
            return played;
        }

        public void RestoreState(object state)
        {
            played = (bool)state;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !played)
            {
                played = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}


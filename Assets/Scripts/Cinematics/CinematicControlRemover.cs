using RPG.Core;
using RPG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        [SerializeField] PlayableDirector playableDirector;
        GameObject player;
        private void Start()
        {
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}

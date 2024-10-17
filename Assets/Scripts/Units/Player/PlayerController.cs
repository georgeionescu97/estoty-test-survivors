using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Units
{
    public class PlayerController : Unit, IPlayerController
    {
        public Transform PlayerTransform => this.transform;

        private void Awake()
        {
            _eventsContainer.OnUnitDied.AddListener(OnUnitDied);
        }

        private void OnUnitDied(Unit unit)
        {
            if (unit == this)
            {
                SceneManager.LoadSceneAsync(0);
            }
        }

        private void OnDestroy()
        {
            _eventsContainer.OnUnitDied.RemoveListener(OnUnitDied);
        }

    }
}

using Components;
using UnityEngine;

namespace Character
{
    public sealed class PlayerDeathObserver : MonoBehaviour
    {
        [SerializeField]
        private HealthComponent _playerHealthComponent;

        private void OnEnable()
        {
            _playerHealthComponent.OnHealthEmpty += OnPlayerDeath;
        }

        private void OnDisable()
        {
            _playerHealthComponent.OnHealthEmpty -= OnPlayerDeath;
        }

        private void OnPlayerDeath(GameObject player)
        {
            Time.timeScale = 0;
        }
    }
}
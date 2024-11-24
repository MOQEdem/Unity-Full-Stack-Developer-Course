using System;
using UnityEngine;

namespace Components
{
    public sealed class HealthComponent : MonoBehaviour
    {
        public event Action<GameObject> OnHealthEmpty;

        [SerializeField]
        private int _health;

        public void GetHit(int damage)
        {
            _health = Mathf.Max(0, _health - damage);

            if (_health == 0)
                OnHealthEmpty?.Invoke(gameObject);
        }
    }
}
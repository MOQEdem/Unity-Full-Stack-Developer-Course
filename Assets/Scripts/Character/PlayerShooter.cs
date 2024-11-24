using Components;
using UnityEngine;

namespace Character
{
    public sealed class PlayerShooter : MonoBehaviour
    {
        [SerializeField]
        private FireComponent _fireComponent;

        public void Shoot()
        {
            _fireComponent.Shoot(Vector2.up);
        }
    }
}
using Bullets;
using UnityEngine;

namespace Components
{
    public sealed class FireComponent : MonoBehaviour
    {
        [SerializeField]
        private BulletManager _bulletManager;

        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private TeamComponent _teamComponent;

        [SerializeField]
        private BulletConfig _config;

        public Transform FirePoint => _firePoint;

        public void Construct(BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
        }

        public void Shoot(Vector2 direction)
        {
            _bulletManager.SpawnBullet(new Args
            {
                Position = _firePoint.position,
                Color = _config.Color,
                PhysicsLayer = (int) _config.PhysicsLayer,
                Damage = _config.Damage,
                IsPlayer = _teamComponent.IsPlayer,
                Velocity = _firePoint.rotation * direction * _config.Speed
            });
        }

    }
}
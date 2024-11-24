using System.Collections.Generic;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField]
        private LevelBounds _levelBounds;

        [SerializeField]
        private BulletPool _bulletPool;

        private readonly List<Bullet> _cacheOfBullet = new();

        private void FixedUpdate()
        {
            _cacheOfBullet.Clear();
            _cacheOfBullet.AddRange(_bulletPool.ActiveObjects);

            for (int i = 0, count = _cacheOfBullet.Count; i < count; i++)
            {
                Bullet bullet = _cacheOfBullet[i];

                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }

        public void SpawnBullet(Args args)
        {
            var bullet = _bulletPool.Rent();
            bullet.Construct(args.Position, args.Color, args.PhysicsLayer, args.Damage, args.IsPlayer,
                args.Velocity);

            bullet.OnCollisionEntered += RemoveBullet;
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.OnCollisionEntered -= RemoveBullet;
            _bulletPool.Return(bullet);
        }
    }
}
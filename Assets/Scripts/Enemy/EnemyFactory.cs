using Bullets;
using Character;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject _player;

        [SerializeField]
        private BulletManager _bulletManager;

        public Enemy Construct(Enemy enemy, Transform container)
        {
            Enemy createdEnemy = Instantiate(enemy, container);
            createdEnemy.Construct(_player, _bulletManager);
            return createdEnemy;
        }
    }
}
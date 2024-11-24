using Pool;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyPool : MonoPool<Enemy>
    {
        [SerializeField]
        private EnemyFactory _enemyFactory;

        protected override Enemy CreateObject()
        {
            return _enemyFactory.Construct(Prefab, Container);
        }
    }
}
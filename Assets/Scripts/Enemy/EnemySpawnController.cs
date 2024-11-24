using UnityEngine;

namespace Enemy
{
    public sealed class EnemySpawnController : MonoBehaviour
    {
        [SerializeField]
        private EnemyManager _enemyManager;

        [SerializeField]
        private float _minSpawnTime = 1;

        [SerializeField]
        private float _maxSpawnTime = 2;

        [SerializeField]
        private int _maxActiveEnemies = 5;

        private float _elapsedTime;
        private float _timeBetweenSpawn = 0;

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _timeBetweenSpawn)
            {
                if (_enemyManager.ActiveEnemies < _maxActiveEnemies)
                {
                    _enemyManager.Spawn();

                    _timeBetweenSpawn = Random.Range(_minSpawnTime, _maxSpawnTime);
                }
            }
        }
    }
}
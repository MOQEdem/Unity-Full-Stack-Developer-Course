using System.Collections.Generic;
using Character;
using Components;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _spawnPositions;

        [SerializeField]
        private Transform[] _attackPositions;

        [SerializeField]
        private EnemyPool _enemyPool;

        private readonly HashSet<Enemy> _cacheOfEnemies = new();

        public int ActiveEnemies => _cacheOfEnemies.Count;

        public void Spawn()
        {
            Enemy enemy = _enemyPool.Rent();
            _cacheOfEnemies.Add(enemy);
            enemy.GetComponent<HealthComponent>().OnHealthEmpty += OnEnemyDied;

            int index = Random.Range(0, _spawnPositions.Length);
            Transform spawnPosition = _spawnPositions[index];
            enemy.transform.position = spawnPosition.position;

            index = Random.Range(0, _attackPositions.Length);
            Transform attackPosition = _attackPositions[index];
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
        }

        private void OnEnemyDied(GameObject healthComponent)
        {
            Enemy enemy = healthComponent.GetComponent<Enemy>();

            healthComponent.GetComponent<HealthComponent>().OnHealthEmpty -= OnEnemyDied;
            _cacheOfEnemies.Remove(enemy);
            _enemyPool.Return(enemy);
        }
    }
}
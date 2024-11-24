using Components;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField]
        private float _countdown;

        [SerializeField]
        private FireComponent _fireComponent;

        [SerializeField]
        private EnemyMoveAgent _moveAgent;

        private GameObject _target;
        private float _currentTime;

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public void Reset()
        {
            _currentTime = _countdown;
        }

        private void FixedUpdate()
        {
            if (!_moveAgent.IsPointReached)
                return;


            _currentTime -= Time.fixedDeltaTime;

            if (_currentTime <= 0)
            {
                Vector2 startPosition = _fireComponent.FirePoint.position;
                Vector2 vector = (Vector2) _target.transform.position - startPosition;
                Vector2 direction = vector.normalized;
                _fireComponent.Shoot(direction);

                _currentTime += _countdown;
            }
        }
    }
}
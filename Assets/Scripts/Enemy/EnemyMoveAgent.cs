using Components;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        [SerializeField]
        private MoveComponent _moveComponent;

        private Vector2 _destination;
        private float _offset =  0.25f;

        public bool IsPointReached { get; private set; }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsPointReached = false;
        }

        private void FixedUpdate()
        {
            Vector2 vector = _destination - (Vector2) transform.position;

            if (vector.magnitude <= _offset)
            {
                IsPointReached = true;
                return;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.Move(direction);
        }
    }
}
using UnityEngine;

namespace Components
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private float _speed;

        public void Move(Vector2 direction)
        {
            _rigidbody.MovePosition(_rigidbody.position + direction * _speed);
        }
    }
}                                                                 
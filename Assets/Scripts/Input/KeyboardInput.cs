using System;
using UnityEngine;

namespace Input
{
    public sealed class KeyboardInput : MonoBehaviour
    {
        public event Action<Vector2> OnMove;
        public event Action OnFire;

        [SerializeField]
        private KeyboardMap _keyboardMap;

        private Vector2 _moveDirection;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(_keyboardMap.Shoot))
                OnFire?.Invoke();
        }

        private void FixedUpdate()
        {
            if (UnityEngine.Input.GetKey(_keyboardMap.Left))
                _moveDirection.x = -1 * Time.fixedDeltaTime;
            else if (UnityEngine.Input.GetKey(_keyboardMap.Right))
                _moveDirection.x = 1 * Time.fixedDeltaTime;
            else
                _moveDirection.x = 0;

            OnMove?.Invoke(_moveDirection);
        }
    }
}
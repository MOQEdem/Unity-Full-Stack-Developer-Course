using Components;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _player;

        [SerializeField]
        private KeyboardInput _keyboardInput;

        private void OnEnable()
        {
            _keyboardInput.OnMove += _player.GetComponent<MoveComponent>().Move;
            _keyboardInput.OnFire += _player.GetComponent<PlayerShooter>().Shoot;
        }

        private void OnDisable()
        {
            _keyboardInput.OnMove -= _player.GetComponent<MoveComponent>().Move;
            _keyboardInput.OnFire -= _player.GetComponent<PlayerShooter>().Shoot;
        }
    }
}
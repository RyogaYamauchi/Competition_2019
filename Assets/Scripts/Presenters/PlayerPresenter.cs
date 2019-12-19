using Scripts.Models;
using Scripts.Views;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Presenters
{
    public class PlayerPresenter
    {
        private PlayerView _playerView = default;
        private IPlayerModel _playerModel { get; } = new PlayerModel();

        public PlayerPresenter(PlayerView playerView)
        {
            _playerView = playerView;
        }

        public Vector3 GetPosition()
        {
            return _playerView.GetPosition();
        }

        public void Move(Vector2 direction)
        {
            _playerView.Move(direction);
        }

        public void Jump()
        {
            Debug.Log("Jump");
            _playerView.Jump().Forget();
        }

        public void Attack()
        {
            Debug.Log("Attack");
            _playerView.Attack().Forget();
        }

        public void GetInput(InputViewModel inputViewModel)
        {
            
            var direction = inputViewModel.GetDirection();
            var inputString = inputViewModel.GetInput();
            switch (inputString)
            {
                case "c":
                    Jump();
                    break;
                case "f":
                    Attack();
                    break;
            }
            Move(direction);
        }
    }
}
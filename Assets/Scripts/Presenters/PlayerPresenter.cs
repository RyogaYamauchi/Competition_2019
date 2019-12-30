using Scripts.Models;
using Scripts.Views;
using UnityEngine;

namespace Scripts.Presenters
{
    public interface IPlayerPresenter
    {
        IPlayerModel PlayerModel { get; }
        float MoveForceMultiplier { get; }
        float MoveSpeed { get; }
        float JumpPower { get; }
        void Move(Vector2 direction);
        void Jump();
        void Attack();
        void GetInput(InputViewModel inputViewModel);
        void UpdatePos(Vector3 transformPosition);
    }

    public class PlayerPresenter : IPlayerPresenter
    {
        private PlayerView _playerView = default;
        public IPlayerModel PlayerModel => GameModel.Instance.PlayerModel;

        public float MoveForceMultiplier => PlayerModel.MoveForceMultiplier;
        public float MoveSpeed => PlayerModel.MoveSpeed;
        public float JumpPower => PlayerModel.JumpPower;

        public PlayerPresenter(PlayerView playerView)
        {
            _playerView = playerView;
            playerView.Init(this);
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
            _playerView.Jump();
        }

        public void Attack()
        {
            Debug.Log("Attack");
            _playerView.Attack();
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

        public void UpdatePos(Vector3 transformPosition)
        {
            PlayerModel.UpdatePos(transformPosition);
        }
    }
}
using Scripts.Models;
using Scripts.Views;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Presenters
{
    public interface IPlayerPresenter
    {
        float GetMoveForceMultiplier();
        float MoveSpeed { get; }
        float JumpPower { get; }
        int Direction { get; }
        void Move(Vector2 direction);
        void Jump();
        void Attack();
        void GetInput();
        void UpdatePos(Vector3 transformPosition);
    }

    public class PlayerPresenter :PresenterBase, IPlayerPresenter
    {
        /// <summary>
        /// View
        /// </summary>
        private PlayerView _playerView = default;
        
        /// <summary>
        /// Model
        /// </summary>
        private IPlayerModel _playerModel => GameModel.Instance.PlayerModel;
        
        /// <summary>
        /// Presenter
        /// </summary>
        public IInputPresenter InputPresenter => GamePresenter.Instance.InputPresenter;

        public float MoveSpeed => _playerModel.MoveSpeed;
        public float JumpPower => _playerModel.JumpPower;
        public int Direction => _playerModel.Direction;

        public PlayerPresenter(PlayerView playerView)
        {
            _playerView = playerView;
            playerView.Init(this);
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

        public void AttackProjectile()
        {
            Debug.Log("飛び道具攻撃！！");
            _playerView.AttackProjectile();
        }

        public async void GetInput()
        {
            while (true)
            {
                var viewModel = InputPresenter.GetInput();
                var direction = viewModel.GetDirection();
                var inputString = viewModel.GetInput();
                switch (inputString)
                {
                    case "c":
                        Jump();
                        break;
                    case "f":
                        Attack();
                        break;
                    case "r":
                        AttackProjectile();
                        break;
                }

                Move(direction);
                await UniTask.DelayFrame(1);
            }
        }

        public void UpdatePos(Vector3 transformPosition)
        {
            _playerModel.UpdatePos(transformPosition);
        }

        public float GetMoveForceMultiplier()
        {
            return _playerModel.MoveForceMultiplier;
        }

    }
}
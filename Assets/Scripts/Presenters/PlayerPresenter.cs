using Scripts.Models;
using Scripts.Views;
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

        public void Move(float direction)
        {
            _playerView.Move(direction);
        }

        public void Jump(bool jump)
        {
            if (jump)
            {
                _playerView.Jump();
            }
        }
    }
}
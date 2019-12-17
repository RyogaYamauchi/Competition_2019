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

        public void Move(Vector2 direction)
        {
            _playerView.Move(direction);
        }
    }
}
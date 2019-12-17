using Framework;
using Scripts.Presenters;
using UnityEngine;

namespace Scripts.Views
{
    public class GameView : ViewBase
    {
        [SerializeField]
        private InputManagerView _inputManagerView = default;

        [SerializeField] private PlayerView _playerView = default;
        private void Awake()
        {
            DontDestroyOnLoad(this);
            GamePresenter.Instance.Init(this,_inputManagerView,_playerView);
        }
    }
}
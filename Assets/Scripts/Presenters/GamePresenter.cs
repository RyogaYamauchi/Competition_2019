using Scripts.Models;
using Scripts.Views;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Playables;

namespace Scripts.Presenters
{
    public class GamePresenter
    {
        public static GamePresenter Instance { get; } = new GamePresenter();
        public InputPresenter InputPresenter { get; private set; }
        public PlayerPresenter PlayerPresenter { get; private set; }
        public GameView GameView { get; private set; }

        private bool _isEnableInput;

        public void Init(GameView gameView, InputManagerView inputManagerView, PlayerView playerView)
        {
            GameView = gameView;
            PlayerPresenter = new PlayerPresenter(playerView);
            InputPresenter = new InputPresenter(inputManagerView);
            StartGame().Forget();
        }

        private async UniTask StartGame()
        {
            Debug.Log("GameStart!!");
            SetEnableInput(true);
            while (_isEnableInput)
            {
                var inputViewModel = InputPresenter.GetInput();
                PlayerPresenter.GetInput(inputViewModel);
                await UniTask.DelayFrame(1);
            }
        }

        public void SetEnableInput(bool enable)
        {
            _isEnableInput = enable;
        }
    }
}
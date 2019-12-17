using Scripts.Views;
using UniRx.Async;
using UnityEngine;

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
                var direction = InputPresenter.GetInput();
                var jump = InputPresenter.GetJump();
                PlayerPresenter.Move(direction);
                PlayerPresenter.Jump(jump);
                await UniTask.DelayFrame(1);
            }
        }

        public void SetEnableInput(bool enable)
        {
            _isEnableInput = enable;
        }

        public void GetDirection(float direction)
        {
           PlayerPresenter.Move(direction);
        }
    }
}
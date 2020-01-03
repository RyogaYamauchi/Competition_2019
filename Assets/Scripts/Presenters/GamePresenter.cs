using System;
using Scripts.Views;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Presenters
{
    public class GamePresenter
    {
        public static GamePresenter Instance { get; } = new GamePresenter();
        public InputPresenter InputPresenter { get; private set; }
        public IPlayerPresenter PlayerPresenter { get; private set; }
        public IEnemyPresenter EnemyPresenter { get; private set; }
        public ICameraPresenter CameraPresenter { get; private set; }
        public IUIPresenter UIPresenter { get; private set; }
        public GameView GameView { get; private set; }
        public bool IsEnableInput => InputPresenter.IsEnableInput;


        public async void Init(GameView gameView, InputManagerView inputManagerView, PlayerView playerView,
            CameraManagerView cameraManagerView, UIManagerView uiManagerView)
        {
            GameView = gameView;
            PlayerPresenter = new PlayerPresenter(playerView);
            InputPresenter = new InputPresenter(inputManagerView);
            EnemyPresenter = new EnemyPresenter();
            CameraPresenter = new CameraPresenter(cameraManagerView);
            UIPresenter = new UIPresenter(uiManagerView);
            await LoadTalkAsync(StartGame, 1);

            EnemyPresenter.SpawnEnemy(10, 1);
        }

        private void StartGame()
        {
            Debug.Log("GameStart!!");
            PlayerPresenter.GetInput();
            CameraPresenter.IsEnableMove = true;
            CameraPresenter.UpdatePos();
        }

        public async UniTask LoadTalkAsync(Action callback, int id)
        {
            await UIPresenter.Open(callback, id);
        }
    }
}
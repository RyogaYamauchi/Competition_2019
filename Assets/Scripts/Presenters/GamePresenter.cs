using System;
using Scripts.Views;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Presenters
{
    public class GamePresenter : PresenterBase
    {
        /// <summary>
        /// シングルトンなインスタンス
        /// </summary>
        public static GamePresenter Instance { get; } = new GamePresenter();
        
        /// <summary>
        /// Presenters
        /// </summary>
        public IInputPresenter InputPresenter { get; private set; }
        public IPlayerPresenter PlayerPresenter { get; private set; }
        public IEnemyPresenter EnemyPresenter { get; private set; }
        public ICameraPresenter CameraPresenter { get; private set; }
        public IUIPresenter UIPresenter { get; private set; }
        public IProjectilePresenter ProjectilePresenter { get; private set; }
        private GameView _gameView;
        
        /// <summary>
        /// 場所を保持するオブジェクト
        /// </summary>
        public GameObject EffectPoint => _gameView.EffectPoint;
        public GameObject EnemySpawnPoint => _gameView.EnemySpawnPoint;


        public async void Init(GameView gameView, InputManagerView inputManagerView, PlayerView playerView,
            CameraManagerView cameraManagerView, UIManagerView uiManagerView)
        {
            _gameView = gameView;
            PlayerPresenter = new PlayerPresenter(playerView);
            InputPresenter = new InputPresenter(inputManagerView);
            EnemyPresenter = new EnemyPresenter();
            CameraPresenter = new CameraPresenter(cameraManagerView);
            UIPresenter = new UIPresenter(uiManagerView);
            ProjectilePresenter = new ProjectilePresenter();
//            await LoadTalkAsync(StartGame, 1);
           StartGame();
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

        public GameObject CreateGameObjectFromObject(string path)
        {
            var obj = Resources.Load(path);
            return _gameView.CreateGameObjectFromObject(obj);
        }
    }
}
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
        public ICameraPresenter CameraPresenter { get; private set; }
        public IUIPresenter UIPresenter { get; private set; }
        public IProjectilePresenter ProjectilePresenter { get; private set; }
        public IEnemiesPresenter EnemiesPresenter { get; private set; }
        public IEnemiesSpawnPointPresneter EnemiesSpawnPointPresenter { get; private set; }
        private GameView _gameView;
        
        /// <summary>
        /// 場所を保持するオブジェクト
        /// </summary>
        public GameObject EffectPoint => _gameView.EffectPoint;
        public GameObject EnemySpawnPoint => _gameView.EnemySpawnPoint;


        public async void Init(GameView gameView, InputManagerView inputManagerView, PlayerView playerView,
            CameraManagerView cameraManagerView, UIManagerView uiManagerView, EnemiesSpawnPointView enemiesSpawnPointView)
        {
            _gameView = gameView;
            PlayerPresenter = new PlayerPresenter(playerView);
            InputPresenter = new InputPresenter(inputManagerView);
            EnemiesPresenter = new EnemiesPresenter();
            CameraPresenter = new CameraPresenter(cameraManagerView);
            UIPresenter = new UIPresenter(uiManagerView);
            ProjectilePresenter = new ProjectilePresenter();
            EnemiesSpawnPointPresenter =new EnemiesSpawnPointPresenter(enemiesSpawnPointView);
            await LoadTalkAsync(StartGame, 1);
           StartGame();
//            EnemiesPresenter.SpawnStaticEnemy(0,10, 1);
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
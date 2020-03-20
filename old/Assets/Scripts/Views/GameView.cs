using Framework;
using Scripts.Presenters;
using UnityEngine;

namespace Scripts.Views
{
    public class GameView : ViewBase
    {
        /// <summary>
        /// Presenterと1:1のViewを管理するフィールド
        /// </summary>
        [SerializeField] private InputManagerView _inputManagerView = default;

        [SerializeField] private PlayerView _playerView = default;
        [SerializeField] private CameraManagerView _cameraManagerView = default;
        [SerializeField] private UIManagerView _uiManagerView = default;
        [SerializeField] private EnemiesSpawnPointView _enemiesSpawnPointView;

        //TODO : あとで消す
        public GameObject EnemySpawnPoint = default;
        public GameObject EffectPoint = default;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            GamePresenter.Instance.Init(this, _inputManagerView, _playerView, _cameraManagerView, _uiManagerView,_enemiesSpawnPointView);
        }

        public GameObject CreateGameObjectFromObject(Object obj)
        {
            return (GameObject) Instantiate(obj, EnemySpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
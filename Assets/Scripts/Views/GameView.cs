using Framework;
using Scripts.Presenters;
using UnityEngine;

namespace Scripts.Views
{
    public class GameView : ViewBase
    {
        [SerializeField] private InputManagerView _inputManagerView = default;

        [SerializeField] private PlayerView _playerView = default;
        [SerializeField] private CameraManagerView _cameraManagerView = default;
        [SerializeField] private UIManagerView _uiManagerView = default;

            //TODO : あとで消す
        [SerializeField] public GameObject EnemySpawnPoint = default;
        [SerializeField] public GameObject EffectPoint = default;



        private void Awake()
        {
            DontDestroyOnLoad(this);
            GamePresenter.Instance.Init(this, _inputManagerView, _playerView,_cameraManagerView,_uiManagerView);
        }

        public GameObject CreateGameObjectFromObject(Object obj)
        {
            return (GameObject) Instantiate(obj, EnemySpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
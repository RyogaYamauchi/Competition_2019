using ForTest;
using Framework;
using Scripts.Presenters;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class TrackingEnemyView : EnemyView
    {
        [SerializeField] private Rigidbody2D _rb;
        private Vector3 _playerPos;

        private void Start()
        {
            Debug.Log("start");
            GamePresenterForTest.Instance.EnemyView = this;
            GamePresenterForTest.Instance.InitEnemy();
        }

        public void Init(Vector3 playerPos, PresenterBase presenter = null, IViewModel enemyViewModel = null)
        {
            Presenter = presenter as IEnemyPresenter;
            _enemyViewModel = enemyViewModel is EnemyViewModel ? (EnemyViewModel) enemyViewModel : default;
            _slider.minValue = 0;
            _slider.maxValue = _enemyViewModel.Hp;
            _slider.value = _enemyViewModel.Hp;
            _playerPos = playerPos;
        }

        
    }
}
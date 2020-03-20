using System.Collections.Generic;
using ForTest;
using Framework;
using Scripts.Presenters;
using Scripts.ViewModels;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Views
{
    public class MoveRouteEnemyView : EnemyView
    {
        [SerializeField]
        private List<Vector3> _movePoints;

        private void Start()
        {
            Debug.Log("start");
//            GamePresenterForTest.Instance.EnemyView = this;
//            GamePresenterForTest.Instance.InitEnemy();
        }

        public void Init(List<Vector3> movePoints, PresenterBase presenter = null, IViewModel enemyViewModel = null)
        {
            Presenter = presenter as IEnemyPresenter;
            _enemyViewModel = enemyViewModel is EnemyViewModel ? (EnemyViewModel) enemyViewModel : default;
            _slider.minValue = 0;
            _slider.maxValue = _enemyViewModel.Hp;
            _slider.value = _enemyViewModel.Hp;
            _movePoints = movePoints;
            MoveStart();
        }

        private async void MoveStart()
        {
            Debug.Log("moveStart");

            var enumerator = _movePoints.GetEnumerator();
            while (enumerator.MoveNext())
            {
                await Move(transform.position, enumerator.Current);
            }
            MoveStart();
        }

        private async UniTask Move(Vector3 start, Vector3 end)
        {
            for (var i = 0f; i < 1.0f; i += 0.01f)
            {
                transform.position = Vector3.Lerp(start, end, i);
                await UniTask.Delay(10);
            }
        }
    }
}
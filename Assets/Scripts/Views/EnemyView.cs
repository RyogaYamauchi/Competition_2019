using Framework;
using Scripts.Presenters;
using Scripts.ViewModels;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Views
{
    public class EnemyView : ViewBase
    {
        /// <summary>
        /// UI用フィールド
        /// </summary>
        [SerializeField] private Slider _slider;

        /// <summary>
        /// ViewModel
        /// </summary>
        private EnemyViewModel _enemyViewModel;

        /// <summary>
        /// Presenter
        /// </summary>
        public IEnemyPresenter Presenter { get; private set; }

        public override void Init(PresenterBase presenter = null, IViewModel enemyViewModel = null)
        {
            Presenter = presenter as IEnemyPresenter;
            _enemyViewModel = enemyViewModel is EnemyViewModel ? (EnemyViewModel) enemyViewModel : default;
            _slider.minValue = 0;
            _slider.maxValue = _enemyViewModel.Hp;
            _slider.value = _enemyViewModel.Hp;
        }

        public async void Damage(int num)
        {
            _enemyViewModel = Presenter.Damage(_enemyViewModel.Id, num);
            await DamageAnimation(num);
            if (_enemyViewModel.Hp <= 0)
            {
                Dead().Forget();
            }
        }

        public async UniTask DamageAnimation(int num)
        {
            var current = _enemyViewModel.Hp;
            for (int i = current; i > current - num; i--)
            {
                if (i <= 0)
                {
                    return;
                }

                _slider.value = i;
                await UniTask.DelayFrame(5);
            }
        }

        public async UniTask Dead()
        {
            await DeadAnimation();
            Destroy(gameObject);
        }

        private async UniTask DeadAnimation()
        {
        }
    }
}
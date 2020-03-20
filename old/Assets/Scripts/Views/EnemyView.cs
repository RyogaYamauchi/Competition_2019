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
        [SerializeField] protected Slider _slider;

        [SerializeField] protected Text _debugText;
        


        /// <summary>
        /// ViewModel
        /// </summary>
        protected EnemyViewModel _enemyViewModel;

        /// <summary>
        /// Presenter
        /// </summary>
        protected IEnemyPresenter Presenter { get; set; }

        public Vector2 Direction;

        protected bool _isDead;
        
        
        protected void Init(PresenterBase presenter = null, IViewModel enemyViewModel = null)
        {
            Presenter = presenter as IEnemyPresenter;
            _enemyViewModel = enemyViewModel is EnemyViewModel ? (EnemyViewModel) enemyViewModel : default;
            _slider.minValue = 0;
            _slider.maxValue = _enemyViewModel.Hp;
            _slider.value = _enemyViewModel.Hp;
        }

        public async void Damage(int num)
        {
            Presenter.Damage(_enemyViewModel.Id, num);
            _enemyViewModel = Presenter.GetViewModel();
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
            _isDead = true;
            await DeadAnimation();
            Destroy(gameObject);
        }

        private async UniTask DeadAnimation()
        {
        }
        
    }
}
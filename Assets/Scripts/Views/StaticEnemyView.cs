using System.Collections.Generic;
using Framework;
using Scripts.Presenters;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class StaticEnemyView : EnemyView
    {
        public void Init(PresenterBase presenter = null, IViewModel enemyViewModel = null)
        {
            Presenter = presenter as IEnemyPresenter;
            _enemyViewModel = enemyViewModel is EnemyViewModel ? (EnemyViewModel) enemyViewModel : default;
            _slider.minValue = 0;
            _slider.maxValue = _enemyViewModel.Hp;
            _slider.value = _enemyViewModel.Hp;
        }
    }
}
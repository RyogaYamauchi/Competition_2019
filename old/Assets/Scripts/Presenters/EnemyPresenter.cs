using System.Collections.Generic;
using System.Linq;
using Scripts.Models;
using Scripts.ViewModels;
using Scripts.Views;
using UnityEngine;
using UnityEngine.Assertions;

namespace Scripts.Presenters
{
    public interface IEnemyPresenter
    {
        

        void Damage(int id, int damage);
        int GetHp();
        EnemyViewModel GetViewModel();
    }

    public class EnemyPresenter : PresenterBase, IEnemyPresenter
    {

        private EnemyModel _model;
        public EnemyPresenter(EnemyModel model)
        {
            _model = model;
        }
        public void Damage(int id, int damage)
        {
            _model.Damage(damage);
        }

        public int GetHp()
        {
            return _model.Hp;
        }

        public EnemyViewModel GetViewModel()
        {
            return _model.GetViewModel();
        }
    }
}
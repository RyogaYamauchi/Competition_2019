using System.Collections.Generic;
using Scripts.Models;
using Scripts.Presenters;
using Scripts.ViewModels;
using Scripts.Views;
using UnityEngine;

namespace ForTest
{
    public class GamePresenterForTest
    {
        public static GamePresenterForTest Instance { get; }= new GamePresenterForTest();
        
        public EnemyPresenter EnemyPresenter = new EnemyPresenter();

        public TrackingEnemyView EnemyView;

        public void InitEnemy()
        {
            var viewModel = new EnemyViewModel(1,10,1);
            var pos = GameModel.Instance.PlayerModel.GetPosition();
            EnemyView.Init(pos,EnemyPresenter,viewModel);
        }
    }
}
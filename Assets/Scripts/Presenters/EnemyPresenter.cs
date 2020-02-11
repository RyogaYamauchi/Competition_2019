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
        void SpawnMoveRouteEnemy(int hp, int attack, Vector3 direction);
        EnemyViewModel Damage(int id, int damage);
    }

    public class EnemyPresenter : PresenterBase, IEnemyPresenter
    {
        /// <summary>
        /// Model
        /// </summary>
        private EnemiesModel _enemiesModel = GameModel.Instance.EnemiesModel;
        public void SpawnMoveRouteEnemy(int hp, int attack, Vector3 direction)
        {
            var gameObject = GamePresenter.Instance.CreateGameObjectFromObject("Prefabs/MoveRouteEnemy");
            var cnt = _enemiesModel.GetEnemiesCount();
            var enemyModel = new EnemyModel(cnt + 1, hp, attack);
            _enemiesModel.AddEnemy(enemyModel);
            var viewModel = enemyModel.GetViewModel();
            var enemyView = gameObject.GetComponent<MoveRouteEnemyView>();
            var routes = new List<Vector3>();
            enemyView.Init(routes,this, viewModel);
            enemyView.transform.SetParent(GamePresenter.Instance.EnemySpawnPoint.transform);
        }

        public void SpawnStaticEnemy(int hp, int attack)
        {
            var gameObject = GamePresenter.Instance.CreateGameObjectFromObject("Prefabs/StaticEnemy");
            var cnt = _enemiesModel.GetEnemiesCount();
            var enemyModel = new EnemyModel(cnt + 1, hp, attack);
            _enemiesModel.AddEnemy(enemyModel);
            var viewModel = enemyModel.GetViewModel();
            var enemyView = gameObject.GetComponent<StaticEnemyView>();
            enemyView.Init(this, viewModel);
            enemyView.transform.SetParent(GamePresenter.Instance.EnemySpawnPoint.transform);
        }


        public void SpawnTrackingEnemy(int hp, int attack)
        {
            var gameObject = GamePresenter.Instance.CreateGameObjectFromObject("Prefabs/MoveRouteEnemy");
            var cnt = _enemiesModel.GetEnemiesCount();
            var enemyModel = new EnemyModel(cnt + 1, hp, attack);
            _enemiesModel.AddEnemy(enemyModel);
            var viewModel = enemyModel.GetViewModel();
            var enemyView = gameObject.GetComponent<TrackingEnemyView>();
            enemyView.Init(GameModel.Instance.PlayerModel.GetPosition(),this, viewModel);
            enemyView.transform.SetParent(GamePresenter.Instance.EnemySpawnPoint.transform);
        }

        public void DespawnEnemy(int id)
        {
            _enemiesModel.RemoveEnemy(id);
        }

        public EnemyViewModel Damage(int id, int damage)
        {
            var enemy = _enemiesModel.EnemyViewList.FirstOrDefault(g => g.Id == id);
            Assert.IsNotNull(enemy);
            _enemiesModel.Damage(enemy, damage);
            return enemy.GetViewModel();
        }
    }
}
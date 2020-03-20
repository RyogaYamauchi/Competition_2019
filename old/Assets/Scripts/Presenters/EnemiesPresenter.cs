using System.Collections.Generic;
using Scripts.Models;
using Scripts.Views;
using UnityEngine;

namespace Scripts.Presenters
{
    public interface IEnemiesPresenter
    {
        void SpawnMoveRouteEnemy(int id, int hp, int attack);
        void SpawnStaticEnemy(int id, int hp, int attack);
    }

    public class EnemiesPresenter : IEnemiesPresenter
    {
        /// <summary>
        /// Model
        /// </summary>
        private EnemiesModel _enemiesModel = GameModel.Instance.EnemiesModel;


        public void SpawnMoveRouteEnemy(int id, int hp, int attack)
        {
            var gameObject = GamePresenter.Instance.CreateGameObjectFromObject("Prefabs/Enemy/MoveRouteEnemy");
            var cnt = _enemiesModel.GetEnemiesCount();
            var enemyModel = new EnemyModel(cnt + 1, hp, attack);
            _enemiesModel.AddEnemy(enemyModel);
            var viewModel = enemyModel.GetViewModel();
            var enemyView = gameObject.GetComponent<MoveRouteEnemyView>();
            var routes = new List<Vector3>();
//            enemyView.Init(routes, new EnemyPresenter(enemyModel), viewModel);
//            enemyView.transform.SetParent(GamePresenter.Instance.EnemySpawnPoint.transform);
        }

        public void SpawnStaticEnemy(int id, int hp, int attack)
        {
            var gameObject = GamePresenter.Instance.CreateGameObjectFromObject("Prefabs/Enemy/StaticEnemy");
            var cnt = _enemiesModel.GetEnemiesCount();
            var enemyModel = new EnemyModel(cnt + 1, hp, attack);
            _enemiesModel.AddEnemy(enemyModel);
            var viewModel = enemyModel.GetViewModel();
            var enemyView = gameObject.GetComponent<StaticEnemyView>();
//            enemyView.Init(new EnemyPresenter(enemyModel), viewModel);
//            var spawnPoint = GamePresenter.Instance.EnemiesSpawnPointPresenter.GetSpawnPoint(id);
//            enemyView.transform.SetParent(spawnPoint.transform);
//            enemyView.transform.position = Vector3.zero;
        }

        public void SpawnTrackingEnemy(int id, int hp, int attack)
        {
            var gameObject = GamePresenter.Instance.CreateGameObjectFromObject("Prefabs/Enemy/MoveRouteEnemy");
            var cnt = _enemiesModel.GetEnemiesCount();
            var enemyModel = new EnemyModel(cnt + 1, hp, attack);
            _enemiesModel.AddEnemy(enemyModel);
            var viewModel = enemyModel.GetViewModel();
            var enemyView = gameObject.GetComponent<TrackingEnemyView>();
            enemyView.Init(GameModel.Instance.PlayerModel.GetPosition(), new EnemyPresenter(enemyModel), viewModel);
            enemyView.transform.SetParent(GamePresenter.Instance.EnemySpawnPoint.transform);
        }
        
        public void DespawnEnemy(int id)
        {
            _enemiesModel.RemoveEnemy(id);
        }
    }
}
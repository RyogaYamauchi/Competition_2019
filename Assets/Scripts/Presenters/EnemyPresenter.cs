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
        void SpawnEnemy(int hp, int attack);
        EnemyViewModel Damage(int id, int damage);
    }

    public class EnemyPresenter : IEnemyPresenter
    {
        private EnemiesModel _enemiesModel = GameModel.Instance.EnemiesModel;

        public void SpawnEnemy(int hp, int attack)
        {
            var obj = Resources.Load("Prefabs/Enemy");
            var gameObject = GamePresenter.Instance.GameView.CreateGameObjectFromObject(obj);
            var cnt = _enemiesModel.GetEnemiesCount();
            var enemyModel = new EnemyModel(cnt + 1, hp, attack);
            _enemiesModel.AddEnemy(enemyModel);
            var viewModel = enemyModel.GetViewModel();
            var enemyView = gameObject.GetComponent<EnemyView>();
            enemyView.Init(this, viewModel);
            enemyView.transform.SetParent(GamePresenter.Instance.GameView.EnemySpawnPoint.transform);
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
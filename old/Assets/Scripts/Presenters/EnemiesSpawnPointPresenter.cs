using Scripts.Views;
using UnityEngine;

namespace Scripts.Presenters
{

    public interface IEnemiesSpawnPointPresneter
    {
        EnemySpawnPointView GetSpawnPoint(int index);
    }
    public class EnemiesSpawnPointPresenter : IEnemiesSpawnPointPresneter
    {
        private EnemiesSpawnPointView _view;
        public EnemiesSpawnPointPresenter(EnemiesSpawnPointView view)
        {
            _view = view;
        }

        public EnemySpawnPointView GetSpawnPoint(int index)
        {
            return _view.list[index];
        }
    }
}
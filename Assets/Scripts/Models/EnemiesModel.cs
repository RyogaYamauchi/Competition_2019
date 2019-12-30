using System.Linq;
using Boo.Lang;

namespace Scripts.Models
{
    public class EnemiesModel
    {
        public List<EnemyModel> EnemyViewList { get; } = new List<EnemyModel>();

        public void AddEnemy(EnemyModel enemyModel)
        {
            EnemyViewList.Add(enemyModel);
        }

        public void RemoveEnemy(int id)
        {
            var enemy = EnemyViewList.FirstOrDefault(g => g.Id == id);
            EnemyViewList.Remove(enemy);
        }

        public int GetEnemiesCount()
        {
            return EnemyViewList.Count;
        }

        public void Damage(EnemyModel enemyModel, int num)
        {
            enemyModel.Damage(num);
        }
    }
}
using Scripts.ViewModels;

namespace Scripts.Models
{
    public class EnemyModel
    {
        public int Id { get; }
        public int Hp { get; private set; }
        public int Attack { get; private set; }

        public EnemyModel(int id, int hp, int attack)
        {
            Id = id;
            Hp = hp;
            Attack = attack;
        }
        public EnemyViewModel GetViewModel()
        {
            return new EnemyViewModel(Id,Hp, Attack);
        }

        public void Damage(int num)
        {
            Hp -= num;
            if (Hp <= 0)
            {
                Hp = 0;
            }
        }
    }
}
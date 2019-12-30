namespace Scripts.ViewModels
{
    public readonly struct EnemyViewModel
    {
        public readonly int Id;
        public readonly int Hp;
        public readonly int Attack;

        public EnemyViewModel(int id, int hp, int attack)
        {
            Id = id;
            Hp = hp;
            Attack = attack;
        }
    }
}
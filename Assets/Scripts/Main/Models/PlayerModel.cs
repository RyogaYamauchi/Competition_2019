using UniRx;

namespace Models
{
    public class PlayerModel
    {
        public IReadOnlyReactiveProperty<int> Hp => _hp;
        public IReadOnlyReactiveProperty<int> MaxHp => _maxHp;

        private ReactiveProperty<int> _hp;
        private ReactiveProperty<int> _maxHp;

        public int Attack { get; }

        public PlayerModel(int hp, int attack)
        {
            Attack = attack;
            _maxHp = new ReactiveProperty<int>(hp);
            _hp = new ReactiveProperty<int>(hp);
        }

        public void SubHp(int hp)
        {
            _hp.Value -= hp;
        }

        public void AddHp(int hp)
        {
            _hp.Value += hp;
        }
    }
}
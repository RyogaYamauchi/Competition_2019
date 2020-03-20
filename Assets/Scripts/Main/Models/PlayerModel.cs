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
        public bool IsDead { get; set; }

        public PlayerModel(int hp, int attack)
        {
            Attack = CheckUnderZero(attack);
            _maxHp = new ReactiveProperty<int>(CheckUnderZero(hp));
            _hp = new ReactiveProperty<int>(CheckUnderZero(hp));
        }

        public void SubHp(int hp)
        {
            if (CheckDead(hp))
            {
                return;
            }

            _hp.Value -= hp;
        }

        public void AddHp(int hp)
        {
            if (CheckDead(hp))
            {
                return;
            }

            _hp.Value += hp;
        }

        private bool CheckDead(int hp)
        {
            if (_hp.Value - hp <= 0 || _hp.Value + hp < 0)
            {
                _hp.Value = 0;
                IsDead = true;
                return true;
            }

            return false;
        }

        private int CheckUnderZero(int num)
        {
            if (num <= 0) return 0;
            return num;
        }
    }
}
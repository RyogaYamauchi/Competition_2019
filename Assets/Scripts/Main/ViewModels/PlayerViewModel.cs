using Models;
using UniRx;
using Views;

namespace ViewModels
{
    public struct PlayerViewModel
    {
        public readonly IReadOnlyReactiveProperty<int> Hp;
        public readonly IReadOnlyReactiveProperty<int> MaxHp;

        public PlayerViewModel(PlayerModel model)
        {
            Hp = model.Hp;
            MaxHp = model.MaxHp;
        }
    }
}
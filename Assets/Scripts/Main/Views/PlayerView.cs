using Framework;
using Presenters;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using ViewModels;
using Zenject;

namespace Views
{
    [PrefabPathAttribute("Prefabs/PlayerView")]
    public class PlayerView : ViewBase
    {
        [SerializeField] private Slider _slider = default;
        private PlayerPresenter _presenter;

        [Inject]
        private void Construct(PlayerPresenter presenter)
        {
            _presenter = presenter;
            _presenter.View = this;
        }

        private void Start()
        {
            _presenter.Start();
        }

        public void Show(PlayerViewModel viewModel)
        {
            viewModel.MaxHp.Subscribe(max => _slider.maxValue = max).AddTo(this);
            viewModel.Hp.Subscribe(hp => { _slider.value = hp; }).AddTo(this);
        }
    }
}
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
        [SerializeField] private Rigidbody2D _rigidbody;
        private PlayerPresenter _presenter;
        
        private float _moveForceMultiplier = 60f;
        private Vector2 _moveVector;
        private float _moveSpeed = 15;

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

        public void Move(float direction)
        {
            _moveVector = Vector2.zero;
            _moveVector.x = _moveSpeed * direction;
            var velocity = _rigidbody.velocity;
            _rigidbody.AddForce(_moveForceMultiplier * (_moveVector - velocity));
        }
    }
}
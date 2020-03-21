using System;
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
        private float _jumpPower = 500f;

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
            _rigidbody.AddForce(new Vector2(_moveForceMultiplier * (_moveVector - velocity).x, 0f));
        }

        public void Jump()
        {
            if (_rigidbody.velocity.y > 0.01f || _rigidbody.velocity.y < -0.01f) return;

            _rigidbody.AddForce(new Vector2(0, _jumpPower));
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                var itemView = other.gameObject.GetComponent<ItemView>();
                _presenter.AddItem(itemView.GetId());
                Destroy(itemView.gameObject);
            }
        }
    }
}
using Framework;
using Repository;
using Scripts.Models;
using Scripts.Presenters;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Views
{
    public class PlayerView : ViewBase
    {
        [SerializeField] private Rigidbody2D _rigidbody = default;
        [SerializeField] private WeaponView _weaponView = default;
        [SerializeField] private SpriteRenderer _spriteRenderer = default;
        
        public IPlayerPresenter Presenter { get; private set; }

        public float MoveForceMultiplier => Presenter.MoveForceMultiplier;
        public float MoveSpeed => Presenter.MoveSpeed;
        public float _jumpPower => Presenter.JumpPower;
        
        private AnimationEnum _animation;
        private bool _isAnimating;

        
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="presenter"></param>
        public void Init(IPlayerPresenter presenter)
        {
            Presenter = presenter;
        }

        private void Start()
        {
            _animation = AnimationEnum.PlayerIdling;
            AnimationStateMachine().Forget();
        }

        private async UniTask AnimationStateMachine()
        {
            while (true)
            {
                await UniTask.DelayFrame(1);
                if (!_isAnimating)
                {
                    switch (_animation)
                    {
                        case AnimationEnum.PlayerAttack3:
                            await AttackAnimation();
                            break;
                        case AnimationEnum.PlayerBlocking:
                            Blocking();
                            break;
                        case AnimationEnum.PlayerJumpming:
                            Jump();
                            break;
                        case AnimationEnum.PlayerWalking:
                            break;
                        case AnimationEnum.PlayerIdling:
                            await IdlingAnimation();
                            break;
                    }
                }
            }
        }

        public void Jump()
        {
            _rigidbody.AddForce(Vector3.up * _jumpPower);
            _animation = AnimationEnum.PlayerJumpming;
        }

        public void Attack()
        {
            Debug.Log("Attack!!");
            _animation = AnimationEnum.PlayerAttack3;
        }

        public void Blocking()
        {
            _animation = AnimationEnum.PlayerBlocking;
        }

        private async UniTask IdlingAnimation()
        {
            var standingSprites = AnimationRepository.GetSprites(AnimationEnum.PlayerIdling);
            var cnt = 0;
            var max = standingSprites.Length - 1;
            _isAnimating = true;
            while (true)
            {
                await UniTask.Delay(200);
                _spriteRenderer.sprite = standingSprites[cnt];
                cnt++;
                if (cnt > max)
                {
                    _isAnimating = false;
                    break;
                }
            }
        }


        private async UniTask AttackAnimation()
        {
            _weaponView.gameObject.SetActive(true);
            _weaponView.PlayAttackAnimation().Forget();
            var attackSprites = AnimationRepository.GetSprites(AnimationEnum.PlayerAttack3);
            _isAnimating = true;
            var max = attackSprites.Length;
            for (int i = 0; i < max; i++)
            {
                _spriteRenderer.sprite = attackSprites[i];
                await UniTask.Delay(50);
            }

            _isAnimating = false;
            _animation = AnimationEnum.PlayerIdling;
        }


        public Vector3 GetPosition()
        {
            return gameObject.transform.position;
        }

        public void Move(Vector2 direction)
        {
            var moveVector = Vector3.zero;
            moveVector.x = MoveSpeed * direction.x;
            //_moveVector.z = _moveSpeed * direction.y;
            moveVector.y = _rigidbody.velocity.y;
            var velocity = _rigidbody.velocity;
            var moveVector2D = new Vector2(moveVector.x, moveVector.y);
            _rigidbody.AddForce(MoveForceMultiplier * (moveVector2D - velocity));
        }
    }
}
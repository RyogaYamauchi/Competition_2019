using System;
using Framework;
using Scripts.Models;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Views
{
    public class PlayerView : ViewBase
    {
        [SerializeField] private Rigidbody2D _rigidbody = default;
        [SerializeField] private Image _image;
        [SerializeField] private WeaponView _weaponView;

        private float _moveForceMultiplier = 50f;
        private Vector3 _moveVector;
        private float _moveSpeed = 50;
        private Sprite[] _standingSprites;
        private Sprite[] _walkingSprites;
        private Sprite[] _attackSprite;
        private int _jumpPower = 10;
        private PlayerAnimationEnum _animation;
        private bool _isAnimating;

        private void Start()
        {
            _standingSprites = Resources.LoadAll<Sprite>("Sprites/Player/Standing");
            _attackSprite = Resources.LoadAll<Sprite>("Sprites/Player/Attack/Player");
            _animation = PlayerAnimationEnum.Idling;
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
                        case PlayerAnimationEnum.Attack:
                            await AttackAnimation();
                            break;
                        case PlayerAnimationEnum.Blocking:
                            await Blocking();
                            break;
                        case PlayerAnimationEnum.Jumpming:
                            await Jump();
                            break;
                        case PlayerAnimationEnum.Walking:
                            break;
                        case PlayerAnimationEnum.Idling:
                            await IdlingAnimation();
                            break;
                    }
                }
            }
        }

        public async UniTask Jump()
        {
            _rigidbody.AddForce(Vector3.up * _jumpPower);
            _animation = PlayerAnimationEnum.Jumpming;
        }

        public async UniTask Attack()
        {
            Debug.Log("Attack!!");
            _animation = PlayerAnimationEnum.Attack;
        }

        public async UniTask Blocking()
        {
            _animation = PlayerAnimationEnum.Blocking;
        }

        private async UniTask IdlingAnimation()
        {
            var cnt = 0;
            var max = _standingSprites.Length - 1;
            _isAnimating = true;
            while (true)
            {
                await UniTask.Delay(200);
                _image.sprite = _standingSprites[cnt];
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
            _weaponView.PlayAttackAnimation();
            var cnt = 0;
            var max = _attackSprite.Length - 1;
            _isAnimating = true;
            while (true)
            {
                await UniTask.Delay(30);
                _image.sprite = _attackSprite[cnt];
                cnt++;
                if (cnt > max)
                {
                    IdlingAnimation().Forget();
                    _isAnimating = false;
                    _animation = PlayerAnimationEnum.Idling;
                    break;
                }
            }
        }

        private async UniTask JumpingAnimation()
        {
        }

        public void SetPosition(Vector2 pos)
        {
            gameObject.transform.position = new Vector3(pos.x, pos.y, 0);
        }

        public Vector3 GetPosition()
        {
            return gameObject.transform.position;
        }

        public void Move(Vector2 direction)
        {
            _moveVector = Vector3.zero;
            _moveVector.x = _moveSpeed * direction.x;
            //_moveVector.z = _moveSpeed * direction.y;
            _moveVector.y = _rigidbody.velocity.y;
            var velocity = _rigidbody.velocity;
            var _moveVector2D = new Vector2(_moveVector.x,_moveVector.y);
            _rigidbody.AddForce(_moveForceMultiplier * (_moveVector2D - velocity));
        }
    }
}
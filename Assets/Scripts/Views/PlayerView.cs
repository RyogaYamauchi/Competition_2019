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
        [SerializeField] private Rigidbody _rigidbody = default;
        [SerializeField] private Image _image;

        private float _moveForceMultiplier = 50f;
        private Vector3 _moveVector;
        private float _moveSpeed = 50;
        private Sprite[] _standingSprites;
        private Sprite[] _walkingSprites;
        private Sprite[] _kenSprites;
        private int _jumpPower = 1000;
        private PlayerAnimationEnum _animation;
        private bool _isAnimating;

        private void Start()
        {            Debug.Log("aaaa");
            Debug.Log("aaaa");
            Debug.Log("aaaa");
            Debug.Log("aaaa");
            Debug.Log("aaaa");
            Debug.Log("aaaa");

            _standingSprites = Resources.LoadAll<Sprite>("");
            _kenSprites = Resources.LoadAll<Sprite>("");
            AnimationStateMachine(PlayerAnimationEnum.Idling).Forget();
            Debug.Log("aaaa");
            Debug.Log("aaaa");
            Debug.Log("aaaa");
            Debug.Log("aaaa");
            Debug.Log("aaaa");

        }

        private async UniTask AnimationStateMachine(PlayerAnimationEnum playerAnimationEnum)
        {
            if (!_isAnimating)
            {
                while (true)
                {
                    await IdlingAnimation();
                }
            }
            switch (playerAnimationEnum)
            {
                case PlayerAnimationEnum.Attack:
                    await Attack();
                    break;
                case PlayerAnimationEnum.Blocking:
                    break;

                case PlayerAnimationEnum.Jumpming:
                    await Jump();
                    break;
                case PlayerAnimationEnum.Walking:
                    break;
            }
        }

        public async UniTask Jump()
        {
            _rigidbody.AddForce(Vector3.up * _jumpPower);
            await AnimationStateMachine(PlayerAnimationEnum.Jumpming);
        }

        
        
        
        
        
        public async UniTask Attack()
        {
            Debug.Log("Attack!!");
            await AnimationStateMachine(PlayerAnimationEnum.Attack);
        }

        public async UniTask Blocking()
        {
            await AnimationStateMachine(PlayerAnimationEnum.Blocking);
        }

        private async UniTask IdlingAnimation()
        {
            var cnt = 0;
            var max = _standingSprites.Length - 1;
            while (true)
            {
                await UniTask.Delay(200);
                _image.sprite = _standingSprites[cnt];
                cnt++;
                if (cnt > max) cnt = 0;
            }

        }

        private async UniTask AttackAnimation()
        {
            _isAnimating = true;
            var cnt = 0;
            var max = _kenSprites.Length - 1;
            while (true)
            {
                await UniTask.Delay(100);
                _image.sprite = _kenSprites[cnt];
                cnt++;
                if (cnt > max)
                {
                    IdlingAnimation().Forget();
                    _isAnimating = false;
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
            _moveVector.z = _moveSpeed * direction.y;
            _moveVector.y = _rigidbody.velocity.y;
            var velocity = _rigidbody.velocity;
            _rigidbody.AddForce(_moveForceMultiplier * (_moveVector - velocity));
        }
    }
}
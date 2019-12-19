using System;
using Framework;
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
        private int _jumpPower = 1000;

        private void Start()
        {
            _standingSprites = Resources.LoadAll<Sprite>("Sprites/Player/Standing");
            StandingAnimation().Forget();
        }

        private async UniTask StandingAnimation()
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

        public void Jump()
        {
            Debug.Log("Jump");
            _rigidbody.AddForce(Vector3.up * _jumpPower);
        }
    }
}
using System;
using Framework;
using UnityEngine;

namespace Scripts.Views
{
    public class PlayerView : ViewBase
    {
        [SerializeField] private Rigidbody _rigidbody = default;

        private float _moveForceMultiplier = 50f;
        private Vector3 _moveVector;
        private Vector3 velocity;

        private float _moveSpeed = 5;

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
            velocity = _rigidbody.velocity;
            _rigidbody.AddForce(_moveForceMultiplier * (_moveVector - velocity));
        }
    }
}
using UnityEngine;

namespace Scripts.Models
{
    public interface IPlayerModel
    {
        float MoveForceMultiplier { get; }
        float MoveSpeed { get; }
        float JumpPower { get; }
        void SetPosition(Vector2 position);
        void SetPosition(float x, float y);
        Vector2 GetPosition();
    }

    public class PlayerModel : IPlayerModel
    {
        private Vector2 _position;

        public float MoveForceMultiplier { get; private set; } = 5f;
        public float MoveSpeed { get; private set; } = 5;
        public float JumpPower { get; private set; } = 100;

        public void SetMoveForceMultiplier(float num)
        {
            MoveForceMultiplier = num;
        }

        public void SetMoveSpeed(float num)
        {
            MoveSpeed = num;
        }

        public void SetJumpPower(float num)
        {
            JumpPower = num;
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public void SetPosition(float x, float y)
        {
            _position = new Vector2(x, y);
        }

        public Vector2 GetPosition()
        {
            return _position;
        }
    }
}
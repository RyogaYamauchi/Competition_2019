using UnityEngine;

namespace Scripts.Models
{
    interface IPlayerModel
    {
        void SetPosition(Vector2 position);
        void SetPosition(float x, float y);
        Vector2 GetPosition();
    }

    public class PlayerModel : IPlayerModel
    {
        private Vector2 _position;

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
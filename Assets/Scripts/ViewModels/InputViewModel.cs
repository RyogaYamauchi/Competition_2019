using UnityEngine;

namespace Scripts.Models
{
    public class InputViewModel
    {
        private string _input;
        private Vector2 _direction;

        public InputViewModel(string input, Vector2 direction)
        {
            _input = input;
            _direction = direction;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void SetInput(string input)
        {
            _input = input;
        }

        public string GetInput()
        {
            return _input;
        }

        public Vector2 GetDirection()
        {
            return _direction;
        }
    }
}
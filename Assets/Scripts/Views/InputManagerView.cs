using Framework;
using Scripts.Models;
using UnityEngine;

namespace Scripts.Views
{
    public class InputManagerView : ViewBase
    {
        private float _horizontalInput;
        private float _verticalInput;
        private string _inputString;

        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            _inputString = "";
            if (Input.GetKeyDown("o"))
            {
                _inputString = "o";
            }
            else if (Input.GetKeyDown("f"))
            {
                _inputString = "f";
            }
            else if (Input.GetKeyDown("c"))
            {
                _inputString = "c";
            }
        }
        

        public InputViewModel GetInput()
        {
            var direction = new Vector2(_horizontalInput,_verticalInput);
            return new InputViewModel(_inputString,direction);
        }

        public bool GetJump()
        {
            return Input.GetKeyDown("j");
        }

        public bool IsInput(string str)
        {
            return _inputString.Equals(str);
        }
    }
}
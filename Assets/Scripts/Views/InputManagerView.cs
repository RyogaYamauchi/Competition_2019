using Framework;
using Scripts.Models;
using UnityEngine;

namespace Scripts.Views
{
    public class InputManagerView : ViewBase
    {
        float _horizontalInput;
        float _verticalInput;

        public InputViewModel GetInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            var direction = new Vector2(_horizontalInput,_verticalInput);
            
            return new InputViewModel(Input.inputString,direction);
        }

        public bool GetJump()
        {
            return Input.GetKeyDown("j");
        }
    }
}
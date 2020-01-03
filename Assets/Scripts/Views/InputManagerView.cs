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
            var input = Input.inputString;
            if (Input.GetKeyDown("o"))
            {
                input = "o";
            }
            else if (Input.GetKeyDown("f"))
            {
                input = "f";
            }
            else if (Input.GetKeyDown("c"))
            {
                input = "c";
            }
            
            return new InputViewModel(input,direction);
        }

        public bool GetJump()
        {
            return Input.GetKeyDown("j");
        }
    }
}
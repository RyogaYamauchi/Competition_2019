using Framework;
using UnityEngine;

namespace Scripts.Views
{
    public class InputManagerView : ViewBase
    {
        float _horizontalInput;
        float _verticalInput;

        public Vector2 GetInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            var direction = new Vector2(_horizontalInput,_verticalInput);
            return direction;
        }

        public bool GetJump()
        {
            return Input.GetKeyDown("j");
        }
    }
}
using Framework;
using UnityEngine;

namespace Scripts.Views
{
    public class InputManagerView : ViewBase
    {
        float _horizontalInput;
        float _verticalInput;

        public float GetInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            return _horizontalInput;
        }

        public bool GetJump()
        {
            return Input.GetKey("j");
        }
    }
}
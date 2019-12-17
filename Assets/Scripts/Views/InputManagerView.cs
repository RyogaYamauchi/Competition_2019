using Framework;
using UnityEngine;
using UniRx.Async; //必要

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
            return new Vector2(_horizontalInput, _verticalInput);
        }
    }
}
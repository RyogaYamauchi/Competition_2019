using Scripts.Views;
using UnityEngine;

namespace Scripts.Presenters
{
    public class InputPresenter
    {
        private InputManagerView _inputManagerView;

        public InputPresenter(InputManagerView inputManagerView)
        {
            _inputManagerView = inputManagerView;
        }

        public Vector2 GetInput()
        {
            return _inputManagerView.GetInput();
        }

        public bool GetJump()
        {
            return _inputManagerView.GetJump();
        }
    }
}
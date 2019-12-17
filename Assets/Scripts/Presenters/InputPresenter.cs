using Scripts.Views;
using UniRx.Async;
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

        public float GetInput()
        {
            return _inputManagerView.GetInput();
        }

        public bool GetJump()
        {
            return _inputManagerView.GetJump();
        }
    }
}
using Scripts.Models;
using Scripts.Views;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Presenters
{
    public class InputPresenter
    {
        private InputManagerView _view;
        public bool IsEnableInput { get; private set; } = true;
        

        public InputPresenter(InputManagerView view)
        {
            _view = view;
        }

        public InputViewModel GetInput()
        {
            return _view.GetInput();
        }

        public async UniTask<bool> IsInputAsync(string need)
        {
            while (IsEnableInput)
            {
                await UniTask.DelayFrame(1);
                if (_view.IsInput(need))
                {
                    return true;
                }
            }

            return false;
        }

        public bool GetJump()
        {
            return _view.GetJump();
        }
    }
}
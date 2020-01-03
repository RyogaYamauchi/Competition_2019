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

        public async UniTask<InputViewModel> Start()
        {
            while (!IsEnableInput)
            {
                await UniTask.DelayFrame(1);
            }
            return GetInput();
        }

        public InputPresenter(InputManagerView view)
        {
            _view = view;
        }

        public InputViewModel GetInput()
        {
            return _view.GetInput();
        }

        public async UniTask<string> GetInputStringAsync(string need)
        {
            while (IsEnableInput)
            {
                await UniTask.DelayFrame(1);
                if (Input.inputString.Equals(need))
                {
                    
                    return Input.inputString;
                }
            }
            return null;
        }

        public bool GetJump()
        {
            return _view.GetJump();
        }
    }
}
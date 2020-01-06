using Scripts.Models;
using Scripts.Views;
using UniRx.Async;

namespace Scripts.Presenters
{
    public interface IInputPresenter
    {
        InputViewModel GetInput();
        UniTask<bool> IsInputAsync(string need);
    }

    public class InputPresenter : PresenterBase, IInputPresenter
    {
        /// <summary>
        /// View
        /// </summary>
        private InputManagerView _view;

        /// <summary>
        /// 外部からアクセスできるプロパティ
        /// </summary>
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
    }
}
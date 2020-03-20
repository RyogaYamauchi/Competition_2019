using Scripts.Views;
using UniRx.Async;

namespace Scripts.Presenters
{
    public interface ICameraPresenter
    {
        bool IsEnableMove { get; set; }
        void UpdatePos();
    }

    public class CameraPresenter : PresenterBase, ICameraPresenter
    {
        /// <summary>
        /// View
        /// </summary>
        private CameraManagerView CameraManagerView { get; }
        
        /// <summary>
        /// 外部からアクセスできるプロパティ
        /// </summary>
        public bool IsEnableMove { get; set; } = false;

        public CameraPresenter(CameraManagerView cameraManagerView)
        {
            CameraManagerView = cameraManagerView;
            cameraManagerView.Init(this);
        }

        public void UpdatePos()
        {
            CameraManagerView.UpdatePos().Forget();
        }
    }
}
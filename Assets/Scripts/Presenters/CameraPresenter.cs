using Scripts.Views;
using UniRx.Async;

namespace Scripts.Presenters
{
    public interface ICameraPresenter
    {
        
    }
    public class CameraPresenter : ICameraPresenter
    {
        public CameraManagerView CameraManagerView { get; private set; }
        public CameraPresenter(CameraManagerView cameraManagerView)
        {
            CameraManagerView = cameraManagerView;
            cameraManagerView.Init(this);
            cameraManagerView.UpdatePos().Forget();
        }
    }
}
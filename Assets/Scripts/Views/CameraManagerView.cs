using Scripts.Models;
using Scripts.Presenters;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Views
{
    public class CameraManagerView : MonoBehaviour
    {
        public ICameraPresenter Presenter { get; private set; }
        public IPlayerModel PlayerModel { get; private set; }
        public void Init(ICameraPresenter CameraPresenter)
        {
            Presenter = CameraPresenter;
            PlayerModel = GameModel.Instance.PlayerModel;
        }

        public async UniTask UpdatePos()
        {
            if (!Presenter.IsEnableMove) return;
            Vector2 playerPos = PlayerModel.GetPosition();
            Vector2 pos;
            while (true)
            {
                await UniTask.DelayFrame(1); 
                playerPos = PlayerModel.GetPosition();
                pos = gameObject.transform.position;
                gameObject.transform.position = Vector3.Lerp(new Vector3(pos.x,pos.y,-10), new Vector3(playerPos.x,playerPos.y,-10) , Time.deltaTime);
            }
        }
    }
}
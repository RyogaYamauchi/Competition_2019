using Framework;
using Scripts.Models;
using Scripts.Presenters;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Views
{
    public class CameraManagerView : ViewBase
    {
        /// <summary>
        /// Presenter
        /// </summary>
        private ICameraPresenter _presenter;
        
        /// <summary>
        /// Model
        /// </summary>
        private IPlayerModel _playerModel;
        
        
        public override void Init(PresenterBase presenter= null, IViewModel viewModel = null)
        {
            _presenter = presenter as ICameraPresenter;
            _playerModel = GameModel.Instance.PlayerModel;
        }

        public async UniTask UpdatePos()
        {
            if (!_presenter.IsEnableMove) return;
            Vector2 playerPos = _playerModel.GetPosition();
            Vector2 pos;
            while (true)
            {
                await UniTask.DelayFrame(1); 
                playerPos = _playerModel.GetPosition();
                pos = gameObject.transform.position;
                gameObject.transform.position = Vector3.Lerp(new Vector3(pos.x,pos.y,-10), new Vector3(playerPos.x,playerPos.y,-10) , Time.deltaTime);
            }
        }
    }
}
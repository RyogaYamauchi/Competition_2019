using System;
using System.Collections.Generic;
using Repository;
using Scripts.Models;
using Scripts.Views;
using UniRx.Async;
using UnityEngine;
using ViewModels;
using Object = UnityEngine.Object;

namespace Scripts.Presenters
{
    public interface IUIPresenter
    {
        UniTask Open(Action callback, int id);
        Object LoadSpritePrefab(int characterId);
        void GetInput();
        void Show();
        void Hide();
    }

    public class UIPresenter : PresenterBase, IUIPresenter
    {
        /// <summary>
        /// View
        /// </summary>
        public UIManagerView View { get; }
        
        /// <summary>
        /// Model
        /// </summary>
        public UIModel Model => GameModel.Instance.UiModel;
        
        /// <summary>
        /// Presenter
        /// </summary>
        public IInputPresenter InputPresenter => GamePresenter.Instance.InputPresenter;

        /// <summary>
        /// Repository
        /// </summary>
        private ITalkRepository _talkRepository => GameRepository.Instance.TalkRepository;

        public UIPresenter(UIManagerView uiManagerView)
        {
            View = uiManagerView;
            uiManagerView.Init(this);
        }

        public async UniTask Open(Action callback, int id)
        {
            var a = await _talkRepository.LoadTalk(id.ToString());
            List<TalkViewModel> talkViewModels = new List<TalkViewModel>();
            for (var i = 0; i < a.Length; i++)
            {
                talkViewModels.Add(new TalkViewModel(a[i]));
            }

            View.Set(callback, talkViewModels.ToArray());
            Show();
            GetInput();
        }

        public Object LoadSpritePrefab(int characterId)
        {
            var sprites = Resources.LoadAll("Sprites/Characters");
            var obj = sprites[0];
            return (GameObject) obj;
        }

        public async void GetInput()
        {
            var input = await InputPresenter.IsInputAsync("o");
            if (input)
            {
                View.GoAction();
                if (View.GetTalkCount > 0)
                {
                    GetInput();
                }
            }
        }

        public void Show()
        {
            View.GoAction();
            View.gameObject.SetActive(true);
        }

        public void Hide()
        {
            View.gameObject.SetActive(false);
        }
    }
}
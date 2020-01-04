using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UIPresenter : IUIPresenter
    {
        public UIManagerView View { get; }
        public UIModel Model => GameModel.Instance.UiModel;
        public InputPresenter InputPresenter => GamePresenter.Instance.InputPresenter;

        public UIPresenter(UIManagerView uiManagerView)
        {
            View = uiManagerView;
            uiManagerView.Init(this);
        }

        public async UniTask Open(Action callback, int id)
        {
            var a = await TalkRepository.LoadTalk(id.ToString());
            List<TalkViewModel> talkViewModels = new List<TalkViewModel>();
            for (var i = 0; i < a.Length; i++)
            {
                talkViewModels.Add(new TalkViewModel(a[i]));
            }
            View.Set(callback,talkViewModels.ToArray());
            Show();
            GetInput();
        }

        public Object LoadSpritePrefab(int characterId)
        {
            var sprites= Resources.LoadAll("Sprites/Characters");
            var obj = sprites[0];
            return (GameObject)obj;
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
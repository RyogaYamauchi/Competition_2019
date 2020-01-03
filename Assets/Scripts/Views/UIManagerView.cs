using System;
using System.Collections.Generic;
using Scripts.Presenters;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using ViewModels;

namespace Scripts.Views
{
    public class UIManagerView : MonoBehaviour
    {
        [SerializeField] private Image _characterImage;
        [SerializeField] private Text _characterName;
        [SerializeField] private Text _main;


        private TalkViewModel[] _talkModels;
        private int _current = 0;
        private Action _callback;
        public bool IsAnimation { get; private set; }
        public bool Skip;

        public IUIPresenter Presenter { get; private set; }
        public int GetTalkCount => _talkModels.Length;

        public void Init(IUIPresenter presenter)
        {
            Presenter = presenter;
        }

        public void Set(Action callback, TalkViewModel[] talkModels)
        {
            _talkModels = talkModels;
            _callback = callback;
        }

        public async void UpdateView(TalkViewModel talkViewModel)
        {
            _characterName.text = talkViewModel.CharacterName;
            _characterImage.sprite = Presenter.LoadSprite(talkViewModel.CharacterID);
            _current++;
            await MainTextAnimation(talkViewModel.MainText);
        }

        private async UniTask MainTextAnimation(string text)
        {
            var current = "";
            IsAnimation = true;
            for (int i = 0; i < text.Length; i++)
            {
                if (Skip)
                {
                    _main.text = text;
                    IsAnimation = false;
                    break;
                }
                current += text[i];
                _main.text = current;
                await UniTask.DelayFrame(5);
            }
            IsAnimation = false;
        }

        public void GoAction()
        {
            if (_current >= _talkModels.Length)
            {
                Finish();
                return;
            }

            if (IsAnimation)
            {
                Skip = true;
            }
            else
            {
                Skip = false;
                UpdateView(_talkModels[_current]);
            }
            
        }

        public void Finish()
        {
            _callback?.Invoke();
            gameObject.SetActive(false);
        }

    }
}
using System;
using Framework;
using Scripts.Presenters;
using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using ViewModels;

namespace Scripts.Views
{
    /// <summary>
    /// トークなどのUIを扱うView
    /// InitでPresenterを受け取りデータの受け渡しをする
    /// </summary>
    public class UIManagerView : ViewBase
    {
        /// <summary>
        /// Unityからアタッチするフィールド
        /// </summary>
        [SerializeField] private GameObject _characterImageRoot;
        [SerializeField] private Text _characterName;
        [SerializeField] private Text _main;

        /// <summary>
        /// トークをするための初期値を入れるフィールド
        /// </summary>
        private TalkViewModel[] _talkModels;
        private Action _callback;

        /// <summary>
        /// 現在のトーク状態などを保持するフィールド
        /// </summary>
        private int _current = 0;
        private bool _skip;

        /// <summary>
        /// 外部から状態をみれるフィールド
        /// </summary>
        public bool IsAnimation { get; private set; }
        public int GetTalkCount => _talkModels.Length;


        /// <summary>
        /// Presenter
        /// </summary>
        public IUIPresenter Presenter { get; protected internal set; }


        /// <summary>
        /// Initializer
        /// </summary>
        /// <param name="presenter"></param>
        public void Init(PresenterBase presenterBase = null, IViewModel viewModel = null)
        {
            Presenter = presenterBase as IUIPresenter;
        }

        public void Set(Action callback, TalkViewModel[] talkModels)
        {
            _talkModels = talkModels;
            _callback = callback;
        }

        private async void UpdateView(TalkViewModel talkViewModel)
        {
            _characterName.text = talkViewModel.CharacterName;
            var a = Presenter.LoadSpritePrefab(talkViewModel.CharacterID);
            CreateGameObjectFromObject(a, _characterImageRoot);
            _current++;
            await MainTextAnimation(talkViewModel.MainText);
        }

        private async UniTask MainTextAnimation(string text)
        {
            var current = "";
            IsAnimation = true;
            for (int i = 0; i < text.Length; i++)
            {
                if (_skip)
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
                _skip = true;
            }
            else
            {
                _skip = false;
                UpdateView(_talkModels[_current]);
            }
        }

        private void Finish()
        {
            _callback?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
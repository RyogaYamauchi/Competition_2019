using System;
using System.Collections.Generic;
using Scripts.Presenters;
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

        public IUIPresenter Presenter { get; private set; }

        public void Init(IUIPresenter presenter)
        {
            Presenter = presenter;
        }

        public void Set(Action callback, TalkViewModel[] talkModels)
        {
            _talkModels = talkModels;
            _callback = callback;
        }

        public void UpdateView(TalkViewModel talkViewModel)
        {
            _main.text = talkViewModel.MainText;
            _characterName.text = talkViewModel.CharacterName;
            _characterImage.sprite = Presenter.LoadSprite(talkViewModel.CharacterID);
            _current++;
            
        }

        public void GoAction()
        {
            if (_current >= _talkModels.Length)
            {
                Finish();
                return;
            }
            UpdateView(_talkModels[_current]);
            if (_talkModels.Length > 0)
            {
                Presenter.GetInput();
            }
        }

        public void Finish()
        {
            _callback?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
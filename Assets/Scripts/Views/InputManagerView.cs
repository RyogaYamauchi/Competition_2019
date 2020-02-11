using Framework;
using Scripts.Models;
using Scripts.Presenters;
using UnityEngine;

namespace Scripts.Views
{
    public class InputManagerView : ViewBase
    {
        /// <summary>
        /// InputViewModelを返すためのフィールド
        /// </summary>
        private float _horizontalInput;
        private float _verticalInput;
        private string _inputString;

        /// <summary>
        /// Presenter
        /// </summary>
        private IInputPresenter _inputPresenter;
        
        
        public void Init(PresenterBase presenterBase = null, IViewModel viewModel = null)
        {
            _inputPresenter = presenterBase as IInputPresenter;
        }

        private void Update()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            _inputString = "";
            if (Input.GetKeyDown("o"))
            {
                _inputString = "o";
            }
            else if (Input.GetKeyDown("f"))
            {
                _inputString = "f";
            }
            else if (Input.GetKeyDown("c"))
            {
                _inputString = "c";
            }
            else if (Input.GetKeyDown("r"))
            {
                _inputString = "r";
            }
        }
        

        public InputViewModel GetInput()
        {
            var direction = new Vector2(_horizontalInput,_verticalInput);
            return new InputViewModel(_inputString,direction);
        }

        public bool IsInput(string str)
        {
            return _inputString.Equals(str);
        }

       
    }
}
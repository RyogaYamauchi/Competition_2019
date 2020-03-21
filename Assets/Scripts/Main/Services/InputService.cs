using Main.MasterDatas;
using UniRx.Async;
using UnityEngine;

namespace Services
{
    public class InputService
    {
        private bool _canInput = false;

        public InputService()
        {
            StartToGetInput();
        }

        public InputType InputType { get; private set; }


        public async void StartToGetInput()
        {
            _canInput = true;
            while (_canInput)
            {
                await UniTask.Yield();
                switch (Input.inputString)
                {
                    case "a":
                        InputType = InputType.a;
                        break;
                    case "s":
                        InputType = InputType.s;
                        break;
                    case "w":
                        InputType = InputType.w;
                        break;
                    case "d":
                        InputType = InputType.d;
                        break;
                    default:
                        InputType = InputType.none;
                        break;
                }
            }
        }

        public void FinishToGetInput()
        {
            _canInput = false;
        }
    }
}
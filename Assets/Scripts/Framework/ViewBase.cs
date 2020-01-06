using System.Collections;
using System.Collections.Generic;
using Scripts.Presenters;
using UnityEngine;

namespace Framework
{
    public abstract class ViewBase : MonoBehaviour
    {
        protected GameObject CreateGameObjectFromObject(Object obj, GameObject parent)
        {
            var instace = (GameObject) Instantiate(obj, parent.transform.position, Quaternion.identity);
            instace.transform.SetParent(parent.transform);
            return instace;
        }

        public virtual void Init(PresenterBase presenterBase = null, IViewModel viewModel = null)
        {
            
        }


        /// <summary>
        /// ロード時に呼び出される
        /// </summary>
        protected virtual void OnLoad()
        {
        }

        /// <summary>
        /// 表示アニメーション用に使われる
        /// </summary>
        protected virtual void OnWillAppear()
        {
        }

        /// <summary>
        /// 表示する際に呼び出される
        /// </summary>
        protected virtual void OnAppear()
        {
        }

        /// <summary>
        /// 消えるアニメーション用に使われる
        /// </summary>
        protected virtual void OnWillDisappear()
        {
        }

        /// <summary>
        /// 消える際に呼び出される
        /// </summary>
        protected virtual void OnDisappear()
        {
        }
    }
}
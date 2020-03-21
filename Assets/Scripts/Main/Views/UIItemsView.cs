using System.Collections.Generic;
using System.Linq;
using Framework;
using Presenters;
using UniRx;
using UnityEngine;
using ViewModels;
using Zenject;

namespace Views
{
    [PrefabPathAttribute("Prefabs/UIItemsView")]
    public class UIItemsView : ViewBase
    {
        private UIItemsPresenter _presenter;

        [Inject]
        private void Construct(UIItemsPresenter presenter)
        {
            _presenter = presenter;
            _presenter.View = this;
        }

        private void Start()
        {
            var viewModel = _presenter.GetUIItemsViewModel();

            viewModel.Items.ObserveAdd().Subscribe(
                items =>
                {
                    DeleteChildren();
                    CreateView(items.Value.Id);
                }).AddTo(this);
        }

        private void DeleteChildren()
        {
            var views = GetComponentsInChildren<UIItemView>();
            foreach (var uiItemView in views)
            {
                Destroy(uiItemView);
            }
        }

        private UIItemView CreateView(int id)
        {
            var view = CreateInstance<UIItemView>();
            view.gameObject.transform.SetParent(transform);
            view.SetSprite(id);
            return view;
        }

        private List<UIItemView> _list = new List<UIItemView>();

        public void AddUiItemView(UIItemView view)
        {
            _list.Add(view);
        }

        public void RemoveUiItemView(UIItemView view)
        {
            _list.Remove(view);
        }
    }
}
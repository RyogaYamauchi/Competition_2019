using System.Collections.Generic;
using Models;
using UniRx;

namespace ViewModels
{
    public struct UIItemsViewModel
    {
        public readonly IReadOnlyReactiveCollection<ItemModel> Items;
        public UIItemsViewModel(IReadOnlyReactiveCollection<ItemModel> items)
        {
            Items = items;
        }
    }
}
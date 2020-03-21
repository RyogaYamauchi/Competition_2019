using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Models
{
    public class ItemsModel 
    {
        public IReadOnlyReactiveCollection<ItemModel> Items => _list;
        private ReactiveCollection<ItemModel> _list;

        public ItemsModel()
        {
            _list = new ReactiveCollection<ItemModel>();
        }

        public void AddItem(int item)
        {
            _list.Add(new ItemModel(item));
        }

        public ItemModel GetItem(int itemId)
        {
            return _list[itemId];
        }
        
    }
}
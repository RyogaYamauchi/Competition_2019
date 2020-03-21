using Framework;
using UnityEngine;

namespace Views
{
    public class ItemView : ViewBase
    {
        [SerializeField] private int _id;

        public int GetId()
        {
            return _id;
        }
    }
}
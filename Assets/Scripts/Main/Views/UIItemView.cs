using Framework;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [PrefabPathAttribute("Prefabs/UIItemView")]
    public class UIItemView : ViewBase
    {
        [SerializeField] private Image _image;

        public void SetSprite(int id)
        {
            
        }
    }
}
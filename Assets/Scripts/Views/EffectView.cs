using Framework;
using Scripts.Models;
using Scripts.Presenters;
using UnityEngine;

namespace Scripts.Views
{
    public class EffectView : ViewBase
    {
        public Vector2 GetPosition()
        {
            var a = GameModel.Instance.PlayerModel.GetPosition();
            gameObject.transform.position = new Vector3(a.x,a.y);
            return this.transform.position;
        }
    }
}
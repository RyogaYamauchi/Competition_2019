using Framework;
using Scripts.Models;
using UnityEngine;

namespace Scripts.Views
{
    public class EnemyCollider : ViewBase
    {
        [SerializeField] private StaticEnemyView _enemyView;
        public void OnCollisionEnter(Collision other)
        {
//            var player = other.gameObject.GetComponent<PlayerView>();
//            if (player != null)
//            {
//                _enemyView.Damage(GameModel.Instance.PlayerModel.AttackPower);
//            }
        }
        
    }
}
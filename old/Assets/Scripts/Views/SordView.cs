using System;
using Framework;
using UnityEngine;

namespace Scripts.Views
{
    public class SordView : ViewBase
    {
        public void OnCollisionEnter(Collision other)
        {
            var enemyView = other.gameObject.GetComponent<EnemyView>();
            if (enemyView == null) return;
            
        }
    }
}
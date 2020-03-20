using System;
using System.Collections.Generic;
using Framework;
using Scripts.Models;
using Scripts.Presenters;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class StaticEnemyView : EnemyView
    {
        [SerializeField] private int _hp;
        [SerializeField] private int attack;
        [SerializeField] private GameObject _animationPrefab;
        [SerializeField] private GameObject _root;
        
        private Script_SpriteStudio6_Root _animationSpriteStudio6Root;


        private void Start()
        {
            _hp = 3;
            attack = 1;
            _slider.minValue = 0;
            _slider.maxValue = 3;
            _slider.value = _hp;
            var instance = CreateGameObjectFromObject((GameObject)_animationPrefab,  _root);
            _animationSpriteStudio6Root = instance.GetComponent<Script_SpriteStudio6_Root>();
            _animationSpriteStudio6Root.AnimationStop(-1);
        }
        public void Init(PresenterBase presenter = null, IViewModel enemyViewModel = null)
        {
            Presenter = presenter as IEnemyPresenter;
            _enemyViewModel = enemyViewModel is EnemyViewModel ? (EnemyViewModel) enemyViewModel : default;
            _slider.minValue = 0;
            _slider.maxValue = _enemyViewModel.Hp;
            _slider.value = _enemyViewModel.Hp;
            
        }

        public void OnCollisionEnter(Collision other)
        {
//            var player = other.gameObject.GetComponent<PlayerView>();
//            if (player != null)
//            {
//                this.Damage(GameModel.Instance.PlayerModel.AttackPower);
//            }
        }

        public void Damage(int damage)
        {
            _slider.value -= damage;
            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
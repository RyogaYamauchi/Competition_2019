using Framework;
using Scripts.Models;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Views
{
    public class ProjectileView : ViewBase
    {
        
        private Script_SpriteStudio6_Root _flashAnimationRoot;
        private bool _isAnimating;
        private int _direction => GameModel.Instance.PlayerModel.Direction;
        private Rigidbody2D _rigidbody2D;
        
        public void Init()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _flashAnimationRoot = GetComponentInChildren<Script_SpriteStudio6_Root>();
            _flashAnimationRoot.FunctionPlayEnd += LoopBackFunction;
            var a=GameModel.Instance.PlayerModel.GetPosition();
            transform.parent.position = new Vector3(a.x, a.y, -1);
        }
        

        public async UniTask PlayAnimation()
        {
            _rigidbody2D.AddForce(new Vector2(300.0f,0));
            _isAnimating = true;
            var direction = _direction == 1 ? -0.1f : 0.1f;
            while (_isAnimating)
            {
                await UniTask.DelayFrame(1);
            }
            Destroy(gameObject.transform.parent.gameObject);
        }

        private bool LoopBackFunction(Script_SpriteStudio6_Root scriptroot, GameObject objectcontrol)
        {
            _isAnimating = false;
            return true;
        }
        
        public void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log(other.gameObject.name);
            if (other == null) return;
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyView>().Damage(1);
            }
        }
    }
}
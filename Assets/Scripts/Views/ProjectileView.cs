using Framework;
using Scripts.Models;
using Scripts.Presenters;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Views
{
    /// <summary>
    /// 飛び道具を扱うView
    /// フィールドに複数存在できるためPresenterと1:1では生成しない
    /// Presenter 1 : View x
    /// </summary>
    public class ProjectileView : ViewBase
    {
        /// <summary>
        /// Animation用のフィールド
        /// </summary>
        private Script_SpriteStudio6_Root _flashAnimationRoot;

        private Rigidbody2D _rigidbody2D;

        /// <summary>
        /// 状態を管理するフィールド
        /// </summary>
        private bool _isAnimating;



        /// <summary>
        /// Presenter
        /// </summary>
        private IProjectilePresenter _presenter;

        private int _direction => GameModel.Instance.PlayerModel.Direction;

        public void Init(PresenterBase presenterBase = null, IViewModel viewModel = null)
        {
            _presenter = presenterBase as IProjectilePresenter;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _flashAnimationRoot = GetComponentInChildren<Script_SpriteStudio6_Root>();
            _flashAnimationRoot.FunctionPlayEnd += LoopBackFunction;
            var a = GameModel.Instance.PlayerModel.GetPosition();
            gameObject.transform.parent.SetParent(GamePresenter.Instance.EffectPoint.transform);
            transform.parent.position = new Vector3(a.x, a.y, -1);
            PlayAnimation().Forget();
        }

        public async UniTask PlayAnimation()
        {
            _isAnimating = true;
            var direction = _direction == 1 ? -1f : 1f;
            _rigidbody2D.AddForce(new Vector2(300.0f * direction, 0));
            while (_isAnimating)
            {
                await UniTask.DelayFrame(1);
            }
            _presenter.Remove(this);
            Destroy(gameObject.transform.parent.gameObject);
        }

        /// <summary>
        /// アニメーションの再生が終わったら呼び出されるコールバック
        /// </summary>
        /// <param name="scriptroot"></param>
        /// <param name="objectcontrol"></param>
        /// <returns></returns>
        private bool LoopBackFunction(Script_SpriteStudio6_Root scriptroot, GameObject objectcontrol)
        {
            _isAnimating = false;
            return true;
        }

        /// <summary>
        /// 特定のオブジェクトに当たったらそのオブジェクトにダメージを与える
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyView>().Damage(1);
            }
        }
        
    }
}
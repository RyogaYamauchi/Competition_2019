using Framework;
using Scripts.Models;
using Scripts.Presenters;
using UniRx.Async;
using UnityEngine;

namespace Scripts.Views
{
    public class PlayerView : ViewBase
    {
        /// <summary>
        /// Unityからアタッチするフィールド
        /// </summary>
        [SerializeField] private Rigidbody _rigidbody = default;

        [SerializeField] private Object _animationPrefab;
        [SerializeField] private Object pos;

        /// <summary>
        /// Presenter
        /// </summary>
        public IPlayerPresenter Presenter { get; private set; }

        public IProjectilePresenter ProjectilePresenter => GamePresenter.Instance.ProjectilePresenter;
        
        /// <summary>
        /// PlayerModelをまとめたフィールド
        /// </summary>
        private float _moveForceMultiplier => Presenter.GetMoveForceMultiplier();
        private float _moveSpeed => Presenter.MoveSpeed;
        private float _jumpPower => Presenter.JumpPower;

        /// <summary>
        /// プレイヤーアニメーションを制御するフィールド
        /// </summary>
        private bool _isAnimating;
        private AnimationEnum _animationEnum = AnimationEnum.PlayerIdling;
        private Script_SpriteStudio6_Root _animationSpriteStudio6Root;


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="presenter"></param>
        public void Init(PresenterBase presenter = null, IViewModel viewModel = null)
        {
            Presenter = presenter as PlayerPresenter;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            var instance = CreateGameObjectFromObject(_animationPrefab, (GameObject) pos);
            _animationSpriteStudio6Root = instance.GetComponent<Script_SpriteStudio6_Root>();
            _animationSpriteStudio6Root.AnimationStop(-1);
        }

        //TODO : StateMachineに切り出す
        private void Update()
        {
            if (_isAnimating) return;
            if (_rigidbody.velocity.y < -0.01 || _rigidbody.velocity.y > 0.01)
            {
                if (_animationEnum == AnimationEnum.PlayerJump) return;
                JumpAnimation();
                return;
            }

            if (_rigidbody.velocity.x < -0.01 || _rigidbody.velocity.x > 0.01)
            {
                if (_animationEnum == AnimationEnum.PlayerWalking) return;
                WalkingAnimation();
                return;
            }
            if (_animationEnum == AnimationEnum.PlayerIdling) return;

            if (Presenter.Direction == 1)
            {
                _animationEnum = AnimationEnum.PlayerIdling;
                IdlingRightAnimation();
            }
            else
            {
                _animationEnum = AnimationEnum.PlayerIdling;
                IdlingLeftAnimation();
            }
        }

        /// <summary>
        /// アニメーションの状態を管理するStateMachine
        /// 引数に再生したアニメーションを指定する
        /// </summary>
        /// <param name="animationEnum"></param>
        private void AnimationStateMachine(AnimationEnum animationEnum)
        {
            if (_isAnimating) return;
            switch (animationEnum)
            {
                case AnimationEnum.PlayerAttack1:
                    AttackAnimation();
                    break;
                case AnimationEnum.PlayerProjectileAttack1:
                    AttackProjectileAnimation();
                    break;
            }
        }

        private void JumpAnimation()
        {
            _animationEnum = AnimationEnum.PlayerJump;
            if (_rigidbody.velocity.y > 0.1)
            {
                JumpRightAnimation();
            }
            else if (_rigidbody.velocity.y < -0.1)
            {
                JumpLeftAnimation();
            }
        }

        private void JumpRightAnimation()
        {
            _animationSpriteStudio6Root.AnimationPlay(-1, 7, 0, 1);
        }

        private void JumpLeftAnimation()
        {
            _animationSpriteStudio6Root.AnimationPlay(-1, 8, 0, 1);
        }

        private void WalkingAnimation()
        {
            _animationEnum = AnimationEnum.PlayerWalking;
            if (_rigidbody.velocity.x > 0.01)
            {
                WalkingRightAnimation();
            }
            else if (_rigidbody.velocity.x < -0.01)
            {
                WalkingLeftAnimation();
            }
        }

        private void WalkingRightAnimation()
        {
            _animationSpriteStudio6Root.AnimationPlay(-1, 3, 0, 1);
        }

        private void WalkingLeftAnimation()
        {
            _animationSpriteStudio6Root.AnimationPlay(-1, 4, 0, 1);

        }

        private void IdlingRightAnimation()
        {
            _animationSpriteStudio6Root.AnimationPlay(-1, 1, 0, 1);
        }

        private void IdlingLeftAnimation()
        {
            _animationSpriteStudio6Root.AnimationPlay(-1, 2, 0, 1);

        }

        public void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _jumpPower);
            AnimationStateMachine(AnimationEnum.PlayerIdling);
        }

        public void Attack()
        {
            AnimationStateMachine(AnimationEnum.PlayerAttack1);
        }

        private void AttackAnimation()
        {
            _isAnimating = true;
            if (_rigidbody.velocity.x > 0.01)
            {
                AttackRightAnimation();
            }

            if (_rigidbody.velocity.x < -0.01)
            {
                AttackLeftAnimation();
            }
            else
            {
                AttackRightAnimation();
            }

            _animationSpriteStudio6Root.FunctionPlayEnd += LoopBackFunction;
        }

        /// <summary>
        /// アイドリング以外のアニメーションが終わった時のコールバック
        /// これを設定しておけば再生が終わると自動でアイドリングに切り替わる
        /// </summary>
        /// <param name="scriptroot"></param>
        /// <param name="objectcontrol"></param>
        /// <returns></returns>
        private bool LoopBackFunction(Script_SpriteStudio6_Root scriptroot, GameObject objectcontrol)
        {
            _isAnimating = false;
            return true;
        }

        private void AttackRightAnimation()
        {
            _animationSpriteStudio6Root.AnimationPlay(-1, 5, 1, 1);
        }

        private void AttackLeftAnimation()
        {
            _animationSpriteStudio6Root.AnimationPlay(-1, 6, 1, 1);

        }


        public Vector3 GetPosition()
        {
            return gameObject.transform.position;
        }

        public void Move(Vector2 direction)
        {
            var moveVector = Vector3.zero;
            moveVector.x = _moveSpeed * direction.x;
            moveVector.y = _rigidbody.velocity.y;
            var velocity = _rigidbody.velocity;
            moveVector = new Vector3(moveVector.x, moveVector.y,0);
            _rigidbody.AddForce(_moveForceMultiplier * (moveVector - velocity));
            Presenter.UpdatePos(gameObject.transform.position);
        }

        public void AttackProjectile()
        {
            AnimationStateMachine(AnimationEnum.PlayerProjectileAttack1);
        }

        private  void AttackProjectileAnimation()
        {
            var instance = ProjectilePresenter.CreateObject("Prefabs/AttackAnimation/1");
            instance.GetComponentInChildren<ProjectileView>().Init(ProjectilePresenter as ProjectilePresenter);
        }
    }
}
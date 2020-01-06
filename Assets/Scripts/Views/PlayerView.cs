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
        [SerializeField] private Rigidbody2D _rigidbody = default;
        [SerializeField] private Object _idolingAnimationPrefab;
        [SerializeField] private Object _walkAnimationPrefab;
        [SerializeField] private Object _attackAnimationPrefab;
        [SerializeField] private Object _jumpAnimationPrefab;
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
        private Script_SpriteStudio6_Root _idolingspriteStudioRoot;
        private Script_SpriteStudio6_Root _walkSpriteStudioRoot;
        private Script_SpriteStudio6_Root _jumpSpriteStudioRoot;
        private Script_SpriteStudio6_Root _attackSpriteStudioRoot;
        private GameObject _idlingGameObject;
        private GameObject _attackGameObject;
        private GameObject _walkGameObject;
        private GameObject _jumpGameObject;


        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="presenter"></param>
        public override void Init(PresenterBase presenter = null, IViewModel viewModel = null)
        {
            Presenter = presenter as PlayerPresenter;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            var instance = CreateGameObjectFromObject(_idolingAnimationPrefab, (GameObject) pos);
            _idolingspriteStudioRoot = instance.GetComponent<Script_SpriteStudio6_Root>();
            _idolingspriteStudioRoot.AnimationStop(-1);
            _idlingGameObject = instance;
            instance = CreateGameObjectFromObject(_walkAnimationPrefab, (GameObject) pos);
            _walkSpriteStudioRoot = instance.GetComponent<Script_SpriteStudio6_Root>();
            _walkSpriteStudioRoot.AnimationStop(-1);
            instance.gameObject.SetActive(false);
            _walkGameObject = instance;
            instance = CreateGameObjectFromObject(_jumpAnimationPrefab, (GameObject) pos);
            _jumpSpriteStudioRoot = instance.GetComponent<Script_SpriteStudio6_Root>();
            _jumpSpriteStudioRoot.AnimationStop(-1);
            instance.gameObject.SetActive(false);
            _jumpGameObject = instance;

            instance = CreateGameObjectFromObject(_attackAnimationPrefab, (GameObject) pos);
            _attackSpriteStudioRoot = instance.GetComponent<Script_SpriteStudio6_Root>();
            _attackSpriteStudioRoot.AnimationStop(-1);
            instance.gameObject.SetActive(false);
            _attackGameObject = instance;
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

            _idlingGameObject.SetActive(true);
            _jumpGameObject.SetActive(false);
            _walkGameObject.SetActive(false);
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
                    _attackGameObject.SetActive(true);
                    _idlingGameObject.SetActive(false);
                    _walkGameObject.SetActive(false);
                    _jumpGameObject.SetActive(false);
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
            _walkGameObject.SetActive(false);
            _idlingGameObject.SetActive(false);
            _jumpGameObject.SetActive(true);
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
            _jumpSpriteStudioRoot.AnimationPlay(-1, 1, 0, 1);
        }

        private void JumpLeftAnimation()
        {
            _jumpSpriteStudioRoot.AnimationPlay(-1, 0, 0, 1);
        }

        private void WalkingAnimation()
        {
            _animationEnum = AnimationEnum.PlayerWalking;
            _idlingGameObject.SetActive(false);
            _jumpGameObject.SetActive(false);
            _walkGameObject.SetActive(true);
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
            _walkSpriteStudioRoot.AnimationPlay(-1, 1, 0, 1);
        }

        private void WalkingLeftAnimation()
        {
            _walkSpriteStudioRoot.AnimationPlay(-1, 0, 0, 1);
        }

        private void IdlingRightAnimation()
        {
            _idolingspriteStudioRoot.AnimationPlay(-1, 0, 0, 1);
        }

        private void IdlingLeftAnimation()
        {
            _idolingspriteStudioRoot.AnimationPlay(-1, 1, 0, 1);
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

            _attackSpriteStudioRoot.FunctionPlayEnd += LoopBackFunction;
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
            _attackGameObject.SetActive(false);
            _idlingGameObject.SetActive(true);
            _walkGameObject.SetActive(false);
            _jumpGameObject.SetActive(false);
            return true;
        }

        private void AttackRightAnimation()
        {
            _attackSpriteStudioRoot.AnimationPlay(-1, 1, 1, 1);
        }

        private void AttackLeftAnimation()
        {
            _attackSpriteStudioRoot.AnimationPlay(-1, 0, 1, 1);
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
            var moveVector2D = new Vector2(moveVector.x, moveVector.y);
            _rigidbody.AddForce(_moveForceMultiplier * (moveVector2D - velocity));
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
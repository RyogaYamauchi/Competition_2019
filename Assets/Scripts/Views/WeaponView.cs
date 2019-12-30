using Repository;
using Scripts.Models;
using UniRx.Async;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public bool IsAnimating { get; private set; } = false;

    private Sprite[] _attackSprites;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public async UniTask PlayAttackAnimation()
    {
        _attackSprites = AnimationRepository.GetSprites(AnimationEnum.WeaponAttack3);
        _spriteRenderer.sprite = null;
        var max = _attackSprites.Length;
        IsAnimating = true;
        for (int i = 0; i < max; i++)
        {
            _spriteRenderer.sprite = _attackSprites[i];
            gameObject.AddComponent<PolygonCollider2D>();
            await UniTask.Delay(50);
            Destroy(GetComponent<PolygonCollider2D>());
        }
        IsAnimating = false;
        gameObject.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            Debug.Log("ターゲットにヒット");
            Debug.Log(other.gameObject.name);
        }
    }
}
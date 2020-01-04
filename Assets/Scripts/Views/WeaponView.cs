using Scripts.Views;
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
    

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other == null) return;
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyView>().Damage(1);
        }
    }
}
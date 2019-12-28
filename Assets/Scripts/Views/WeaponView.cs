using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Async;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public async UniTask PlayAttackAnimation()
    {
        _animator.SetBool("Attack",true);
        await UniTask.DelayFrame(1);
        _animator.SetBool("Attack",false);
    }
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("ターゲットにヒット");
        Debug.Log(other.gameObject.name);
    }
}

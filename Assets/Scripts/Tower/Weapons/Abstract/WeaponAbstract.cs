using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAbstract : MonoBehaviour
{
    public float RangeToAttack = 20f;
    public float CooldownAttack = 2f;

    public Transform PositionAttack;
    public string BulletTag;
    public ObjectPool BulletPool;

    public UnityEvent OnActivateAttack = new UnityEvent();
    public UnityEvent OnAttack = new UnityEvent();
    public UnityEvent OnDeactivateAttack = new UnityEvent();

    private void Start()
    {
        BulletPool = PoolsManager.GetBulletPoolHandle.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Pistol : WeaponAbstract, IWeapon
{
    float IWeapon.RangeToAttack { get => RangeToAttack; }
    float IWeapon.CooldownAttack { get => CooldownAttack; }

    public void StartAttack(GameObject target)
    {
        GameObject bullet = BulletPool.SpawnFromPool(BulletTag);
        bullet.transform.position = PositionAttack.position;
        bullet.GetComponent<IBullet>().SetTarget(target.transform);

        OnActivateAttack.Invoke();
    }

    public void StopAttack()
    {
        OnDeactivateAttack.Invoke();
    }
}
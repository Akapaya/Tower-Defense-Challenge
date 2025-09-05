using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IWeapon
{
    float RangeToAttack { get; }
    float CooldownAttack { get; }

    public void StartAttack(GameObject target);
    public void StopAttack();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : BulletAbstract, IBullet
{

    private void Start()
    {
        RotateToTargetOnStart = GetComponent<RotateToTargetOnStart>();
    }

    public void SetTarget(Transform target)
    {
        RotateToTargetOnStart.Target = target;
        RotateToTargetOnStart.enabled = true;
    }
}

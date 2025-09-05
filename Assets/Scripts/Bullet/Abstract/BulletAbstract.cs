using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAbstract : MonoBehaviour
{
    public RotateToTargetOnStart RotateToTargetOnStart;

    public string TargetTag = "Enemy";
    public string OffBoundsTag = "OffBounds";
    public int DamageAmount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TargetTag)
        {
            collision.GetComponent<IDamagable>().TakeDamage(DamageAmount);
            RotateToTargetOnStart.enabled = false;
            this.gameObject.SetActive(false);
            return;
        }

        if (collision.gameObject.tag == OffBoundsTag)
        {
            RotateToTargetOnStart.enabled = false;
            this.gameObject.SetActive(false);
            return;
        }
    }
}

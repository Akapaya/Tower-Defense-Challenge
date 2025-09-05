using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpriteFlashDamage : MonoBehaviour
{
    [Header("Components")]
    private SpriteRenderer spriteRenderer;

    [Header("Values")]
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlashDamage()
    {
        spriteRenderer.DOColor(flashColor, flashDuration).OnComplete(() =>
        {
            spriteRenderer.DOColor(Color.white, flashDuration);
        });
    }
}

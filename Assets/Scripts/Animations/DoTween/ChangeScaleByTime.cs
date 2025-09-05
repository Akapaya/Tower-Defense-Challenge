using DG.Tweening;
using UnityEngine;

public class ChangeScaleByTime : MonoBehaviour
{
    [SerializeField] private float minScale = 0.8f;
    [SerializeField] private float maxScale = 1.0f;
    [SerializeField] private float duration = 1f;

    void Start()
    {
        AnimateScale();
    }

    private void AnimateScale()
    {
        Sequence scaleSequence = DOTween.Sequence();

        scaleSequence.Append(transform.DOScale(minScale, duration).SetEase(Ease.InOutQuad));
        scaleSequence.Append(transform.DOScale(maxScale, duration).SetEase(Ease.InOutQuad));

        scaleSequence.SetLoops(-1);
    }
}
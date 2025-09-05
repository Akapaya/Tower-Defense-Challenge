using UnityEngine;
using UnityEngine.Events;

public class ClickableSprite : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.OnPickupSummonPointHandle.Invoke();
        Destroy(gameObject);
    }
}

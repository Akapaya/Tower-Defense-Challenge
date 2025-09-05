using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Values")]
    public GameObject Tower;

    public delegate void ActivateCellEvent();
    public static ActivateCellEvent ActivateCellHandle;

    public delegate void DeactivateCellEvent();
    public static DeactivateCellEvent DeactivateCellHandle;

    #region StartMethods
    private void OnEnable()
    {
        ActivateCellHandle += ActivateCell;
        DeactivateCellHandle += DeactivateCell;
    }

    private void OnDisable()
    {
        ActivateCellHandle -= ActivateCell;
        DeactivateCellHandle -= DeactivateCell;
    }
    #endregion

    public void ActivateCell()
    {
        if(Tower == null)
        {
            _spriteRenderer.enabled = true;
        }
    }

    public void DeactivateCell()
    {
        _spriteRenderer.enabled = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private WalkToCell _walkToCell;
    [SerializeField] private TowerViewer _towerViewer;

    private void Start()
    {
        _walkToCell = this.gameObject.GetComponent<WalkToCell>();
        _towerViewer = this.gameObject.GetComponent<TowerViewer>();
    }

    public void Initialize(Vector3 cellPosition)
    {
        _walkToCell.CellPosition = cellPosition;
        _towerViewer.ChangeDirectionSprite(cellPosition);
    }
}
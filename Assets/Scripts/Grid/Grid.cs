using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{
    public CellController[,] _gridCellControllerArray;
    //public GameObject[,] _gridObjectsArray;

    #region Construct
    public Grid(int columnCount, int rowsCount)
    {
        _gridCellControllerArray = new CellController[columnCount, rowsCount];
    }
    #endregion

    #region GetMethods
    public Vector3 GetWorldPosition(int columnCount, int rowsCount, float cellSize, Vector3 originPosition)
    {
        return new Vector3(columnCount, rowsCount) * cellSize + originPosition;
    }

    public Vector3 GetWorldPositionFromCell(int columnCount, int rowsCount, float cellSize, Vector3 originPosition)
    {
        return new Vector3(columnCount * cellSize + cellSize / 2, rowsCount * cellSize + cellSize / 2) + originPosition;
    }

    public void GetGridPosition(Vector3 worldPosition, Vector3 originPosition, float cellSize, out int columnCount, out int rowsCount)
    {
        columnCount = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        rowsCount = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    #endregion

    #region SetMethods
    public void SetObjectInGrid(int columnCount, int rowsCount, GameObject tower)
    {
        _gridCellControllerArray[columnCount, rowsCount].Tower = tower;
    }
    #endregion
}
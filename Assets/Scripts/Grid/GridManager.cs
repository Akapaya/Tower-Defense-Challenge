using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Components")]
    private Grid grid;

    [Header("Grid Settings")]
    public int RowsGrid = 5;
    public int ColumnGrid = 5;
    [SerializeField] private float _cellSize = 1f;
    [SerializeField] private Vector3 _originPosition;

    [Header("Prefabs")]
    [SerializeField] private GameObject _gridObject;

    [Header("Hide In Inspector")]
    [HideInInspector]
    public List<bool> CellsAbleToBuildTowerList;
    [HideInInspector]
    public List<bool> CellsPathOfEnemiesList;

    #region Delegates
    public delegate bool CheckIfCanBuildInCellEvent(Vector3 mousePosition);
    public static CheckIfCanBuildInCellEvent CheckIfCanBuildInCellHandle;

    public delegate void SetTowerInCellEvent(TowerBuildData towerBuildData);
    public static SetTowerInCellEvent SetTowerInCellHandle;

    public delegate Vector3 GetCellPositionEvent(TowerBuildData towerBuildData);
    public static GetCellPositionEvent GetCellPositionHandle;
    #endregion

    #region StartMethods
    private void OnEnable()
    {
        CheckIfCanBuildInCellHandle += CheckIfCanBuildInCell;

        SetTowerInCellHandle += SetObjectInGrid;

        GetCellPositionHandle += GetCellPositionInWorld;
    }

    private void OnDisable()
    {
        CheckIfCanBuildInCellHandle -= CheckIfCanBuildInCell;

        SetTowerInCellHandle -= SetObjectInGrid;

        GetCellPositionHandle -= GetCellPositionInWorld;
    }

    void Start()
    {
        InitializeCellsAbleToBuildTowerList();

        grid = new Grid(ColumnGrid, RowsGrid);

        for (int row = 0; row < RowsGrid; row++)
        {
            for (int column = 0; column < ColumnGrid; column++)
            {
                if (GetCellAbleToBuildValue(row, column, CellsAbleToBuildTowerList) == true)
                {
                    GameObject cell = Instantiate(_gridObject, Vector3.zero, Quaternion.identity);
                    grid._gridCellControllerArray[column,row] = cell.GetComponent<CellController>();
                    cell.transform.position = grid.GetWorldPositionFromCell(column, row, _cellSize, _originPosition);
                    cell.transform.SetParent(this.transform);
                }
            }
        }
    }
    #endregion

    #region Custom Editor Methods
    public void InitializeCellsAbleToBuildTowerList()
    {
        if (CellsAbleToBuildTowerList == null || CellsAbleToBuildTowerList.Count != ColumnGrid * RowsGrid)
        {
            CellsAbleToBuildTowerList = new List<bool>(ColumnGrid * RowsGrid);
            for (int i = 0; i < ColumnGrid * RowsGrid; i++)
            {
                CellsAbleToBuildTowerList.Add(false);
            }
        }

        if (CellsPathOfEnemiesList == null || CellsPathOfEnemiesList.Count != ColumnGrid * RowsGrid)
        {
            CellsPathOfEnemiesList = new List<bool>(ColumnGrid * RowsGrid);
            for (int i = 0; i < ColumnGrid * RowsGrid; i++)
            {
                CellsPathOfEnemiesList.Add(false);
            }
        }
    }
    #endregion

    #region Grid Able To Build List
    public bool GetCellAbleToBuildValue(int row, int column, List<bool> cellList)
    {
        int index = row * ColumnGrid + column;
        return cellList[index];
    }

    public void SetCellAbleToBuildValue(int row, int column, bool value, List<bool> cellList)
    {
        int index = row * ColumnGrid + column;
        cellList[index] = value;
    }
    #endregion

    #region Grid Path To Enemies List
    public bool GetCellPathEnemiesValue(int row, int column, int columnDirection)
    {

        while (row >= 0 && row < RowsGrid && column >= 0 && column < ColumnGrid)
        {
            if (CellsPathOfEnemiesList[row * ColumnGrid + column])
            {
                return true;
            }

            column += columnDirection;
        }

        return false;
    }

    public void SetCellPathEnemiesValue(int row, int column, bool value)
    {
        int index = row * ColumnGrid + column;
        CellsPathOfEnemiesList[index] = value;
    }
    #endregion

    #region SettersMethods
    public void SetObjectInGrid(TowerBuildData args)
    {
        int column = 0;
        int row = 0;

        grid.GetGridPosition(args.MousePosition, _originPosition, _cellSize, out column, out row);

        if (GetCellAbleToBuildValue(row, column, CellsAbleToBuildTowerList) == false)
        {
            return;
        }

        CheckDirectionToEnemyPath(args);

        grid.SetObjectInGrid(column, row, args.InstantiatedObject);
    }
    #endregion

    #region GettersMethods
    public Vector3 GetCellPositionInWorld(TowerBuildData args)
    {
        int column = 0;
        int row = 0;

        grid.GetGridPosition(args.MousePosition, _originPosition, _cellSize, out column, out row);

        if (GetCellAbleToBuildValue(row, column, CellsAbleToBuildTowerList) == false)
        {
            return Vector3.zero;
        }

        return grid.GetWorldPositionFromCell(column, row, _cellSize, _originPosition);
    }
    #endregion

    #region CheckMethods
    bool CheckIfCanBuildInCell(Vector3 mousePosition)
    {
        int column = 0;
        int row = 0;

        grid.GetGridPosition(mousePosition, _originPosition, _cellSize, out column, out row);

        if (row < RowsGrid && row >= 0 && column < ColumnGrid && column >= 0)
        {
            if (GetCellAbleToBuildValue(row, column, CellsAbleToBuildTowerList) == true && grid._gridCellControllerArray[column, row].Tower == null)
            {
                return true;
            }
        }

        return false;
    }

    private void CheckDirectionToEnemyPath(TowerBuildData args)
    {
        int column = 0;
        int row = 0;

        grid.GetGridPosition(args.MousePosition, _originPosition, _cellSize, out column, out row);

        //Check If Enemy path is on Right Side
        if (GetCellPathEnemiesValue(row, column, columnDirection: 1) == true)
        {
            args.Direction = Direction.ToRight;
            return;
        }

        //Check If Enemy path is on Left Side
        if (GetCellPathEnemiesValue(row, column, columnDirection: -1) == true)
        {
            args.Direction = Direction.ToLeft;
            return;
        }
    }
    #endregion

    void OnDrawGizmos()
    {
        if (grid != null)
        {
            Gizmos.color = Color.green;

            // Draw vertical lines
            for (int x = 0; x <= ColumnGrid; x++)
            {
                Vector3 startPos = grid.GetWorldPosition(x, 0, _cellSize, _originPosition);
                Vector3 endPos = grid.GetWorldPosition(x, RowsGrid, _cellSize, _originPosition);
                Gizmos.DrawLine(startPos, endPos);
            }

            //Draw horizontal lines
            for (int y = 0; y <= RowsGrid; y++)
            {
                Vector3 startPos = grid.GetWorldPosition(0, y, _cellSize, _originPosition);
                Vector3 endPos = grid.GetWorldPosition(ColumnGrid, y, _cellSize, _originPosition);
                Gizmos.DrawLine(startPos, endPos);
            }
        }
    }
}

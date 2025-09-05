using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Events;

public class BuildManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private List<GameObject> _towersFabsList = new List<GameObject>();

    [Header("Values")]
    private bool _canBuild = true;

    [Header("Positions To Spawn")]
    [SerializeField] private Transform _leftSpawn;
    [SerializeField] private Transform _rightSpawn;

    #region UnityEvents
    public UnityEvent OnBuildedTower = new UnityEvent();
    #endregion


    void CheckIfCanBuildTower(Vector3 mousePosition)
    {
        if(GridManager.CheckIfCanBuildInCellHandle(mousePosition) == false)
        {
            return;
        }

        SpawnTowerOnMousePosition(mousePosition);
    }

    void SpawnTowerOnMousePosition(Vector3 mousePosition)
    {
        GameObject selectedTower = _towersFabsList[Random.Range(0, _towersFabsList.Count)];

        GameObject instantiatedObject = Instantiate(selectedTower, Vector3.zero, Quaternion.identity);

        TowerBuildData towerBuildData = new TowerBuildData(instantiatedObject, mousePosition);

        GridManager.SetTowerInCellHandle.Invoke(towerBuildData);
        
        Vector3 cellPosition = GridManager.GetCellPositionHandle.Invoke(towerBuildData);

        switch (towerBuildData.Direction)
        {
            case Direction.ToRight:
                {
                    instantiatedObject.transform.position = _leftSpawn.position;
                    break;
                }
            case Direction.ToLeft:
                {
                    instantiatedObject.transform.position = _rightSpawn.position;
                    break;
                }
        }

        instantiatedObject.GetComponent<TowerController>().Initialize(cellPosition);

        OnBuildedTower.Invoke();
    }

    #region UpdateMethods
    private void Update()
    {
        if (_canBuild)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckIfCanBuildTower(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
    #endregion

    public void ActivateBuild()
    {
        _canBuild = true;
        CellController.ActivateCellHandle.Invoke();
    }

    public void DeactivateBuild()
    {
        _canBuild = false;
        CellController.DeactivateCellHandle.Invoke();
    }
}
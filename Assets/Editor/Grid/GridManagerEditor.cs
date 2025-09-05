using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridManager gridManager = (GridManager)target;

        if (gridManager.CellsAbleToBuildTowerList == null || gridManager.CellsAbleToBuildTowerList.Count != gridManager.ColumnGrid * gridManager.RowsGrid
            || gridManager.CellsPathOfEnemiesList == null || gridManager.CellsPathOfEnemiesList.Count != gridManager.ColumnGrid * gridManager.RowsGrid)
        {
            gridManager.InitializeCellsAbleToBuildTowerList();
        }

        EditorGUILayout.Space(10f);

        EditorGUILayout.LabelField("Cells Able To Build Tower", EditorStyles.boldLabel);


        for (int row = gridManager.RowsGrid - 1; row >= 0; row--)
        {
            EditorGUILayout.BeginHorizontal();
            for (int column = 0; column < gridManager.ColumnGrid; column++)
            {
                bool value = gridManager.GetCellAbleToBuildValue(row, column, gridManager.CellsAbleToBuildTowerList);
                value = EditorGUILayout.Toggle(value);
                gridManager.SetCellAbleToBuildValue(row, column, value, gridManager.CellsAbleToBuildTowerList);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space(10f);

        EditorGUILayout.LabelField("Cells Path Of Enemies", EditorStyles.boldLabel);


        for (int row = gridManager.RowsGrid - 1; row >= 0; row--)
        {
            EditorGUILayout.BeginHorizontal();
            for (int column = 0; column < gridManager.ColumnGrid; column++)
            {
                bool value = gridManager.GetCellAbleToBuildValue(row, column, gridManager.CellsPathOfEnemiesList);
                value = EditorGUILayout.Toggle(value);
                gridManager.SetCellAbleToBuildValue(row, column, value, gridManager.CellsPathOfEnemiesList);
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}

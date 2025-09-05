using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path2DManager : MonoBehaviour
{
    public List<Vector2> waypoints;

    public static Path2DManager instance;

    private void Start()
    {
        instance = this;
    }

    #region DrawMethods
    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count < 2)
            return;

        Gizmos.color = Color.red;
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
        }
    }
    #endregion
}

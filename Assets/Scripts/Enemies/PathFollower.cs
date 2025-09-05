using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float _movementSpeed;
    public bool CanMove = true;

    [SerializeField] private int currentWaypointIndex = 0;
    [SerializeField] private Vector2 targetWaypoint;

    #region StartMethods
    private void OnEnable()
    {
        currentWaypointIndex = 0;
        if (Path2DManager.instance != null)
        {
            transform.position = Path2DManager.instance.waypoints[currentWaypointIndex];            
        }
    }

    void Start()
    {
        CanMove = true;
        NextTargetWayPoint();
    }
    #endregion

    void NextTargetWayPoint()
    {
        currentWaypointIndex++;
        targetWaypoint = Path2DManager.instance.waypoints[currentWaypointIndex];
    }

    void LateUpdate()
    {
        if (CanMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, _movementSpeed * Time.deltaTime);

            if ((Vector2)transform.position == targetWaypoint)
            {
                if (currentWaypointIndex == Path2DManager.instance.waypoints.Count - 1)
                {
                    GameManager.OnEnemyPassedHandle.Invoke();
                    this.gameObject.SetActive(false);
                }
                else
                {
                    NextTargetWayPoint();
                }
            }
        }
    }

    internal void ResetWayPoint()
    {
        currentWaypointIndex = 0;
        NextTargetWayPoint();
    }
}
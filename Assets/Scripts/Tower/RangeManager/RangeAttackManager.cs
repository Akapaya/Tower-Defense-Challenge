using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackManager
{
    private List<GameObject> _detectedEnemies = new List<GameObject>();

    #region Start Methods
    void Start()
    {
    }
    #endregion

    #region GetTargetEnemy
    public void GetAllEnemiesInRange(Vector2 position, float range)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, range);        

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                if(collider.gameObject.GetComponent<ITargetable>().IsAlive)
                {
                    _detectedEnemies.Add(collider.gameObject);
                }
            }
        }
    }

    public GameObject GetClosestEnemy(Vector2 position, float range)
    {
        GetAllEnemiesInRange(position, range);

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in _detectedEnemies)
        {
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, position);
            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                closestEnemy = enemy;
            }
        }

        _detectedEnemies.Clear();

        return closestEnemy;
    }

    public GameObject GetFirstEnemyInRange(Vector2 position, float range)
    {
        GetAllEnemiesInRange(position, range);

        if (_detectedEnemies.Count > 0)
        {
            GameObject firstEnemy = _detectedEnemies[0];
            _detectedEnemies.Clear();
            return firstEnemy;
        }

        return null;
    }

    #endregion
}

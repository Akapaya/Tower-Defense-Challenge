using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerViewer : MonoBehaviour
{
    public void ChangeDirectionSprite(Vector3 direction)
    {
        if (transform.position.x - direction.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
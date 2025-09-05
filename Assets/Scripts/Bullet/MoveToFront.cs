using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFront : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 2f;

    #region Updates
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * _movementSpeed);
    }
    #endregion
}

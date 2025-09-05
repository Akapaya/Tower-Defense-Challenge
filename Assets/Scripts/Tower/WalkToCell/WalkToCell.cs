using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToCell : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator _animator;

    [Header("Target Cell Position")]
    public Vector3 CellPosition = Vector3.zero;

    [Header("Variables Speed")]
    [SerializeField] private float _movementSpeed = 1.0f;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(Walking());
    }

    IEnumerator Walking()
    {
        transform.position = Vector3.MoveTowards(transform.position, CellPosition, _movementSpeed * Time.deltaTime);
        _animator.SetBool("InMovement", true);

        yield return new WaitForEndOfFrame();

        if(transform.position != CellPosition)
        {
            StartCoroutine("Walking");
        }
        else
        {
            _animator.SetBool("InMovement", false);
        }
    }
}
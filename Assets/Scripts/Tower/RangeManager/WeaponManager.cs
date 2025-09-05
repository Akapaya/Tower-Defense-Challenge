using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class WeaponManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RangeAttackManager _rangeAttackManager = new();
    [SerializeField] private CircleMovement _circleMovement;

    [Header("Data Values")]
    [SerializeField] IWeapon _weapon;

    [SerializeField] private float _currentRange = 20f;

    GameObject _target;

    #region Draw Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 1f, 1f);
        Vector3 position = transform.position;

        int segments = 32;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * 2f * Mathf.PI / segments;
            Vector3 pointA = position + new Vector3(Mathf.Cos(angle) * _currentRange, Mathf.Sin(angle) * _currentRange, 0f);
            Vector3 pointB = position + new Vector3(Mathf.Cos(angle + 2f * Mathf.PI / segments) * _currentRange, Mathf.Sin(angle + 2f * Mathf.PI / segments) * _currentRange, 0f);

            Gizmos.DrawLine(pointA, pointB);
        }
    }
    #endregion

    #region Start Methods

    private void Start()
    {
        _weapon = GetComponent<IWeapon>();
        _circleMovement = GetComponent<CircleMovement>();
        _rangeAttackManager = new RangeAttackManager();        
        StartWeaponsRoutines();
    }

    public void StartWeaponsRoutines()
    {
        StopAllCoroutines();

        _currentRange = _weapon.RangeToAttack;

        StartCoroutine(AttackWithWeapon());
    }
    #endregion

    public void Update()
    {
        _target = _rangeAttackManager.GetClosestEnemy(transform.position, _weapon.RangeToAttack);

        if (_target != null)
        {
            _circleMovement.PointToObject(_target.transform);
        }
    }

    #region Routines
    IEnumerator AttackWithWeapon()
    {     

        yield return new WaitForSeconds(_weapon.CooldownAttack);

        if (_target != null)
        {
            _weapon.StartAttack(_target);
        }

        StartCoroutine(AttackWithWeapon());
    }
    #endregion
}

using UnityEngine;

public class RotateToTargetOnStart : MonoBehaviour
{
    public Transform Target;
    [SerializeField] private float _offSetRotationZ = 90f;

    #region Start Methods
    private void OnEnable()
    {
        if (Target != null)
        {
            Vector3 directionToTarget = Target.position - transform.position;

            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - _offSetRotationZ));
        }
    }
    #endregion
}

using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float circleRadius = 5f;
    public float rotationOffset = 0f;

    public Transform centerPosition;
    public Transform target;

    private void Update()
    {
        if (target != null)
        {
            Vector3 directionToTarget = target.position - transform.position;
            float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            float angleIncrement = rotationSpeed * Time.deltaTime;
            transform.RotateAround(centerPosition.position, Vector3.forward, angleIncrement);

            Vector3 newPosition = centerPosition.position + Quaternion.Euler(0f, 0f, angleToTarget) * Vector3.right * circleRadius;
            transform.position = newPosition;

            Vector3 lookAtTarget = new Vector3(target.position.x, target.position.y, transform.position.z);
            float angle = Mathf.Atan2(lookAtTarget.y - transform.position.y, lookAtTarget.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset); 

            transform.localScale = transform.parent.localScale;
        }
    }

    public void PointToObject(Transform target)
    {
        this.target = target;
    }
}

using UnityEngine;

public class CitizenController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float waitTime = 2f;

    private Vector3[] patrolPoints = new Vector3[4];
    private int currentPointIndex = 0;
    private float waitTimer = 0f;

    void Start()
    {
        patrolPoints[0] = new Vector3(-20f, 0.5f, 20f);
        patrolPoints[1] = new Vector3(20f, 0.5f, 20f);
        patrolPoints[2] = new Vector3(20f, 0.5f, -20f);
        patrolPoints[3] = new Vector3(-20f, 0.5f, -20f);

        transform.position = patrolPoints[0];
        currentPointIndex = 1;
    }

    void Update()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        Vector3 target = patrolPoints[currentPointIndex];
        Vector3 direction = (target - transform.position).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }

        if (Vector3.Distance(transform.position, target) < 0.2f)
        {
            waitTimer = waitTime;
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }
}

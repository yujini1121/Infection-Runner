// 이 스크립트는 시민 캐릭터가 사각형 맵을 따라 순차적으로 이동하도록 제어합니다.
// 각 지점에서 일정 시간 대기하며, 다음 지점으로 이동합니다.
// 이동 중에는 캐릭터가 이동 방향을 향해 부드럽게 회전합니다.

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
        // 사각형 경로의 네 지점을 설정합니다.
        patrolPoints[0] = new Vector3(-20f, 0.5f, 20f);
        patrolPoints[1] = new Vector3(20f, 0.5f, 20f);
        patrolPoints[2] = new Vector3(20f, 0.5f, -20f);
        patrolPoints[3] = new Vector3(-20f, 0.5f, -20f);

        // 시작 위치를 첫 번째 지점으로 설정하고, 다음 목표 지점을 두 번째 지점으로 설정합니다.
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

        // 현재 위치를 목표 지점을 향해 이동시킵니다.
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 이동 방향으로 부드럽게 회전합니다.
        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }

        // 목표 지점에 도달하면 대기 시간을 설정하고, 다음 지점으로 인덱스를 갱신합니다.
        if (Vector3.Distance(transform.position, target) < 0.2f)
        {
            waitTimer = waitTime;
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }
}

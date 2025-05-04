// 이 스크립트는 시민 캐릭터가 자신의 초기 위치를 기준으로 일정 반경 내에서
// 임의의 방향으로 이동하고, 일정 시간 대기하는 행동을 반복하도록 제어합니다.
// 각 시민은 독립적으로 움직이며, 서로 다른 경로로 배회합니다.

using UnityEngine;

public class CitizenController : MonoBehaviour
{
    [Header("이동 속도 및 반경 설정")]
    public float moveSpeed = 1f;             
    public float moveRadius = 10f;           
    public float waitTimeMin = 1f;           
    public float waitTimeMax = 3f;           

    private Vector3 originPosition;          
    private Vector3 targetPosition;          
    private float waitTimer = 0f;            
    private bool isWaiting = false;          

    void Start()
    {
        originPosition = transform.position; // 초기 위치 저장
        SetNewTargetPosition();              // 첫 번째 목표 위치 설정
    }

    void Update()
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                SetNewTargetPosition();
            }
            return;
        }

        // 목표 위치로 이동
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 이동 중 회전 처리
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // 목표 위치에 도달하면 대기 상태로 전환
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            isWaiting = true;
            waitTimer = Random.Range(waitTimeMin, waitTimeMax);
        }
    }

    // 새로운 목표 위치 설정
    void SetNewTargetPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * moveRadius;
        targetPosition = originPosition + new Vector3(randomCircle.x, 0f, randomCircle.y);
    }
}

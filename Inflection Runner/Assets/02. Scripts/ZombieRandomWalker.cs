// 이 스크립트는 좀비 캐릭터가 무작위 방향으로 일정 시간 동안 이동하도록 제어합니다.
// 이동 방향, 속도, 지속 시간을 무작위로 설정하며, 이동 중에는 방향을 향해 부드럽게 회전합니다.
// 랜덤워크 모델이 적용된 핵심 스크립트입니다.
using UnityEngine;
using TMPro;

public class ZombieRandomWalker : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float minMoveDuration = 2f;
    public float maxMoveDuration = 5f;
    public float rotationSpeed = 5f;

    public TextMeshProUGUI debugText;

    private Vector3 minBounds = new Vector3(-23.5f, 0.5f, -23.5f);
    private Vector3 maxBounds = new Vector3(23.5f, 0.5f, 23.5f);

    private Vector3 moveDirection;
    private float moveSpeed;
    private float moveTimer;

    void Start()
    {
        PickNewMovement();
    }

    void Update()
    {
        Vector3 nextPos = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // 다음 위치가 경계 내에 있는지 확인
        if (IsInsideBoundary(nextPos))
        {
            transform.position = nextPos;
        }
        else
        {
            PickNewMovement();
            return;
        }

        moveTimer -= Time.deltaTime;

        // 이동 방향으로 부드럽게 회전
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 이동 시간이 종료되면 새로운 이동 방향을 설정
        if (moveTimer <= 0f)
        {
            PickNewMovement();
        }

        // 디버그 텍스트 업데이트
        if (debugText != null)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            debugText.text = $"Speed: {moveSpeed:F2} | Duration: {moveTimer:F1}s | Direction: {angle:F1}°";
        }
    }

    // 무작위 방향, 속도, 이동 시간 설정
    void PickNewMovement()
    {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        moveTimer = Random.Range(minMoveDuration, maxMoveDuration);
    }

    // 맵 바운더리 안에 있는지 체크
    bool IsInsideBoundary(Vector3 position)
    {
        return position.x >= minBounds.x && position.x <= maxBounds.x &&
               position.z >= minBounds.z && position.z <= maxBounds.z;
    }
}

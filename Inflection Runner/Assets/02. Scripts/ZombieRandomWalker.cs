// �� ��ũ��Ʈ�� ���� ĳ���Ͱ� ������ �������� ���� �ð� ���� �̵��ϵ��� �����մϴ�.
// �̵� ����, �ӵ�, ���� �ð��� �������� �����ϸ�, �̵� �߿��� ������ ���� �ε巴�� ȸ���մϴ�.
// ������ũ ���� ����� �ٽ� ��ũ��Ʈ�Դϴ�.
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

        // ���� ��ġ�� ��� ���� �ִ��� Ȯ��
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

        // �̵� �������� �ε巴�� ȸ��
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // �̵� �ð��� ����Ǹ� ���ο� �̵� ������ ����
        if (moveTimer <= 0f)
        {
            PickNewMovement();
        }

        // ����� �ؽ�Ʈ ������Ʈ
        if (debugText != null)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            debugText.text = $"Speed: {moveSpeed:F2} | Duration: {moveTimer:F1}s | Direction: {angle:F1}��";
        }
    }

    // ������ ����, �ӵ�, �̵� �ð� ����
    void PickNewMovement()
    {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        moveTimer = Random.Range(minMoveDuration, maxMoveDuration);
    }

    // �� �ٿ���� �ȿ� �ִ��� üũ
    bool IsInsideBoundary(Vector3 position)
    {
        return position.x >= minBounds.x && position.x <= maxBounds.x &&
               position.z >= minBounds.z && position.z <= maxBounds.z;
    }
}

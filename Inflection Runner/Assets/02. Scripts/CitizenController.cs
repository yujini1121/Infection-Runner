// �� ��ũ��Ʈ�� �ù� ĳ���Ͱ� �ڽ��� �ʱ� ��ġ�� �������� ���� �ݰ� ������
// ������ �������� �̵��ϰ�, ���� �ð� ����ϴ� �ൿ�� �ݺ��ϵ��� �����մϴ�.
// �� �ù��� ���������� �����̸�, ���� �ٸ� ��η� ��ȸ�մϴ�.

using UnityEngine;

public class CitizenController : MonoBehaviour
{
    [Header("�̵� �ӵ� �� �ݰ� ����")]
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
        originPosition = transform.position; // �ʱ� ��ġ ����
        SetNewTargetPosition();              // ù ��° ��ǥ ��ġ ����
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

        // ��ǥ ��ġ�� �̵�
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // �̵� �� ȸ�� ó��
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // ��ǥ ��ġ�� �����ϸ� ��� ���·� ��ȯ
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            isWaiting = true;
            waitTimer = Random.Range(waitTimeMin, waitTimeMax);
        }
    }

    // ���ο� ��ǥ ��ġ ����
    void SetNewTargetPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * moveRadius;
        targetPosition = originPosition + new Vector3(randomCircle.x, 0f, randomCircle.y);
    }
}

// �� ��ũ��Ʈ�� �ù� ĳ���Ͱ� �簢�� ���� ���� ���������� �̵��ϵ��� �����մϴ�.
// �� �������� ���� �ð� ����ϸ�, ���� �������� �̵��մϴ�.
// �̵� �߿��� ĳ���Ͱ� �̵� ������ ���� �ε巴�� ȸ���մϴ�.

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
        // �簢�� ����� �� ������ �����մϴ�.
        patrolPoints[0] = new Vector3(-20f, 0.5f, 20f);
        patrolPoints[1] = new Vector3(20f, 0.5f, 20f);
        patrolPoints[2] = new Vector3(20f, 0.5f, -20f);
        patrolPoints[3] = new Vector3(-20f, 0.5f, -20f);

        // ���� ��ġ�� ù ��° �������� �����ϰ�, ���� ��ǥ ������ �� ��° �������� �����մϴ�.
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

        // ���� ��ġ�� ��ǥ ������ ���� �̵���ŵ�ϴ�.
        transform.position += direction * moveSpeed * Time.deltaTime;

        // �̵� �������� �ε巴�� ȸ���մϴ�.
        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }

        // ��ǥ ������ �����ϸ� ��� �ð��� �����ϰ�, ���� �������� �ε����� �����մϴ�.
        if (Vector3.Distance(transform.position, target) < 0.2f)
        {
            waitTimer = waitTime;
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }
}

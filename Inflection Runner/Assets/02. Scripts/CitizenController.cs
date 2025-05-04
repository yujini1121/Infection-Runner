using UnityEngine;

public class CitizenController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float moveDuration = 2f;
    public float restDuration = 2f;

    private Vector3 moveDirection;
    private float timer;
    private bool isMoving = true;

    void Start()
    {
        PickNewDirection();
        timer = moveDuration;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isMoving = !isMoving;
            timer = isMoving ? moveDuration : restDuration;

            if (isMoving)
            {
                PickNewDirection();
            }
        }
    }

    void PickNewDirection()
    {
        float angle = Random.Range(0f, 360f);
        moveDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }
}

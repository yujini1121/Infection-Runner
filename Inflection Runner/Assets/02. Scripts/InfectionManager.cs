using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    [Header("����")]
    public GameObject zombie;
    public GameObject[] citizens;

    [Header("���� ���� �� ������")]
    public float infectionRadius = 2f;
    public float infectionDelay = 3f;

    void Update()
    {
        foreach (GameObject citizen in citizens)
        {
            float distance = Vector3.Distance(zombie.transform.position, citizen.transform.position);
            if (distance <= infectionRadius)
            {
                StartCoroutine(InfectCitizen(citizen));
            }
        }
    }

    System.Collections.IEnumerator InfectCitizen(GameObject citizen)
    {
        yield return new WaitForSeconds(infectionDelay);

        // �ù��� ����� ��ȯ
        citizen.tag = "Zombie";
        citizen.AddComponent<ZombieRandomWalker>();
        Destroy(citizen.GetComponent<CitizenController>());
    }
}

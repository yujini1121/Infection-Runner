using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    [Header("À¯´Ö")]
    public GameObject zombie;
    public GameObject[] citizens;

    [Header("°¨¿° ¹üÀ§ ¹× µô·¹ÀÌ")]
    public float infectionRadius = 2f;
    public float infectionDelay = 3f;

    private System.Collections.Generic.HashSet<GameObject> infectedCitizens = new System.Collections.Generic.HashSet<GameObject>();

    void Update()
    {
        foreach (GameObject citizen in citizens)
        {
            if (infectedCitizens.Contains(citizen)) continue;

            float distance = Vector3.Distance(zombie.transform.position, citizen.transform.position);
            if (distance <= infectionRadius)
            {
                StartCoroutine(InfectCitizen(citizen));
                infectedCitizens.Add(citizen);
            }
        }
    }

    System.Collections.IEnumerator InfectCitizen(GameObject citizen)
    {
        yield return new WaitForSeconds(infectionDelay);

        // ½Ã¹ÎÀ» Á»ºñ·Î º¯È¯
        citizen.tag = "Zombie";
        citizen.GetComponent<CitizenController>().enabled = false;
        citizen.GetComponent<ZombieRandomWalker>().enabled = true;
    }
}

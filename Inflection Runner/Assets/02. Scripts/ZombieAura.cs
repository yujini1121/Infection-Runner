using UnityEngine;

public class ZombieAura : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Citizen"))
        {
            UnitController unit = other.GetComponentInParent<UnitController>();
            if (unit != null)
            {
                unit.Infect();
            }
        }
    }
}

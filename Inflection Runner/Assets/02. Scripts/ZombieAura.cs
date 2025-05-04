// 이 스크립트는 좀비 오라 이펙트 범위 내에 시민이 들어올 경우, 해당 시민을 감염시킵니다.

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

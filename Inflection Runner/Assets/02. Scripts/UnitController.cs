// 이 스크립트는 유닛의 타입(시민 또는 좀비)을 관리하고, 감염 과정을 처리합니다.
// 감염 시, 시민 모델을 비활성화하고 좀비 모델을 활성화하며, 관련 컨트롤러를 전환합니다.
// 시민은 감염된 그 즉시, 자리에 멈추고 5초 후에 좀비로 바뀌고, 새로 태어난 좀비는 3초동안 구체 이펙트도 함께 적용됩니다.
// (원래는 애니메이션 넣어서 쓰러지고, 다시 일어나는 애니메이션을 추가하기 위해 5초를 설정했던 건데 리깅 에러로 인해 그냥 멈춰있기만 하도록 구현했습니다.)
// 한마디로 유닛의 State를 관리하고, 전환시키는 스크립트입니다.

using UnityEngine;
using System.Collections;

public enum UnitType { Citizen, Zombie }

public class UnitController : MonoBehaviour
{
    [Header("초기 유닛 타입 설정")]
    public UnitType initialType = UnitType.Citizen;

    [Header("모델 오브젝트")]
    public GameObject citizenModel;
    public GameObject zombieModel;

    public ParticleSystem particle;

    private UnitType currentType;
    private Animator animator;
    private CitizenController citizenController;
    private ZombieRandomWalker zombieController;

    void Awake()
    {
        animator = GetComponent<Animator>();
        citizenController = GetComponentInChildren<CitizenController>(true);
        zombieController = GetComponentInChildren<ZombieRandomWalker>(true);

        SetUnitType(initialType);
    }

    public void SetUnitType(UnitType type)
    {
        currentType = type;

        if (type == UnitType.Citizen)
        {
            // 시민 모델 및 컨트롤러 활성화, 좀비 모델 및 컨트롤러 비활성화
            citizenModel.SetActive(true);
            zombieModel.SetActive(false);
            if (citizenController != null) citizenController.enabled = true;
            if (zombieController != null) zombieController.enabled = false;
            gameObject.tag = "Citizen";
        }
        else if (type == UnitType.Zombie)
        {
            // 현재 시민의 위치와 회전을 저장
            Vector3 lastPosition = citizenModel.transform.position;
            Quaternion lastRotation = citizenModel.transform.rotation;

            // 시민 모델 및 컨트롤러 비활성화
            if (citizenController != null) citizenController.enabled = false;
            citizenModel.SetActive(false);

            // 좀비 모델의 위치와 회전을 시민의 위치와 회전으로 설정
            zombieModel.transform.position = lastPosition;
            zombieModel.transform.rotation = lastRotation;

            // 좀비 모델 및 컨트롤러 활성화
            zombieModel.SetActive(true);
            if (zombieController != null) zombieController.enabled = true;

            gameObject.tag = "Zombie";
        }
    }

    public void Infect()
    {
        if (currentType == UnitType.Citizen)
        {
            StartCoroutine(DelayedInfection());
        }
    }

    private IEnumerator DelayedInfection()
    {
        if (animator != null)
        {
            animator.SetTrigger("Infect");
        }

        // 시민 이동을 멈추고 5초 대기
        if (citizenController != null)
        {
            citizenController.enabled = false;
        }

        yield return new WaitForSeconds(5f);

        // 유닛 타입을 좀비로 변경하고, 구체 파티클 이펙트를 활성화
        SetUnitType(UnitType.Zombie);
        particle.GetComponent<ParticleSystemRenderer>().enabled = true;

        yield return new WaitForSeconds(3f);

        // 구체 파티클 이펙트를 비활성화
        particle.GetComponent<ParticleSystemRenderer>().enabled = false;
    }
}

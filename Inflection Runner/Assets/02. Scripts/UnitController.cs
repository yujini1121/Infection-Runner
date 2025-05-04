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
            citizenModel.SetActive(true);
            zombieModel.SetActive(false);
            if (citizenController != null) citizenController.enabled = true;
            if (zombieController != null) zombieController.enabled = false;
            gameObject.tag = "Citizen";
        }
        else if (type == UnitType.Zombie)
        {
            // 시민의 현재 위치를 저장
            Vector3 lastPosition = citizenModel.transform.position;
            Quaternion lastRotation = citizenModel.transform.rotation;

            // 시민 모델 비활성화 및 컨트롤러 비활성화
            if (citizenController != null) citizenController.enabled = false;
            citizenModel.SetActive(false);

            // 좀비 모델 위치 및 회전 설정
            zombieModel.transform.position = lastPosition;
            zombieModel.transform.rotation = lastRotation;

            // 좀비 모델 활성화 및 컨트롤러 활성화
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

        // 5초 동안 멈춤
        if (citizenController != null)
        {
            citizenController.enabled = false; // 시민 이동을 멈추기
        }

        yield return new WaitForSeconds(5f); // 5초 기다림

        // 좀비로 전환
        SetUnitType(UnitType.Zombie);
    }
}

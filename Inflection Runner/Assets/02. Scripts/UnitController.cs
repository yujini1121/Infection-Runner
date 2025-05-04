using UnityEngine;
using System.Collections;

public enum UnitType { Citizen, Zombie }

public class UnitController : MonoBehaviour
{
    [Header("�ʱ� ���� Ÿ�� ����")]
    public UnitType initialType = UnitType.Citizen;

    [Header("�� ������Ʈ")]
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
            // �ù��� ���� ��ġ�� ����
            Vector3 lastPosition = citizenModel.transform.position;
            Quaternion lastRotation = citizenModel.transform.rotation;

            // �ù� �� ��Ȱ��ȭ �� ��Ʈ�ѷ� ��Ȱ��ȭ
            if (citizenController != null) citizenController.enabled = false;
            citizenModel.SetActive(false);

            // ���� �� ��ġ �� ȸ�� ����
            zombieModel.transform.position = lastPosition;
            zombieModel.transform.rotation = lastRotation;

            // ���� �� Ȱ��ȭ �� ��Ʈ�ѷ� Ȱ��ȭ
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

        // 5�� ���� ����
        if (citizenController != null)
        {
            citizenController.enabled = false; // �ù� �̵��� ���߱�
        }

        yield return new WaitForSeconds(5f); // 5�� ��ٸ�

        // ����� ��ȯ
        SetUnitType(UnitType.Zombie);
    }
}

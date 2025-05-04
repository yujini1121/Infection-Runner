// �� ��ũ��Ʈ�� ������ Ÿ��(�ù� �Ǵ� ����)�� �����ϰ�, ���� ������ ó���մϴ�.
// ���� ��, �ù� ���� ��Ȱ��ȭ�ϰ� ���� ���� Ȱ��ȭ�ϸ�, ���� ��Ʈ�ѷ��� ��ȯ�մϴ�.
// �ù��� ������ �� ���, �ڸ��� ���߰� 5�� �Ŀ� ����� �ٲ��, ���� �¾ ����� 3�ʵ��� ��ü ����Ʈ�� �Բ� ����˴ϴ�.
// (������ �ִϸ��̼� �־ ��������, �ٽ� �Ͼ�� �ִϸ��̼��� �߰��ϱ� ���� 5�ʸ� �����ߴ� �ǵ� ���� ������ ���� �׳� �����ֱ⸸ �ϵ��� �����߽��ϴ�.)
// �Ѹ���� ������ State�� �����ϰ�, ��ȯ��Ű�� ��ũ��Ʈ�Դϴ�.

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
            // �ù� �� �� ��Ʈ�ѷ� Ȱ��ȭ, ���� �� �� ��Ʈ�ѷ� ��Ȱ��ȭ
            citizenModel.SetActive(true);
            zombieModel.SetActive(false);
            if (citizenController != null) citizenController.enabled = true;
            if (zombieController != null) zombieController.enabled = false;
            gameObject.tag = "Citizen";
        }
        else if (type == UnitType.Zombie)
        {
            // ���� �ù��� ��ġ�� ȸ���� ����
            Vector3 lastPosition = citizenModel.transform.position;
            Quaternion lastRotation = citizenModel.transform.rotation;

            // �ù� �� �� ��Ʈ�ѷ� ��Ȱ��ȭ
            if (citizenController != null) citizenController.enabled = false;
            citizenModel.SetActive(false);

            // ���� ���� ��ġ�� ȸ���� �ù��� ��ġ�� ȸ������ ����
            zombieModel.transform.position = lastPosition;
            zombieModel.transform.rotation = lastRotation;

            // ���� �� �� ��Ʈ�ѷ� Ȱ��ȭ
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

        // �ù� �̵��� ���߰� 5�� ���
        if (citizenController != null)
        {
            citizenController.enabled = false;
        }

        yield return new WaitForSeconds(5f);

        // ���� Ÿ���� ����� �����ϰ�, ��ü ��ƼŬ ����Ʈ�� Ȱ��ȭ
        SetUnitType(UnitType.Zombie);
        particle.GetComponent<ParticleSystemRenderer>().enabled = true;

        yield return new WaitForSeconds(3f);

        // ��ü ��ƼŬ ����Ʈ�� ��Ȱ��ȭ
        particle.GetComponent<ParticleSystemRenderer>().enabled = false;
    }
}

// �� ��ũ��Ʈ�� ������Ʈ�� ī�޶� �ٶ󺸰ų�, ī�޶��� ������ ���� ȸ���ϵ��� �����մϴ�.
// �پ��� ��带 ���� ������ �� ������ ���������� ȸ���� �����մϴ�.

using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                // ������Ʈ�� ī�޶� ���� �ٶ󺸵��� �����մϴ�.
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                // ������Ʈ�� ī�޶��� �ݴ� ������ �ٶ󺸵��� �����մϴ�.
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                // ������Ʈ�� ������ ī�޶��� ����� �����ϵ��� �����մϴ�.
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                // ������Ʈ�� ������ ī�޶��� ����� �ݴ밡 �ǵ��� �����մϴ�.
                transform.forward = -Camera.main.transform.forward;
                break;
            default:
                break;
        }
    }
}

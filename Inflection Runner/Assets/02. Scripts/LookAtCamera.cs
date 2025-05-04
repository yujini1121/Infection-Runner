// 이 스크립트는 오브젝트가 카메라를 바라보거나, 카메라의 방향을 따라 회전하도록 제어합니다.
// 다양한 모드를 통해 정방향 및 반전된 방향으로의 회전을 지원합니다.

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
                // 오브젝트가 카메라를 직접 바라보도록 설정합니다.
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                // 오브젝트가 카메라의 반대 방향을 바라보도록 설정합니다.
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                // 오브젝트의 전방이 카메라의 전방과 동일하도록 설정합니다.
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                // 오브젝트의 전방이 카메라의 전방과 반대가 되도록 설정합니다.
                transform.forward = -Camera.main.transform.forward;
                break;
            default:
                break;
        }
    }
}

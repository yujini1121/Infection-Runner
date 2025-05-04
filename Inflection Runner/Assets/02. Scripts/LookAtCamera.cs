using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted, //* ���� ���� ����
        CameraForward,
        CameraForwardInverted, //* ���� ���� ����
    }

    [SerializeField] private Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                //* ī�޶� ������ �˾Ƴ��� �� ���� ��ŭ �����༭ ������Ű��
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                //* ī�޶� �������� Z�� (�յ�)�� �ٲ��ֱ�
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                //* ī�޶� �������� Z�� (�յ�)�� �ٲ��ְ� ������Ű��
                transform.forward = -Camera.main.transform.forward;
                break;
            default:

                break;
        }
    }
}
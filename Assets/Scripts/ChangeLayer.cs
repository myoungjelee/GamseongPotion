using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ChangeLayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� XRGrabInteractable ������Ʈ�� �����ɴϴ�.
        XRGrabInteractable grabInteractable = other.gameObject.GetComponent<XRGrabInteractable>();

        // XRGrabInteractable ������Ʈ�� �����ϸ� ���̾ �����մϴ�.
        if (grabInteractable != null)
        {
            other.gameObject.layer = 0;
            Debug.Log($"���̾ {LayerMask.LayerToName(0)}�� ����Ǿ����ϴ�.");
        }
        else
        {
            Debug.Log("�׷� ����");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �浹�� ��ü�� XRGrabInteractable ������Ʈ�� �����ɴϴ�.
        XRGrabInteractable grabInteractable = other.gameObject.GetComponent<XRGrabInteractable>();

        // XRGrabInteractable ������Ʈ�� �����ϸ� ���̾ �����մϴ�.
        if (grabInteractable != null)
        {
            other.gameObject.layer = 6;
            Debug.Log($"���̾ {LayerMask.LayerToName(6)}�� ����Ǿ����ϴ�.");
        }
        else
        {
            Debug.Log("�׷� ����");
        }
    }
}

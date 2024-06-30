using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HerbGrabInteractable : XRGrabInteractable
{
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    //protected override void OnSelectEntered(SelectEnterEventArgs args)
    //{
    //    base.OnSelectEntered(args);

    //    // �׷��� �� isKinematic�� false�� �����Ͽ� ���� ��ȣ�ۿ��� ����
    //    if (rb != null)
    //    {
    //        rb.isKinematic = false;
    //    }
    //}

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // �׷��� ���� �� isKinematic�� true�� ����
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionGrabInteractable : XRGrabInteractable
{
    private Rigidbody rb;

    public bool isInPocket;

    public Vector3 originScale;

    [Header("����Ʈ")]
    public GameObject effect;
    public AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        isInPocket = false;
        originScale = transform.localScale;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        //rb.isKinematic = false;  

        if (!args.interactorObject.transform.CompareTag("Socket"))
        {
            effect.SetActive(false);
            audioSource.enabled = false;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (isInPocket)
        {
            rb.isKinematic = true;
            movementType = MovementType.Instantaneous;

            // �������� �ڽ����� �缳��
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f); // ������ ã�� ���� ���� �ݰ��� �����մϴ�.
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("MagicPocket"))
                {
                    Transform pocketWorld = hitCollider.transform.Find("PocketWorld");
                    if (pocketWorld != null)
                    {
                        isInPocket = true;
                        transform.SetParent(pocketWorld); // ���Ͽ����� �ڽ����� �����մϴ�.
                        break;
                    }
                }
            }
        }
        else
        {
            rb.isKinematic = false;
            movementType = MovementType.VelocityTracking;

            // �����±׸� ���� �ִ� ���� ������Ʈ�� ã�Ƽ� Ȱ��ȭ
            Transform nameTag = gameObject.transform.Find("NameTag");
            if (nameTag != null)
            {
                nameTag.gameObject.SetActive(true);
            }
        }
    }   
}

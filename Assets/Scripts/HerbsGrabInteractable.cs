using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HerbsGrabInteractable : XRGrabInteractable
{
    private Rigidbody rb;

    public bool isInPocket;

    public Vector3 originScale;

    [Header("����")]
    public GameObject herbLight;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        isInPocket = false; // �ʱ�ȭ
        originScale = transform.localScale;

        //Debug.Log(originScale);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        //rb.isKinematic = false;
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

            if (herbLight != null)
            {
                herbLight.SetActive(false);
            }
        }
        else
        {
            rb.isKinematic = false;
            movementType = MovementType.VelocityTracking;
            
            if (herbLight != null)
            {
                herbLight.SetActive(true);
            }
        }    
    }
}
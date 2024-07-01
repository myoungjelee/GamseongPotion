using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrindingBat : MonoBehaviour
{
    [Header("����� �ǵ��ư���")]
    public Transform grinder; // �������� Transform
    private Vector3 originPos; // ����� ó�� ��ġ
    private Quaternion originRot; // ����� ó�� ȸ����
    private float maxDistance = 1.5f; // �����뿡�� ��� �ִ� �Ÿ�
    private bool isGrabbed = false;

    private void Awake()
    {
        originPos = transform.position;
        originRot = transform.rotation;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, grinder.transform.position);
            if (distance > maxDistance)
            {
                ReturnToOrigin();
            }
        }
    }

    private void ReturnToOrigin()
    {
        transform.position = originPos;
        transform.rotation = originRot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public void OnGrab()
    {
        isGrabbed = true;
    }

    public void UnGrab()
    {
        isGrabbed = false;
    }
}

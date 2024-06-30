using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladle : MonoBehaviour
{
    [Header("����� �ǵ��ư���")]
    public Transform cauldron;
    private Vector3 originPos; // ����� ó�� ��ġ
    private Quaternion originRot; // ����� ó�� ȸ����
    private float maxDistance = 1.8f; 
    public bool isGrabbed = false;

    private void Awake()
    {
        originPos = transform.position;
        originRot = transform.rotation;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, cauldron.position);
            if (distance > maxDistance)
            {
                ReturnToOrigin();
            }
        }
    }
    public void ReturnToOrigin()
    {
        transform.position = originPos;
        transform.rotation = originRot;
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

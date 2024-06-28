using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    public Transform grinder; // �������� Transform
    public Transform batPos; // ��������̰� ���ư� ��ġ (���� ��)
    public float maxDistance = 2.0f; // �����뿡�� ��� �ִ� �Ÿ�
    private bool isGrabbed = false;

    // VR �׷� �̺�Ʈ ó��
    public void OnGrab()
    {
        isGrabbed = true;
    }

    public void OnRelease()
    {
        isGrabbed = false;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, grinder.position);
            if (distance > maxDistance)
            {
                // ��������̰� �����뿡�� �ʹ� �־����� ���� ������ �̵�
                transform.position = batPos.position;
                transform.rotation = batPos.rotation;
            }
        }
    }
}

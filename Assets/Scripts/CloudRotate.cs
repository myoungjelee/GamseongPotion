using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRotate : MonoBehaviour
{
    // ȸ�� �ӵ� (����/��)
    public float rotationSpeed = 1f;

    private void FixedUpdate()
    {
        // Y���� �������� ȸ��
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}

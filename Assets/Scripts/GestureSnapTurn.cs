using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureSnapTurn : MonoBehaviour
{
    private float snapTurnAngle = 45f;

    public void SnapTurnRight()
    {
        // ���� ȸ�� ���� �����ɴϴ�.
        Quaternion currentRotation = transform.rotation;

        // Y���� �������� 45�� ȸ���ϴ� Quaternion�� �����մϴ�.
        Quaternion turnRotation = Quaternion.Euler(0, snapTurnAngle, 0);

        // ���� ȸ���� ���� ���� ȸ���� ���մϴ�.
        transform.rotation = currentRotation * turnRotation;
    }

    public void SnapTurnLeft()
    {
        // ���� ȸ�� ���� �����ɴϴ�.
        Quaternion currentRotation = transform.rotation;

        // Y���� �������� -45�� ȸ���ϴ� Quaternion�� �����մϴ�.
        Quaternion turnRotation = Quaternion.Euler(0, -snapTurnAngle, 0);

        // ���� ȸ���� ���� ���� ȸ���� ���մϴ�.
        transform.rotation = currentRotation * turnRotation;
    }
}

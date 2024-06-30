using UnityEngine;

public class GazeObjectName : MonoBehaviour
{
    public Transform mainCamera;
    public float maxDistance = 10f;
    public LayerMask layerMask;  // �ʿ��� ��� ���̾� ����ũ �߰�

    void Update()
    {
        if (mainCamera == null) return;

        // ����ĳ��Ʈ�� ���� ��ġ�� ���� ����
        Vector3 rayOrigin = mainCamera.transform.position;
        Vector3 rayDirection = mainCamera.transform.forward;

        Ray ray = new Ray(rayOrigin, rayDirection);
        RaycastHit hit;

        // ����� ���� �׸���
        Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * maxDistance, Color.red);

        // Ʈ���� �ݶ��̴����� �浹�� �����Ͽ� ����ĳ��Ʈ
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask, QueryTriggerInteraction.Collide))
        {
            Debug.Log("Hit Object: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log("No hit detected.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineOnHover : MonoBehaviour
{
    public string outlineMaterialPath = "Materials/M_Outline"; // Resources ���� ���� ��Ƽ���� ���

    private Material outlineMaterial;
    private MeshRenderer meshRenderer;
    private Material[] originalMaterials; // ������ ��Ƽ���� �迭

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            // ������ ��Ƽ���� �迭�� �����մϴ�.
            originalMaterials = meshRenderer.materials;
        }

        // Resources �������� �ƿ����� ��Ƽ������ �ε��մϴ�.
        outlineMaterial = Resources.Load<Material>(outlineMaterialPath);

        if (outlineMaterial == null)
        {
            Debug.LogError("Outline Material�� �ε��� �� �����ϴ�. ��θ� Ȯ���ϼ���: " + outlineMaterialPath);
        }
    }

    // ȣ�� ���� �� ȣ��Ǵ� �޼���
    public void OnHoverEnter()
    {
        if (meshRenderer != null && outlineMaterial != null)
        {
            // ���� ��Ƽ���� �迭�� �ƿ����� ��Ƽ������ �߰��մϴ�.
            Material[] newMaterials = new Material[originalMaterials.Length + 1];
            originalMaterials.CopyTo(newMaterials, 0);
            newMaterials[newMaterials.Length - 1] = outlineMaterial;

            // ���ο� ��Ƽ���� �迭�� �����մϴ�.
            meshRenderer.materials = newMaterials;
        }
    }

    // ȣ�� ���� �� ȣ��Ǵ� �޼���
    public void OnHoverExit()
    {
        if (meshRenderer != null)
        {
            // ������ ��Ƽ���� �迭�� �ǵ����ϴ�.
            meshRenderer.materials = originalMaterials;
        }
    }
}

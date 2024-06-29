using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HerbData
{
    public string name;
    public Color color = Color.white;
    public int usePrice;
}

public class Herb : MonoBehaviour
{
    [Header("��� ������")]
    public HerbData data;

    [Header("������ ��Ẵ ���� �Ÿ�")]
    private float spawnDistance = 0.3f;

    public bool isChange = false;

    public void SpawnHerb()
    {
        // Resources �������� ������ �ε�
        GameObject herbPrefab = Resources.Load<GameObject>($"Prefabs/Herbs/{data.name}");

        if (herbPrefab != null)
        {
            // �� ���� ��ġ ���
            Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;

            // �������� �� ���� ��ġ�� �ν��Ͻ�ȭ
            GameObject spawnHerb = Instantiate(herbPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError($"herbPrefab with name {data.name} not found in Resources/Prefabs.");
        }
    }
}

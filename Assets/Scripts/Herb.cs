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

    [Header("�̸�ǥ")]
    public GameObject nameTag;

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
            Herb spawningHerb = spawnHerb.GetComponent<Herb>();
            GameManager.Instance.SubtractCoins(spawningHerb.data.usePrice);
        }
        else
        {
            Debug.LogError($"herbPrefab with name {data.name} not found in Resources/Prefabs.");
        }
    }

    // ȣ�� ���� �� ȣ��Ǵ� �޼���
    public void OnHoverEnter()
    {
        nameTag.SetActive(true);
    }

    // ȣ�� ���� �� ȣ��Ǵ� �޼���
    public void OnHoverExit()
    {
        nameTag.SetActive(false);
    }
}

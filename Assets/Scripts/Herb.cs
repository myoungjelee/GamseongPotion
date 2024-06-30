using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
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

    [Header("��밡��")]
    public GameObject minusCoin;
    public TextMeshProUGUI text_MinusCoin;

    public void SpawnHerb()
    {
        // Resources �������� ������ �ε�
        GameObject herbPrefab = Resources.Load<GameObject>($"Prefabs/Herbs/{data.name}");

        if (herbPrefab != null)
        {
            // �� ���� ��ġ ���
            Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;

            // �������� �� ���� ��ġ�� ����
            GameObject spawnHerb = Instantiate(herbPrefab, spawnPosition, Quaternion.identity);
            Herb spawningHerb = spawnHerb.GetComponent<Herb>();

            //���� ǥ��
            text_MinusCoin.text = $"-{data.usePrice}";
            minusCoin.SetActive(true);
            minusCoin.transform.DOLocalMoveY(-0.08f, 1).OnComplete(() =>
            {
                minusCoin.SetActive(false);
                minusCoin.transform.localPosition = Vector3.zero;
            });
            GameManager.Instance.SubtractCoins(data.usePrice);
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

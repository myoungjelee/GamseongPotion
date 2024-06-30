using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotionData
{
    public string name;
    public int sellPrice;
}

public class Potion : MonoBehaviour
{
    [Header("���� ������")]
    public PotionData data;

    [Header("�̸�ǥ")]
    public GameObject nameTag;

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

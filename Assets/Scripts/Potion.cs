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
    private Transform player;

    private void Start()
    {
        // "Player" �±׸� ���� ���� ������Ʈ�� Transform�� ã���ϴ�.
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // �÷��̾ �ٶ󺸵��� ȸ��
            Vector3 direction = player.position - nameTag.transform.position;
            direction.y = 0; // y �� ȸ���� �����Ͽ� �����±װ� ������ �����ϵ��� ��
            nameTag.transform.rotation = Quaternion.LookRotation(direction);
            nameTag.transform.forward *= -1;
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

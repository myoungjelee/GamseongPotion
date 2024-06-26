using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private List<string> herbPowders = new List<string>();

    [Header("������ �׾Ƹ� ���� �Ÿ�")]
    public float spawnDistance = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HerbPowder"))
        {
            HerbPowder powder = other.gameObject.GetComponent<HerbPowder>();

            if (powder != null)
            {
                // ���縦 �׾Ƹ��� �߰�
                herbPowders.Add(powder.powderName);

                // ���� ������Ʈ ����
                Destroy(other.gameObject);
            }
        }
    }

    // ���� ȣ�� �����ְ� ���� ȣ�����ִ� �κ� �����Ұ�!!!!!!!!!!
    public void Stir()
    {
        // ���ս��� Ȯ���Ͽ� ���� ����
        string potion = CheckCombination();

        if (potion != null)
        {
            Debug.Log("���� ������: " + potion);
            SpawnPotion(potion);
        }
        else
        {
            Debug.Log("��ȿ�� ���ս��� �����ϴ�.");
        }

        // �׾Ƹ� �ʱ�ȭ
        herbPowders.Clear();
    }

    // �����Ұ�!!!! ���ս� �������ٰ�
    private string CheckCombination()
    {
        // ���� ���ս�
        if (herbPowders.Contains("HerbName1") && herbPowders.Contains("HerbName2"))
        {
            return "PotionName1";
        }
        else if (herbPowders.Contains("HerbName3") && herbPowders.Contains("HerbName4"))
        {
            return "PotionName2";
        }

        return null;
    }

    public void SpawnPotion(string potionName)
    {
        // Resources �������� ������ �ε�
        GameObject potionPrefab = Resources.Load<GameObject>($"Prefabs/Potions/{potionName}");

        if (potionPrefab != null)
        {
            // �� ���� ��ġ ���
            Vector3 spawnPosition = transform.position + transform.up * spawnDistance;

            // �������� �� ���� ��ġ�� �ν��Ͻ�ȭ
            Instantiate(potionPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError($"potionPrefab with name {potionName} not found in Resources/Prefabs.");
        }
    }
}

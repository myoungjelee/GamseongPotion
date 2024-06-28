using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private List<string> herbPowders = new List<string>();

    private float spawnDistance = 1.0f;

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
        if (herbPowders.Contains("Mosquito Mushroom") && herbPowders.Contains("Watery plant"))
        {
            return "Acceleration potion";
        }
        else if (herbPowders.Contains("Daffodil of fire") && herbPowders.Contains("Mosquito Mushroom"))
        {
            return "Addiction potion";
        }
        else if (herbPowders.Contains("Watery plant") && herbPowders.Contains("fluffy crystal"))
        {
            return "Coolness potion";
        }
        else if (herbPowders.Contains("fluffy crystal") && herbPowders.Contains("Daffodil of fire"))
        {
            return "Deceleration potion";
        }
        else if (herbPowders.Contains("Blood Garnet") && herbPowders.Contains("Daffodil of fire"))
        {
            return "Explosion potion";
        }
        else if (herbPowders.Contains("Blood Garnet") && herbPowders.Contains("Watery plant"))
        {
            return "Healing potion";
        }
        else if (herbPowders.Contains("Mosquito Mushroom") && herbPowders.Contains("Blood Garnet") && herbPowders.Contains("fluffy crystal"))
        {
            return "Necromancy potion";
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

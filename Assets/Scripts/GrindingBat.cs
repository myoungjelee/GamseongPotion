using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrindingBat : MonoBehaviour
{
    [Header("���� ���� Ƚ��")]
    public int count = 0;

    [Header("���簡 �� Ƚ��")]
    public int grindCount = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Herb"))
        {
            Herb herb = collision.gameObject.GetComponent<Herb>();

            if (herb != null && !herb.isChange)
            {
                count++;

                if (count >= grindCount)
                {
                    // Resources �������� ���� ������ �ε�
                    GameObject powderPrefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herb.data.name}"); 

                    if (powderPrefab != null)
                    {
                        // ���� �������� ��� ��ġ�� ����
                        GameObject powder = Instantiate(powderPrefab, collision.transform.position, collision.transform.rotation);

                        HerbPowder powderScript = powder.GetComponent<HerbPowder>();
                        powderScript.powderName = herb.data.name;
                    }
                    else
                    {
                        Debug.LogError("PowderPrefab�� Resources �������� ã�� �� �����ϴ�.");
                    }

                    // ���� ��� ������Ʈ ����
                    Destroy(collision.gameObject);

                    // ī��Ʈ�� �ʱ�ȭ�ϰ� ��ȯ �÷��� ����
                    count = 0;
                    herb.isChange = true;
                }
            }
        }
    }
}

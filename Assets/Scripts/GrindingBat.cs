using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrindingBat : MonoBehaviour
{
    [Header("���� ���� Ƚ��")]
    public int count = 0;

    [Header("���簡 �� Ƚ��")]
    public int grindCount1 = 3;
    public int grindCount2 = 7;

    private GameObject powder1;

    string herbName;

    [Header("����� �ǵ��ư���")]
    public Transform grinder; // �������� Transform
    private Transform batPos; // ��������̰� ���ư� ��ġ (���� ��)
    public float maxDistance = 2.0f; // �����뿡�� ��� �ִ� �Ÿ�
    private bool isGrabbed = false;

    private void Awake()
    {
        batPos = transform;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, grinder.position);
            if (distance > maxDistance)
            {         
                transform.position = batPos.position;
                transform.rotation = batPos.rotation;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Herb"))
        {
            Herb herb = collision.gameObject.GetComponent<Herb>();

            if (herb != null && !herb.isChange)
            {
                count++;

                if (count >= grindCount1 && count < grindCount2)
                {
                    // ù ��° �ܰ�
                    GameObject powder1Prefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herb.data.name}_1");

                    if (powder1Prefab != null)
                    {
                        // ���� �������� ��� ��ġ�� ����
                        powder1 = Instantiate(powder1Prefab, collision.transform.position, collision.transform.rotation);
                     
                        herbName = herb.data.name;

                        Destroy(collision.gameObject);
                    }
                    else
                    {
                        Debug.LogError("PowderPrefab_1�� Resources �������� ã�� �� �����ϴ�.");
                    }
                }
                else if (count >= grindCount2)
                {
                    // �� ��° �ܰ� (���� ���� ��ȯ)
                    GameObject Powder2Prefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herb.data.name}_2");

                    if (Powder2Prefab != null)
                    {
                        // ���� ���� �������� ��� ��ġ�� ����
                        GameObject powder2 = Instantiate(Powder2Prefab, powder1.transform.position, powder1.transform.rotation);

                        HerbPowder finalPowderScript = powder2.GetComponent<HerbPowder>();
                        finalPowderScript.powderName = herbName;
                    }
                    else
                    {
                        Debug.LogError("PowderPrefab_2�� Resources �������� ã�� �� �����ϴ�.");
                    }

                    // ù��° ����Ŀ�� ������Ʈ ����
                    Destroy(powder1);

                    // ī��Ʈ�� �ʱ�ȭ�ϰ� ��ȯ �÷��� ����
                    count = 0;
                    herb.isChange = true;
                }
            }
        }
    }

    public void OnGrab()
    {
        isGrabbed = true;
    }

    public void UnGrab()
    {
        isGrabbed = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrindingBat : MonoBehaviour
{
    [Header("���� ���� Ƚ��")]
    public int count = 0;

    [Header("���簡 �� Ƚ��")]
    public int grindCount1 = 5;
    public int grindCount2 = 12;

    private GameObject powder1;

    private string herbName;
    private Color herbColor;

    [Header("����� �ǵ��ư���")]
    public Transform grinder; // �������� Transform
    private Vector3 originPos; // ����� ó�� ��ġ
    private Quaternion originRot; // ����� ó�� ȸ����
    private float maxDistance = 1.8f; // �����뿡�� ��� �ִ� �Ÿ�
    private bool isGrabbed = false;

    private bool isProcessing = false; // ���� ������ Ȯ���ϴ� �÷���


    private void Awake()
    {
        originPos = transform.position;
        originRot = transform.rotation;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            float distance = Vector3.Distance(transform.position, grinder.position);
            if (distance > maxDistance)
            {
                ReturnToOrigin();
            }
        }
    }

    private void ReturnToOrigin()
    {
        transform.position = originPos;
        transform.rotation = originRot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrabbed || isProcessing) return; // ���� ���̸� �ٷ� ��ȯ

        if (collision.gameObject.CompareTag("Herb") || collision.gameObject.CompareTag("HerbPowder"))
        {
            Herb herb = collision.gameObject.GetComponent<Herb>();

            if (herb != null && !herb.isChange)
            {
                StartCoroutine(ProcessHerb(collision, herb));
            }
        }
    }

    private IEnumerator ProcessHerb(Collision collision, Herb herb)
    {
        isProcessing = true; // ���� ����

        count++;

        if (count >= grindCount2)
        {
            CreatePowderStage2(herb);
        }
        else if (count >= grindCount1)
        {
            CreatePowderStage1(collision, herb);
        }

        yield return new WaitForSeconds(0.1f); // 0.1�� ���

        isProcessing = false; // ���� �Ϸ�
    }

    private void CreatePowderStage1(Collision collision, Herb herb)
    {
        // ù ��° �ܰ�
        GameObject powder1Prefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herb.data.name}_1");

        if (powder1Prefab != null)
        {
            // ���� �������� ��� ��ġ�� ����
            powder1 = Instantiate(powder1Prefab, collision.transform.position, collision.transform.rotation);

            herbName = herb.data.name;
            herbColor = herb.data.color;

            Destroy(collision.gameObject);
        }
        else
        {
            Debug.LogError($"Prefabs/HerbPowders/P_{herb.data.name}_1�� Resources �������� ã�� �� �����ϴ�.");
        }
    }

    private void CreatePowderStage2(Herb herb)
    {
        // �� ��° �ܰ� (���� ���� ��ȯ)
        GameObject powder2Prefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herb.data.name}_2");

        if (powder2Prefab != null)
        {
            // ���� ���� �������� ��� ��ġ�� ����
            GameObject powder2 = Instantiate(powder2Prefab, powder1.transform.position, powder1.transform.rotation);

            Debug.Log(powder2.gameObject.name);

            HerbPowder finalPowderScript = powder2.GetComponent<HerbPowder>();
            finalPowderScript.powderName = herbName;
            finalPowderScript.powderColor = herbColor;

            // ù ��° ����Ŀ�� ������Ʈ ����
            Destroy(powder1);

            // ī��Ʈ�� �ʱ�ȭ�ϰ� ��ȯ �÷��� ����
            count = 0;
            herb.isChange = true;
        }
        else
        {
            Debug.LogError($"Prefabs/HerbPowders/P_{herb.data.name}_2�� Resources �������� ã�� �� �����ϴ�.");
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

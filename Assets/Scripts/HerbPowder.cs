using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbPowder : MonoBehaviour
{
    public string powderName; // ����� ������ ����
    public Color powderColor;

    [Header("��� ����")]
    //private bool isInGrinder = false;
    private Grinder grinder;
    private int count;
    private int grindCount = 5;
    private int powderState;

    private void Awake()
    {
        // ���̾��Ű���� "Grinder" �±׸� ���� ������Ʈ�� ã��
        grinder = GameObject.FindWithTag("Grinder").GetComponent<Grinder>();

        if (grinder == null)
        {
            Debug.LogError("Grinder�� ã�� �� �����ϴ�. 'Grinder' �±׸� ���� ������Ʈ�� �����ϴ��� Ȯ���ϼ���.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.name.Contains("_2")) return;

        if (collision.gameObject.CompareTag("GrindingBat"))
        {
            if (grinder.isInSide)
            {
                count++;

                Debug.Log(count);

                if (count >= grindCount)
                {
                    CreatePowder(2, this);
                }
            }
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grinder"))
        {
            //isInGrinder = false;
            Debug.Log("Exited Grinder");
        }
    }

    private void CreatePowder(int id, HerbPowder herbPowder)
    {
        // ù ��° �ܰ�
        GameObject powderPrefab = Resources.Load<GameObject>($"Prefabs/HerbPowders/P_{herbPowder.powderName}_{id}");

        if (powderPrefab != null)
        {
            // ���� �������� ��� ��ġ�� ����
            HerbPowder spawnPowder = Instantiate(powderPrefab, transform.position, transform.rotation).GetComponent<HerbPowder>();

            spawnPowder.powderName = powderName;
            spawnPowder.powderColor = powderColor;

            Destroy(gameObject);
        }
        else
        {
            Debug.LogError($"Prefabs/HerbPowders/P_{herbPowder.powderName}_{id}�� Resources �������� ã�� �� �����ϴ�.");
        }
    }
}

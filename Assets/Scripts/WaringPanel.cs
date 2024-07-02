using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class WaringPanel : MonoBehaviour
{
    public Material warningMaterial;
    private bool isFading = false;
    public TextMeshProUGUI[] warningText;
    private void Start()
    {
        warningMaterial.SetFloat("_FadeAmount", 1f);//�������ڸ��� �����ϰ� �����
        warningText[0].DOFade(0f, 1f);
        warningText[1].DOFade(0f, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            warningMaterial.DOFloat(-0.1f, "_FadeAmount", 1f); // 1�� ���� �ִϸ��̼�
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            warningMaterial.DOFloat(1f, "_FadeAmount",1f); // 1�� ���� �ִϸ��̼�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ʈ���� ����");
            // _FadeAmount ���� 1���� 0.1�� ���� (�ִϸ��̼�����)
            if (!isFading)
            {
                warningMaterial.DOFloat(-0.1f, "_FadeAmount", 1f); // 1�� ���� �ִϸ��̼�
                warningText[0].DOFade(1f,1f);
                warningText[1].DOFade(1f,1f);
                isFading = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // _FadeAmount ���� 0.1���� 1�� ���� (�ִϸ��̼�����)
            if (isFading)
            {
                warningMaterial.DOFloat(1f, "_FadeAmount", 1f); // 1�� ���� �ִϸ��̼�
                warningText[0].DOFade(0f, 1f);
                warningText[1].DOFade(0f, 1f);
                isFading = false;
            }
        }
    }
}

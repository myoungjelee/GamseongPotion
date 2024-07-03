using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class WarningPanel : MonoBehaviour
{
    public Material warningMaterial;
    private bool isFading = false;
    public TextMeshProUGUI[] warningText;
    
    private void Start()
    {
        warningMaterial.SetFloat("_FadeAmount", 1f);//�������ڸ��� �����ϰ� �����

        for (int i = 0; i < warningText.Length; i++)
        {
            warningText[i].DOFade(0f, 1f);
        }
    }
    private void OnTriggerEnter(Collider other) //Ʈ���� ���������� ��Ÿ���°�
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ʈ���� ����");
            // _FadeAmount ���� 1���� 0.1�� ���� (�ִϸ��̼�����)
            if (!isFading)
            {
                warningMaterial.DOFloat(-0.1f, "_FadeAmount", 1f); // 1�� ���� �ִϸ��̼�

                for (int i = 0; i < warningText.Length; i++)
                {
                    warningText[i].DOFade(1f, 1f);
                }
                isFading = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIFadeOut();
        }
    }

    public void UIFadeOut()
    {
        // _FadeAmount ���� 0.1���� 1�� ���� (�ִϸ��̼�����)
        if (isFading)
        {
            warningMaterial.DOFloat(1f, "_FadeAmount", 1f); // 1�� ���� �ִϸ��̼�

        }

        isFading = false;
    }
}

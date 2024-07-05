using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class WarningPanel : MonoBehaviour
{
    public Material[] warningMaterials;
    private bool isFading = false;
    public TextMeshProUGUI[] warningTexts;
    
    private void Start()
    {
        //warningMaterial.SetFloat("_FadeAmount", 1f);//�������ڸ��� �����ϰ� �����
        foreach (var warningMaterial in warningMaterials)
        {
            warningMaterial.SetFloat("_FadeAmount", 1f);
        }

        for (int i = 0; i < warningTexts.Length; i++)
        {
            warningTexts[i].DOFade(0, 0f);
        }
    }
    private void OnTriggerEnter(Collider other) //Ʈ���� ���������� ��Ÿ���°�
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Ʈ���� ����");
            // _FadeAmount ���� 1���� 0.1�� ���� (�ִϸ��̼�����)
            if (!isFading)
            {
                foreach(var warningMaterial in warningMaterials)
                {
                    warningMaterial.DOFloat(-0.1f, "_FadeAmount", 1f); // 1�� ���� �ִϸ��̼�
                }

                for (int i = 0; i < warningTexts.Length; i++)
                {
                    warningTexts[i].DOFade(1f, 1f);
                }

                isFading = true;
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.UI);
                //Debug.Log(other.gameObject.name);
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
            foreach (var warningMaterial in warningMaterials)
            {
                warningMaterial.DOFloat(1f, "_FadeAmount", 1f); // 1�� ���� �ִϸ��̼�
            }

            for (int i = 0; i < warningTexts.Length; i++)
            {
                warningTexts[i].DOFade(0, 1f); // �ؽ�Ʈ�� ������� �����
            }

            isFading = false;
            //AudioManager.Instance.PlaySfx(AudioManager.Sfx.UI);
        }


    }
}

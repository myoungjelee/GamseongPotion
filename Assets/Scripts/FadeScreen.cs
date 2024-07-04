using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    private Renderer rend;
    private Vector3 originalPosition; // ���� ��ġ�� ������ ����

    private void Start()
    {
        rend = GetComponent<Renderer>();
        originalPosition = transform.position; // ���� ��ġ ����
    }

    public void FadeIn()
    {
        Fade(1, 0);
        Debug.Log("���̵���");
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.006f);
    }

    public void FadeOut()
    {
        Fade(0, 1);
        Debug.Log("���̵�ƿ�");
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.006f);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;     // �� �����Ӹ� ��ٸ���
        }

        // ���̵尡 ���� �� ���� ��ġ�� �ǵ�����
        transform.position = originalPosition;
    }
}

using UnityEngine;
using DG.Tweening;

public class FloatingObject : MonoBehaviour
{
    public float minFloatDuration = 1f; // �������� �������� �ּ� �ð�
    public float maxFloatDuration = 3f; // �������� �������� �ִ� �ð�
    public float minFloatStrength = 0.2f; // �ּ� �̵� �Ÿ� (����)
    public float maxFloatStrength = 1f; // �ִ� �̵� �Ÿ� (����)

    void Start()
    {
        StartFloating();
    }

    void StartFloating()
    {
        // �ʱ� ��ġ ����
        Vector3 originalPosition = transform.localPosition;

        // ������ �������� ���̿� �ð� ����
        float floatDuration = Random.Range(minFloatDuration, maxFloatDuration);
        float floatStrength = Random.Range(minFloatStrength, maxFloatStrength);

        // �յ� ���ٴϴ� �ִϸ��̼�
        transform.DOLocalMoveY(originalPosition.y + floatStrength, floatDuration)
            .SetLoops(-1, LoopType.Yoyo) // ���� �ݺ�, Yoyo ������� (���� ��ġ�� ���ƿԴٰ� �ٽ� �̵�)
            .SetEase(Ease.Linear); // �ϸ��ϰ� �̵�
    }
}

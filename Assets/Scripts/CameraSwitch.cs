using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    public Camera initialCamera; // �ʱ� ī�޶�
    public Camera vrCamera; // VR ī�޶�
    public Image startAnim;
    private float switchTime = 2f; // ��ȯ �ð�

    void Start()
    {
        if (GameManager.Instance.currentState == GameManager.State.CanSleep)
        {
            initialCamera.enabled = true;
            vrCamera.enabled = false;

            // DOTween �ִϸ��̼� ��� �� �Ϸ� �� ī�޶� ��ȯ
            startAnim.GetComponent<DOTweenAnimation>().RecreateTweenAndPlay();

            StartCoroutine(SwitchCameraRoutine());

            GameManager.Instance.currentState = GameManager.State.BeAwake;
        }
    }

    IEnumerator SwitchCameraRoutine()
    {
        yield return new WaitForSeconds(switchTime);

        SwitchToVRCamera();
    }

    void SwitchToVRCamera()
    {
        initialCamera.enabled = false;
        vrCamera.enabled = true;
    }
}
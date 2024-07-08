using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private WarningPanel warningPanel;

    private void Awake()
    {
         warningPanel = transform.parent.gameObject.GetComponent<WarningPanel>();
    }

    public void OnClick_GoToBedRoom()
    {
        warningPanel.UIFadeOut();
        GameManager.Instance.GoToBedRoom();
    }

    public void OnClick_GoToMainHall()
    {
        warningPanel.UIFadeOut();
        GameManager.Instance.GoToMainHall();
    }

    public void OnClick_GoToNextDay()
    {
        GameManager.Instance.currentState = GameManager.State.Sleeping;
        warningPanel.UIFadeOut();
        GameManager.Instance.GoToNextDay();
        GameManager.Instance.SaveGameData();
    }

    public void OnClick_CancleButton()
    {
        warningPanel.UIFadeOut();
    }

    public void OnClick_GoToEnding()
    {
        warningPanel.UIFadeOut();
        GameManager.Instance.GoToEnding();
        GameManager.Instance.SaveGameData();
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameCoRoutine());
    }

    IEnumerator QuitGameCoRoutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        // ����Ƽ �����Ϳ��� ���� ���� ���
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���� �Ϸ��� �������Ͽ��� ���� ���� ���
        Application.Quit();
#endif
    }
}

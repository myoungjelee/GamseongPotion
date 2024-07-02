using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("�̱���")]
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public enum State
    {
        BeAwake,          // �����ִ� ����
        Sleeping,       // �ڴ� �� ����
        CanSleep        // �� �� �ִ� ����
    }

    [Header("���º�ȯ")]
    private State currentState;

    [Header("���嵥����")]
    private int totalCoin;
    private int date;

    [SerializeField] private TextMeshProUGUI text_CoinBank;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        totalCoin = 1000;
        SetCoinText(totalCoin);

        currentState = State.Sleeping;
    }

    public void AddCoins(int amount)
    {
        totalCoin += amount;
        SetCoinText(totalCoin);
    }

    public void SubtractCoins(int amount)
    {
        totalCoin -= amount;
        SetCoinText(totalCoin);
    }

    private void SetCoinText(int coin)
    {
        text_CoinBank.text = $"{coin}\u20A9";
    }

    public void GoToMainHall()
    {
        SceneManager.sceneLoaded += OnMainHallLoaded;
        SceneManager.LoadScene("MainHall");
    }

    private void OnMainHallLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainHall")
        {
            // PlayerStart��� �̸��� ���� ������Ʈ�� ã��
            GameObject playerStart = GameObject.Find("PlayerStart");
            if (playerStart != null)
            {
                // �÷��̾��� ��ġ�� ȸ�� ���� PlayerStart ������Ʈ�� ��ġ�� ȸ�� ������ ����
                transform.position = playerStart.transform.position;
                transform.rotation = playerStart.transform.rotation;
            }
            else
            {
                Debug.LogWarning("PlayerStart ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }

        // �̺�Ʈ ���� ����
        SceneManager.sceneLoaded -= OnMainHallLoaded;
    }

    public void GoToBedRoom()
    {
        switch (currentState)
        {
            case State.BeAwake:
                SceneManager.sceneLoaded += OnBedRoomLoaded;
                SceneManager.LoadScene("BedRoom_Morning");
                break;

            case State.Sleeping:
                SceneManager.sceneLoaded += OnBedRoomLoaded;
                SceneManager.LoadScene("BedRoom_Morning");
                break;

            case State.CanSleep:
                SceneManager.sceneLoaded += OnBedRoomLoaded;
                SceneManager.LoadScene("BedRoom_Night");
                break;
        }
    }

    private void OnBedRoomLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("BedRoom"))
        {
            GameObject playerStart;
            switch (currentState)
            {
                case State.BeAwake:
                case State.CanSleep:
                    // PlayerStart��� �̸��� ���� ������Ʈ�� ã��
                    playerStart = GameObject.Find("PlayerStart_BeAwake");
                    if (playerStart != null)
                    {
                        // �÷��̾��� ��ġ�� ȸ�� ���� PlayerStart ������Ʈ�� ��ġ�� ȸ�� ������ ����
                        transform.position = playerStart.transform.position;
                        transform.rotation = playerStart.transform.rotation;
                    }
                    else
                    {
                        Debug.LogWarning("PlayerStart_BeAwake ������Ʈ�� ã�� �� �����ϴ�.");
                    }
                    break;

                case State.Sleeping:
                    // PlayerStart��� �̸��� ���� ������Ʈ�� ã��
                    playerStart = GameObject.Find("PlayerStart_Bed");
                    if (playerStart != null)
                    {
                        // �÷��̾��� ��ġ�� ȸ�� ���� PlayerStart ������Ʈ�� ��ġ�� ȸ�� ������ ����
                        transform.position = playerStart.transform.position;
                        transform.rotation = playerStart.transform.rotation;
                    }
                    else
                    {
                        Debug.LogWarning("PlayerStart_Bed ������Ʈ�� ã�� �� �����ϴ�.");
                    }
                    break;
            }
        }

        // �̺�Ʈ ���� ����
        SceneManager.sceneLoaded -= OnBedRoomLoaded;
    }
}

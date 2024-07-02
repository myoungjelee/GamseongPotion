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
        Awake,          // �����ִ� ����
        Sleeping,       // �ڴ� �� ����
        CanSleep        // �� �� �ִ� ����
    }

    [Header("���º�ȯ")]
    private State currentState;

    [Header("���嵥����")]
    private int totalCoin;
    private int date;

    [SerializeField]private TextMeshProUGUI text_CoinBank;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("MainHall");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void GoToBedRoom()
    {
        switch (currentState)
        {
            case State.Awake:
                SceneManager.LoadScene("Start Scenes");
                break;
            case State.CanSleep:
                SceneManager.LoadScene("Start Scenes");
                break;
            default:
                break;
        }
    }
}

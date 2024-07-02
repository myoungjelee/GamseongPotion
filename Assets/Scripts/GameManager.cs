using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int totalCoin;

    public TextMeshProUGUI text_CoinBank;

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

    private int date;
    private bool isSleep;
    private int money;

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

        // ��Ȱ��ȭ�� ������Ʈ�� �����Ͽ� ��� TextMeshProUGUI ������Ʈ�� ã���ϴ�.
        TextMeshProUGUI[] textComponents = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();

        // ������ ������Ʈ�� Ȯ���Ͽ� �̸��� "Text_CoinBank"�� ������Ʈ�� ã���ϴ�.
        foreach (TextMeshProUGUI tmp in textComponents)
        {
            if (tmp.gameObject.name == "Text_CoinBank")
            {
                text_CoinBank = tmp;
                break;
            }
        }

        totalCoin = 1000;
        SetCoinText(totalCoin);
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
}

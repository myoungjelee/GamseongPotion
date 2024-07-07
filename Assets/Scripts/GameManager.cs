using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;



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
    public State currentState;
    public int customerCount;

    [Header("���嵥����")]
    private int totalCoin;
    public int date;

    [Header("���̵���/�ƿ�")]
    public FadeScreen fadeScreen;
    private bool isSceneChanging;
    public CharacterController characterController;
    public GameObject locomotion;
   

    //public XRController

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
            Destroy(gameObject);
        }

        date = 1;
        totalCoin = 1000;
        SetCoinText(totalCoin);

        currentState = State.CanSleep;
    }

    private void Start()
    {
        WakeUp();
        AudioManager.Instance.PlayBgm("BedRoom_Morning");
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
        GoToSceneAsync("MainHall");

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
                gameObject.transform.position = playerStart.transform.position;
                gameObject.transform.rotation = playerStart.transform.rotation;
                //fadeScreen.FadeIn();
                isSceneChanging = false; // �÷��� ����
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
            case State.Sleeping:
                DOTween.KillAll();
                SceneManager.sceneLoaded += OnBedRoomLoaded;
                GoToSceneAsync("BedRoom_Morning");
                break;

            case State.CanSleep:
                DOTween.KillAll();
                SceneManager.sceneLoaded += OnBedRoomLoaded;
                GoToSceneAsync("BedRoom_Night");
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
                        //fadeScreen.FadeIn();
                        isSceneChanging = false; // �÷��� ����
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
                        WakeUp();
                        isSceneChanging = false; // �÷��� ����
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

    public void GoToNextDay()
    {
        SceneManager.sceneLoaded += OnBedRoomLoaded;
        GoToSceneAsync("BedRoom_Morning");
        SetCalendar();
    }

    public void SetCalendar()
    {
        date++;
        GameManager.Instance.customerCount = 0;
    }

    public void GoToSceneAsync(string sceneName)
    {
        if(!isSceneChanging)
        {
            StartCoroutine(SceneChangeAsync(sceneName));
           // Debug.Log("�� ��ȯ");
        }
    }

    IEnumerator SceneChangeAsync(string sceneName)
    {
        isSceneChanging = true; // �÷��� ����

        AudioManager.Instance.StopAllSfx();
        locomotion.SetActive(false);
        DOTween.KillAll();
        fadeScreen.FadeOut();
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while(timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
        AudioManager.Instance.PlayBgm(sceneName);
        fadeScreen.FadeIn();
        locomotion.SetActive(true);
    }

    void WakeUp()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(1);
        locomotion.SetActive(false);
        fadeScreen.FadeIn();

        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        fadeScreen.FadeOut();

        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        fadeScreen.FadeIn();

        transform.position = new Vector3(-0.5f, 0, -2.3f);
        transform.rotation = Quaternion.identity;
        currentState = State.BeAwake;
        locomotion.SetActive(true);
    }

    public void GoToEnding()
    {
        SceneManager.sceneLoaded += OnEndingLoaded;
        GoToSceneAsync("Ending_Credit");
    }

    void OnEndingLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Ending"))
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            characterController.enabled = false;
            locomotion.SetActive(false);

            DOTweenAnimation Anim = FindFirstObjectByType<DOTweenAnimation>();
            if(Anim != null )
            {
                Anim.RecreateTweenAndPlay();
            }
            else
            {
                Debug.Log("�ִϸ��̼� ��ã��");
            }
        }
    }
}

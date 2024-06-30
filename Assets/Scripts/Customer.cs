using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using DG.Tweening;
using UnityEditor.Rendering;

public class Customer : MonoBehaviour
{
    private Vector3 initPos;
    
    [Header("��Ų")]
    public Mesh[] skins;
    private SkinnedMeshRenderer skinRenderer;

    [Header("��ȭ")]
    public GameObject textUI;
    public TextMeshProUGUI dialogueText;
    public string selectedDialogue;
    private string currentDialogue;
    private string correctAnswer;
    private string[] dialogues = new string[21]
    {
        "ġ���� ���� �Ĵ°� ����? �Ѻ� �ֽ���. Ȥ�� ���� �Ѻ� �常�صη� �մϴ�",
        "*�ݷ�*  *�ݷ�* ����*�ݷ�* *�ݷ�* �ּ���*�ݷ�* ",
        "���� �翡�� �۹��� ��Ȯ�ϸ� �� �� ���� �ϰ� �־��µ�, ���ڱ� ���𰡰� �� �߸��� ��� �� ���������ϴ�! ���ϱ� Ŀ�ٶ� ����������. �༮�� �� ���� ��ô ���Ŵϴ�. ġ�����ֽðھ��?",
        "�����ڸ�, ���� \"������� ��\" ����� ������� ���� ���ٰ� �սô�. �׷� ������� ���Ⱑ �ǰ��� ���� ���ٰ� ����������. ���뿡 �߰��Ͽ� ���⸦ ��... �ǰ��ϰ� ���鸸�� ���� �������",
        "���������� ���� ħ���߾��! ���� ������ �׵��� �ұ��� �Ǿ����ϴ�! �����ؿ�! �� �̷��� �� �� ���ٰ��! ���� �� �������� �� ���� ���� �� �ִ� ���� �ּ���.",
        "��� �����ڸ� ���� ��ġ�� ����� �������? ���Ŀ� ���� Ÿ�� ���� �������?",
        "���� �����ؾ� �ϴµ�, �׷� �ֹ��� �ϳ��� �𸨴ϴ�... �����ֽ� �� �ֳ���?",
        "���꿡�� ���ϴµ� ���� ������ �ʿ��մϴ�.",
        "���� ���̳� ��ö ���� ����ġ�� �� ������ �ɸ��� ������ �ֳ���?",
        "������ �ĳ´µ� ������ �����ϴ�����. ���� ���մ� ���� ���Դϴ�. �༮���� ���� ���� �׳���� �ǲ� �󸱸��� ������ �ʿ��մϴ�.",
        "�õ� �������� ���峵���ϴ�! ��⳪ ä�� ���� ������ �ż��ϰ� ���������ִ� ������ �ʿ��մϴ�.",
        "�ȳ��ϼ���! �ƽôٽ��� ���ִ� ������ ���ž� �����ϴ�. ������ ���� �����̳� ���ݱ������� �״�� �����ϱⰡ �������. ������ û������ �����Ǵ� ���ָ� ����� �;��! ������ �� ���� ������ �������?",
        "���忡 �� ������ ���� ���� ������ ������ ���� �ִٴ� �� �ߵ鵵 �ƴ� �� �����ϴ�. �׷��� ��ģ ���� �޾Ƴ�������! �� ���̿� �༮���� ������ ����� �ִ� �� �ְ� �ͽ��ϴ�.",
        "�ȳ��ϼ���! ������. �� ������ ��ģ ����Ⱑ �־��. ���ò۵��� ��� �ȴ�ϴ�. �̳��� ���ڸ��� ��ģ ���� ȭ�� ���� �׾�� �ϰ� ����Ŀ�. �� � �����ٷε� ������ �� ����! �̳��� ������� �ӵ��� ���ߴ� ������ �ٸ��� �;��. ���� �����ּ���!",
        "����� ���� �ӵ��� �����ִ� ������ �ʿ��մϴ�.",
        "������κ��� ����ġ�ų� ���� �� ������ �ɸ��� ������ �ʿ��մϴ�.",
        "�� ���� ���ֿ��� �̱�� ���� ������ �ʿ��մϴ�.",
        "�� �ź��̸�ŭ �������� �ͽ��ϴ�! ��, � �ź��̴� ���� �������� �����ϰŵ��!",
        "���� �ϰ� �ִ� ���迡 �ּ��� ������ �ʿ��մϴ�.",
        "�����̿� ���ڸ� �ǻ츮�� ������ �ɰ� �ͽ��ϴ�.",
        "�����Ͻ� �׺��� ��ȯ�ϰ�ͽ��ϴ�! "
    };

    private void Awake()
    {
        skinRenderer = transform.Find("Character").GetComponent<SkinnedMeshRenderer>();
        initPos = new Vector3(-3f, 0, 11.1f);
        transform.position = initPos;
    }

    private void OnEnable()
    {
        StartCoroutine(InitCustomer());     
    }

    IEnumerator InitCustomer()
    {
        SetRandomCharacter();
        float randomDelay = Random.Range(2f, 6f); // 2�ʿ��� 5�� ������ ���� �ð�
        yield return new WaitForSecondsRealtime(randomDelay);
        transform.DOMoveX(-0.16f, 2).OnComplete(() => SetText());
    }

    private void OnDisable()
    {
        //StartCoroutine(InitCustomer());
        transform.position = initPos;
        textUI.SetActive(false);
    }

    void SetRandomCharacter()
    {
        int randomDialogue = Random.Range(0, dialogues.Length);
        selectedDialogue = dialogues[randomDialogue];

        int randomMesh = Random.Range(0, skins.Length);
        skinRenderer.sharedMesh = skins[randomMesh];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SetText();
        }
    }

    void SetText()
    {
        textUI.SetActive(true);
        dialogueText.text = "";
        dialogueText.DOKill();
        dialogueText.DOText(selectedDialogue, 3);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dialogueText.DOKill();
            textUI.SetActive(false);
            Debug.Log("Ʈ���� �ƿ�");
        }
    }

    public bool OnPotionResult(string potionName)
    {
        string correctPotion = GetCorrectPotionForDialogue(selectedDialogue);

        if (potionName == correctPotion)
        {
            return true;
        }
        else
        {        
            return false;
        }
    }

    string GetCorrectPotionForDialogue(string dialogue)
    {
        if (dialogue.Contains("ġ��") || dialogue.Contains("�ݷ�") || dialogue.Contains("ġ��")) return "Healing potion";
        if (dialogue.Contains("�ǰ�") || dialogue.Contains("��������") || dialogue.Contains("������")) return "Addiction potion";
        if (dialogue.Contains("����") || dialogue.Contains("����") || dialogue.Contains("��")) return "Explosion potion";
        if (dialogue.Contains("�ǲ�") || dialogue.Contains("�õ�") || dialogue.Contains("����")) return "Coolness potion";
        if (dialogue.Contains("����") || dialogue.Contains("�����") || dialogue.Contains("�ӵ�")) return "Deceleration potion";
        if (dialogue.Contains("���") || dialogue.Contains("����") || dialogue.Contains("�ź���")) return "Acceleration potion";
        if (dialogue.Contains("�ּ�") || dialogue.Contains("����") || dialogue.Contains("��ȯ")) return "Necromancy potion";

        return string.Empty;
    }
}

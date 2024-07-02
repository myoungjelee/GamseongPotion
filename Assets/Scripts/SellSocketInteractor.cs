using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;
using System.Collections; 

public class SellSocketInteractor : XRSocketInteractor
{
    [Header("�մ�")]
    public Customer customer;

    private void Awake()
    {
        StartCoroutine(StartVisitCustormer());
    }

    IEnumerator StartVisitCustormer()
    {
        yield return new WaitForSeconds(6);

        customer.gameObject.SetActive(true);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (customer == null) return;

        XRBaseInteractable interactableObject = args.interactableObject as XRBaseInteractable;
        if (interactableObject != null)
        {
            Potion potion = interactableObject.GetComponent<Potion>();

            if (potion != null)
            {
                if (customer.OnPotionResult(potion.data.name))
                {
                    // ���� ���� (���������� ����� �ּ�Ǯ��)
                    GameObject coinPrefab = Resources.Load<GameObject>($"Prefabs/Coin");
                    if (coinPrefab != null)
                    {
                        GameObject spawnCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
                        Coin coin = spawnCoin.GetComponent<Coin>();
                        if (coin != null)
                        {
                            coin.coin = potion.data.sellPrice;
                        }
                    }
                    else
                    {
                        Debug.LogError("���� �������� �ε��� �� �����ϴ�.");
                    }

                    Destroy(interactableObject.gameObject);

                    // ��� �Ϸ� �� �̵�
                    if (customer.currentCoroutine != null)
                    {
                        StopCoroutine(customer.currentCoroutine);
                        customer.currentCoroutine = null;
                    }
                    customer.dialogueText.text = "";
                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(customer.dialogueText.DOText("�����ϴ�!", 1));
                    sequence.Append(customer.transform.DOMoveX(3, 3));
                    sequence.OnComplete(() => StartCoroutine(CustomerOnOff()));
                }
                else
                {
                    StartCoroutine(WrongAnswer());
                }
            }
        }
        else
        {
            Debug.LogError("���� NULL");
        }
    }

    IEnumerator CustomerOnOff()
    {
        customer.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        customer.gameObject.SetActive(true);
    }

    IEnumerator WrongAnswer()
    {
        if (customer.currentCoroutine != null)
        {
            StopCoroutine(customer.currentCoroutine);
            customer.currentCoroutine = null;
        }
        customer.dialogueText.text = "";
        Tween wrongTween = customer.dialogueText.DOText("�̰� ���� ���� ���� �ƴϿ���.", 1);
        yield return wrongTween.WaitForCompletion();

        yield return new WaitForSeconds(0.5f);

        customer.SetText();
    }
}

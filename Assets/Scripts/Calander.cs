using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calander : MonoBehaviour
{
    public TextMeshProUGUI dateText;

    void Start()
    {
        dateText.text = $"DAY {GameManager.Instance.date}";
        //Debug.Log($"���� ��¥ : {GameManager.Instance.date}");
    }
}

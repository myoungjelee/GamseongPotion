using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HerbData
{
    public string name;
    public Color color = Color.white;
    public int usePrice;
}

public class Herb : MonoBehaviour
{
    [Header("재료 데이터")]
    public HerbData data;

    [Header("생성시 재료병 앞쪽 거리")]
    private float spawnDistance = 0.3f;

    [Header("이름표")]
    public GameObject nameTag;

    public void SpawnHerb()
    {
        // Resources 폴더에서 프리팹 로드
        GameObject herbPrefab = Resources.Load<GameObject>($"Prefabs/Herbs/{data.name}");

        if (herbPrefab != null)
        {
            // 병 앞쪽 위치 계산
            Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;

            // 프리팹을 병 앞쪽 위치에 인스턴스화
            GameObject spawnHerb = Instantiate(herbPrefab, spawnPosition, Quaternion.identity);
            Herb spawningHerb = spawnHerb.GetComponent<Herb>();
            GameManager.Instance.SubtractCoins(spawningHerb.data.usePrice);
        }
        else
        {
            Debug.LogError($"herbPrefab with name {data.name} not found in Resources/Prefabs.");
        }
    }

    // 호버 시작 시 호출되는 메서드
    public void OnHoverEnter()
    {
        nameTag.SetActive(true);
    }

    // 호버 종료 시 호출되는 메서드
    public void OnHoverExit()
    {
        nameTag.SetActive(false);
    }
}

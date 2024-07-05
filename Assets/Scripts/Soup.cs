using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soup : MonoBehaviour
{
    private List<string> herbPowders = new List<string>();
    private MeshRenderer mdshRenderer;
    private Color originColor;
    private Color currentColor;
    public ParticleSystem soupParticle;
    private float spawnDistance = 1.0f;

    private float currentTime;
    private float spawnTime = 10f;

    [SerializeField] private Image progressBar; 
    [SerializeField] private GameObject progressCanvas;

    public bool isPotionSpawn;

    private void Awake()
    {
        mdshRenderer = GetComponent<MeshRenderer>();
        originColor = mdshRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HerbPowder"))
        {
            HerbPowder powder = other.gameObject.GetComponent<HerbPowder>();

            if (powder != null)
            {
                // ���縦 �׾Ƹ��� �߰�
                herbPowders.Add(powder.powderName);

                // ���� ȥ��
                MixColor(powder.powderColor);

                // ���� ������Ʈ ����
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.CompareTag("Ladle"))
        {
            Ladle ladle = other.gameObject.GetComponent<Ladle>();

            if (ladle.isGrabbed) AudioManager.Instance.PlaySfx(AudioManager.Sfx.Bubble);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladle"))
        {
            currentTime = 0;
            UpdateProgressBar(0); // ���α׷��� �� �ʱ�ȭ
            progressBar.color = Color.white;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ladle"))
        {
            Ladle ladle = other.gameObject.GetComponent<Ladle>();

            if (ladle.isGrabbed)
            {
                currentTime += Time.deltaTime;
                UpdateProgressBar(currentTime / spawnTime); // ���α׷��� �� ������Ʈ

                if (currentTime > spawnTime)
                {
                    Stir();    
                }
            }
            else
            {
                currentTime = 0;
                UpdateProgressBar(0); // ���α׷��� �� �ʱ�ȭ
                AudioManager.Instance.StopSfx(AudioManager.Sfx.Bubble);
                progressBar.color = Color.white;
            }
        }
    }

    private void MixColor(Color newColor)
    {
        // ���� ����� ���ο� ������ ȥ��
        currentColor = Color.Lerp(currentColor, newColor, 0.6f);

        // ������ ���� ����
        mdshRenderer.material.color = currentColor;
        soupParticle.startColor = currentColor;
    }

    public void Stir()
    {
        string potion = CheckCombination();

        if (potion != null)
        {
            SpawnPotion(potion);
            currentTime = 0;
            //progressBar.color = Color.green;
        }
        else
        {
            progressBar.color = Color.red;
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Fail);
            //Debug.Log("��ȿ�� ���ս��� �����ϴ�.");
        }

        // �׾Ƹ� �ʱ�ȭ
        herbPowders.Clear();

        // ���� �ʱ�ȭ
        currentColor = originColor;
        mdshRenderer.material.color = currentColor;
        soupParticle.startColor = currentColor;
    }

    private string CheckCombination()
    {
        //foreach (string herb in herbPowders)
        //{
        //    Debug.Log($"Herb in list: {herb}");
        //}

        if (herbPowders.Contains("Mosquito Mushroom") && herbPowders.Contains("Watery Plant"))
        {
            return "Acceleration Potion";
        }
        else if (herbPowders.Contains("Daffodil Of Fire") && herbPowders.Contains("Mosquito Mushroom"))
        {
            return "Addiction Potion";
        }
        else if (herbPowders.Contains("Watery Plant") && herbPowders.Contains("Fluffy Crystal"))
        {
            return "Coolness Potion";
        }
        else if (herbPowders.Contains("Fluffy Crystal") && herbPowders.Contains("Daffodil Of Fire"))
        {
            return "Deceleration Potion";
        }
        else if (herbPowders.Contains("Blood Garnet") && herbPowders.Contains("Daffodil Of Fire"))
        {
            return "Explosion Potion";
        }
        else if (herbPowders.Contains("Blood Garnet") && herbPowders.Contains("Watery Plant"))
        {
            return "Healing Potion";
        }
        else if (herbPowders.Contains("Mosquito Mushroom") && herbPowders.Contains("Blood Garnet") && herbPowders.Contains("Fluffy Crystal"))
        {
            return "Necromancy Potion";
        }

        return null;
    }

    public void SpawnPotion(string potionName)
    {
        GameObject potionPrefab = Resources.Load<GameObject>($"Prefabs/Potions/{potionName}");

        if (potionPrefab != null)
        {
            Vector3 spawnPosition = transform.position + transform.up * spawnDistance;
            Instantiate(potionPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError($"potionPrefab with name {potionName} not found in Resources/Prefabs.");
        }
    }

    // UpdateProgressBar �޼���� ���α׷��� ��(progressBar)�� ���� ���¸� ������Ʈ�ϰ�,
    // Ư�� ���ǿ� ���� ���α׷��� ���� Ȱ��ȭ ���θ� �����մϴ�.
    private void UpdateProgressBar(float progress)
    {
        if (progressBar != null)
        {
            if(!isPotionSpawn)
            {
                progressBar.fillAmount = Mathf.Clamp01(progress);
                bool shouldShowProgressBar = progress > 0f;
                progressCanvas.gameObject.SetActive(shouldShowProgressBar);              
            }
            else
            {
                progressCanvas.gameObject.SetActive(false);
                AudioManager.Instance.StopSfx(AudioManager.Sfx.Bubble);
            }


            //if (shouldShowProgressBar)
            //{
            //    // ���α׷��� ĵ������ �÷��̾� �������� ȸ��
            //    Vector3 directionToPlayer = Player.transform.position - progressCanvas.transform.position;
            //    directionToPlayer.y = 0; // ���� ȸ���� ���
            //    progressCanvas.transform.rotation = Quaternion.LookRotation(directionToPlayer);
            //}
        }
    }
}

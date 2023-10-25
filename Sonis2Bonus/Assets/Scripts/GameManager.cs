using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [Header("Stats")]
    public float rings;
    public float lives;
    public float metres;
    public bool isDead;

    [Header("Time")]
    public float countdownStart;
    public float timeSpawnCurrently;
    public float timeSpawn1;
    public float timeSpawn2;
    public float timeSpawn3;

    [Header("Positions")]
    public float posSpawn;
    public float posFinal;
    public float roadSpeed;

    [Header("Difficulty")]
    public float difficultyLevel;
    public float metresLevel2;
    public float metresLevel3;

    [Header("UI")]
    [SerializeField] Text txtLives;
    [SerializeField] Text txtMetres;
    [SerializeField] Text txtRings;
    [SerializeField] GameObject gameOverPanel;

    [Header("Player")]
    [SerializeField] Player player;

    void Awake()
    {
        Application.targetFrameRate = 60;
        gameManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic("Music");
        StartCoroutine(GenerateTemplate());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            metres += Time.deltaTime * 1;
            AdjustDifficulty();
        }
        UpdateUI();
    }

    public void AdjustDifficulty()
    {
        if (Time.frameCount % 5 == 0)
        {
            if (metres >= metresLevel3)
                difficultyLevel = 3;
            else if (metres >= metresLevel2)
                difficultyLevel = 2;
            else
                difficultyLevel = 1;
        }
    }

    public void UpdateUI()
    {
        txtLives.text = lives.ToString() + " lives";
        txtMetres.text = metres.ToString("0.0") + " m";
        txtRings.text = rings.ToString() + " rings";
    }

    public void ResetGame()
    {
        SceneLoader.instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddRing()
    {
        rings++;
    }

    public void AddLive()
    {
        lives++;
    }
    public void RemoveLive()
    {
        lives--;
        if (lives <= 0)
            GameOver();
    }

    public void GameOver()
    {
        lives = 0;
        isDead = true;
        gameOverPanel.SetActive(true);
        Destroy(player.gameObject);
    }

    IEnumerator GenerateTemplate()
    {
        yield return new WaitForSeconds(countdownStart);

        while (!isDead)
        {
            switch (difficultyLevel)
            {
                case 1:
                    timeSpawnCurrently = timeSpawn1;
                    break;

                case 2:
                    timeSpawnCurrently = timeSpawn2;
                    break;

                case 3:
                    timeSpawnCurrently = timeSpawn3;
                    break;
            }

            yield return new WaitForSeconds(timeSpawnCurrently);

            Vector3 templatePosition = new Vector3(5, 0, posSpawn - Mathf.Abs(posFinal));
            int rnd = Random.Range(1, 6);
            string templateTag = difficultyLevel.ToString() + "-" + rnd.ToString();
            ObjectPooler.instance.SpawnFromPool(templateTag, templatePosition, Quaternion.identity);
        }
    }
}
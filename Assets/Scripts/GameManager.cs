using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Scripts variables
    private Enemy enemyScript;
    private HealthSystem healthSystem;
    private PlayerController playerController;
    private PostProcessing postProcessingScript;

    //GameObjects variables 
    public GameObject[] powerupPrefabs;
    public GameObject enemyPrefab;
    public GameObject healthPrefab;
    public GameObject titleScreen;
    public GameObject creditsScreen;

    //UI and audio variables
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI versionText;
    public Image healthImage;
    public Button restartButton;
    public Button resumeButton;
    public Button quitButton;
    public Button mainMenuButton;
    public AudioSource camAudio;
    public AudioClip clickSound;
    public AudioClip clickBackSound;
    public AudioClip damageSound;

    //Float variables
    private float spawnRangeX = 21f;
    private float spawnRangeY = 20f;
    private float lerpSpeed = 5f;
    private float maxPitch = 1.5f;
    private float minPitch = 1f;
    private float increasingSpeed = 0.05f;
    private float volumeScale = 2f;

    //Int variables
    public int powerupCount;
    public int enemyCount;
    public int randomPowerupPerWave;
    private int waveNumber = 1;
    private int spawnPickupsLimitation = 10;
    private int score;
    private int maxPowerupPerWave = 3;
    private int minPowerupPerWave = 1;

    //Bool variables
    public bool canResume = false;

    //Transform variables
    public Transform playerPos;
    
    
    void Start()
    {
    
        enemyScript = enemyPrefab.gameObject.GetComponent<Enemy>();
        healthSystem = GameObject.Find("Player").GetComponent<HealthSystem>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        camAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        postProcessingScript = GameObject.Find("Post Process Volume").GetComponent<PostProcessing>();
        
        camAudio.Stop();

        score = 0;
        UpdateScore(0);
    }

    void Update()
    {
        randomPowerupPerWave = Random.Range(minPowerupPerWave, maxPowerupPerWave);
        enemyCount = FindObjectsOfType<Enemy>().Length;
        powerupCount = FindObjectsOfType<Powerup>().Length;

        if(enemyCount == 0)
        {
            enemyScript.speed += increasingSpeed;
            waveNumber++;
            SpawnEnemyWave(waveNumber);


            if(powerupCount <= spawnPickupsLimitation)
            SpawnRandomPowerupNum(randomPowerupPerWave);
        }

        //Inputs
        if(Input.GetKeyDown(KeyCode.Tab) && !canResume && !playerController.gameOver)
        {
            Time.timeScale = 0;
            canResume = true;
            UISetFalse();
            camAudio.Pause();
            resumeButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        }
        
        
        if(postProcessingScript.canChangePitch == true)
        {
            camAudio.pitch = Mathf.SmoothDamp(camAudio.pitch, maxPitch, ref lerpSpeed, lerpSpeed);
        }
        else 
        {
            camAudio.pitch = Mathf.Lerp(camAudio.pitch, minPitch, lerpSpeed * Time.deltaTime);
        }

        UpdateHealth(healthSystem.health);
        
    }

    private void SpawnRandomPowerupNum(int length)
    {
        for (int i = 0; i < length; i++)
        {
            int index = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[index], GenerateSpawnPos(), powerupPrefabs[index].transform.rotation); 
        }
    }

    private void SpawnEnemyWave(int length)
    {
        for (int i = 0; i < length; i++)
        {
           Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation); 
        }
    }

    private Vector2 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosY = Random.Range(-spawnRangeY, spawnRangeY);

        Vector2 spawnPos = new Vector2(spawnPosX, spawnPosY);
        Vector2 playerPosV2 = playerPos.transform.position;
        Vector2 zero = Vector2.zero;
        float distance = 10f;

        if(Vector2.Distance(spawnPos, playerPosV2) > distance)
        {
            return spawnPos;
        }
        else
        {
            return zero;
        }

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
         
        scoreText.text = "Score\n " + score;
        
    }
    public void UpdateHealth(int i)
    {
        healthText.text = healthSystem.health.ToString();
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);   
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        camAudio.PlayOneShot(clickBackSound, volumeScale);
    }

    public void StartGame()
    {
        playerController.gameOver = false;
        titleScreen.gameObject.SetActive(false);
        versionText.gameObject.SetActive(false);
        UISetActive();
        camAudio.Play();
        
    }

    public void StopMusic()
    {
        camAudio.Stop();
    }

    public void AfterResume()
    {
        mainMenuButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    public void UISetFalse()
    {
        healthText.gameObject.SetActive(false);
        healthImage.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
    }

    public void UISetActive()
    {
        scoreText.gameObject.SetActive(true);
        healthText.gameObject.SetActive(true);
        healthImage.gameObject.SetActive(true);
    }

    public void ChangeToCredits()
    {
        titleScreen.gameObject.SetActive(false);
        creditsScreen.gameObject.SetActive(true);
        playerController.gameObject.SetActive(false);

    }

    public void ChangeToTitleScreen()
    {
        titleScreen.gameObject.SetActive(true);
        creditsScreen.gameObject.SetActive(false);
        playerController.gameObject.SetActive(true);
    }

    public void ButtonSound()
    {
        camAudio.PlayOneShot(clickSound, volumeScale);
    }

    public void BackButtonSound()
    {
        camAudio.PlayOneShot(clickBackSound, volumeScale);
    }

    public void HurtSound()
    {
        camAudio.PlayOneShot(damageSound, volumeScale);
    }
}

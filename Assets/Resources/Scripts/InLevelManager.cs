using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class InLevelManager : MonoBehaviour
{
    public static InLevelManager Instance;

    public SpawnEnemy[] spawnEnemies;
    public int maxEnemyCount;

    private int enemyCount = 0;
    private Transform player;
    

    public TextMeshProUGUI quantity;
    public int enemyDeathCount;

    public GameObject LoseGamePanel;
    public GameObject WinGamePanel;
    public Button btn_backToHome;
    public Button btn_nextLevel;
    public Button btn_retry;
    public GameObject AwardPanel;
    public GameObject PausePanel;
    public GameObject CoinHolderInGame;
    public GameObject coinSummary;
    
    public float realTimeRun;
    public int realHealth;

    private bool isPaused = false;
    private int curentIndexScene;

    public AwardManager award;

    private GameObject pfDamagePopup;
    public Transform spawnPlayer;

    private int coinAwardInGame;
    private TextMeshProUGUI textCoinAward;
    private TextMeshProUGUI textCoinSummary;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        Instance = this;
        realTimeRun -= Time.deltaTime;
        AwardPanel.SetActive(false);
        PausePanel.SetActive(false);
        pfDamagePopup = Resources.Load<GameObject>("Prefabs/pfDamagePopup");
    }
    private void Start()
    {
        
        InitializePlayer();

        for (int i = 0; i < spawnEnemies.Length; i++)
        {
            maxEnemyCount += spawnEnemies[i].maxEnemyCount;
        }


        curentIndexScene = SceneManager.GetActiveScene().buildIndex;

        enemyCount = maxEnemyCount;
        enemyDeathCount = 0;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        player.transform.position = spawnPlayer.position;
        
        this.quantity.text = "0 / " + maxEnemyCount.ToString();

        coinAwardInGame = 0;
        textCoinAward = CoinHolderInGame.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
        textCoinSummary = coinSummary.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
        textCoinAward.text = "0";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                RemuseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private GameObject InitializePlayer()
    {
        int selectedIndex = OfflineManager.Instance.idCharacter;

        Debug.Log("id selected " + selectedIndex);

        GameObject player = Instantiate(Resources.Load<GameObject>($"Prefabs/PlayerPrefab/{selectedIndex}") as GameObject, Vector2.zero, Quaternion.identity);
        float moveSpeed = OfflineManager.Instance.characterMoveSpeed;
        float health = OfflineManager.Instance.characterHealth;
        player.GetComponent<PlayerCtrl>().speed = moveSpeed;
        player.GetComponent<Health>().maxHealth = health;
        return player;
    }
    
    public void EnemyDestroyed()
    {
        // Giảm biến đếm quái vật
        enemyCount--;
        this.quantity.text = enemyDeathCount.ToString() + " / " + maxEnemyCount.ToString();
        if (enemyCount <= 0)
        {
            WinGame();
        }
    }
    public void LoseGame()
    {
        LoseGamePanel.SetActive(true);
        AwardPanel.SetActive(true);
        btn_backToHome.gameObject.SetActive(true);
        btn_retry.gameObject.SetActive(true);
        AudioManager.Instance.PlaySFX("game_over");
        AudioManager.Instance.musicSource.Stop();
        award.GameOver(0);
        UpdateCoinInGame(0);
        SaveCoinAward(coinAwardInGame);
        CoinHolderInGame.SetActive(false);
        coinSummary.SetActive(true);
    }
    public void WinGame()
    {
        WinGamePanel.SetActive(true);
        AwardPanel.SetActive(true);
        btn_backToHome.gameObject.SetActive(true);
        btn_nextLevel.gameObject.SetActive(true);
        AudioManager.Instance.PlaySFX("win_game");
        AudioManager.Instance.musicSource.Stop();
        SetCoditionStar();
        ResetLevel();
        SaveCoinAward(coinAwardInGame);
        CoinHolderInGame.SetActive(false);
        coinSummary.SetActive(true);
    }
    public void PauseGame()
    {
        PausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }
    public void RemuseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
    private void ResetLevel()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        if (obj != null)
            Destroy(obj);
        Time.timeScale = 1;
    }
    public void OnBackToHomeButtonClick()
    {
        
        ResetLevel();
        SceneManager.LoadScene("Offline");
        AudioManager.Instance.PlaySFX("button_click");
        AudioManager.Instance.PlayMusic("background");
    }

    public void OnNextLevelButtonClick()
    {
        SceneManager.LoadScene(curentIndexScene + 1);
    }
    public void OnRetryButtonClick()
    {
        SceneManager.LoadScene(curentIndexScene);
    }
    private void SetCoditionStar()
    {
        if (player.gameObject.GetComponent<Health>().GetCurrentHealth() >= realHealth)
        {
            //3 sao
            award.GameOver(3);
            UpdateCoinInGame(300);
        }
        else if (realTimeRun >= 0)
        {
            //2sao
            award.GameOver(2);
            UpdateCoinInGame(200);
        } 
        else
        {
            //1 sao
            award.GameOver(1);
            UpdateCoinInGame(100);
        }
    }

    public GameObject CreatePopup(Vector2 position, int damageAmount)
    {
        //Debug.Log(pfDamage.name);
        //GameObject damagePopupTransform = Instantiate(Resources.Load<GameObject>("Prefabs/pfDamagePopup"), position, Quaternion.identity);
        GameObject damagePopupTransform = Instantiate(pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);
        return damagePopupTransform;
    }
    
    public void UpdateCoinInGame(int coin)
    {
        coinAwardInGame += coin;
        textCoinAward.text = "" + coinAwardInGame;
        textCoinSummary.text = "+" + coinAwardInGame;
    }
    private void SaveCoinAward(int coin)
    {
        int currentCoin = PlayerPrefs.GetInt("PLAYER_COIN");
        PlayerPrefs.SetInt("PLAYER_COIN", currentCoin + coin);
        PlayerPrefs.Save();
    }
}

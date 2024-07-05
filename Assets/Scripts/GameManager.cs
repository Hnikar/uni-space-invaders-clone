using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject StageClearUI;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text livesText;

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;

    private int score;
    private int lives;

    public int Score => score;
    public int Lives => lives;

    private int savedHighscore;

    [SerializeField] 
    private Text highScoreEndText;
    [SerializeField] 
    private Text highScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>();
        mysteryShip = FindObjectOfType<MysteryShip>();
        bunkers = FindObjectsOfType<Bunker>();

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        gameOverUI.SetActive(false);
        StageClearUI.SetActive(false);
        player.gameObject.SetActive(true);
        SetScore(0);
        SetLives(3);
        for (int i = 0; i < bunkers.Length; i++)
        {
            bunkers[i].ResetBunker();
        }
        NewRound();
    }

    private IEnumerator NewRoundCoroutine()
    {
        yield return new WaitForSeconds(3f);

        StageClearUI.SetActive(false);
        Respawn();
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);
    }

    private void NewRound()
    {
        StageClearUI.SetActive(false);
        Respawn();
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        SetHighscore(score);
        gameOverUI.SetActive(true);
        invaders.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(4, '0');
    }

    private void SetHighscore(int score)
    {
        this.score = score;
        if(savedHighscore < score)
        {
            savedHighscore = score;
            highScoreEndText.text = ("New Highscore: " + savedHighscore.ToString().PadLeft(4, '0'));
            highScoreText.text = (savedHighscore.ToString().PadLeft(4, '0'));
        }
        else highScoreEndText.text = null;

    }

    private void SetLives(int lives)
    {
        this.lives = Mathf.Max(lives, 0);
        livesText.text = this.lives.ToString();
    }

    private IEnumerator RespawnDelay(){
        yield return new WaitForSeconds(3);
        Respawn();
    }

    public void OnPlayerKilled(Player player)
    {
        SetLives(lives - 1);

        player.gameObject.SetActive(false);

        if (lives > 0)
        {
            StartCoroutine(RespawnDelay());
        }
        else
        {
            GameOver();
        }
    }

    public void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);
       
        SetScore(score + invader.score);
        if (invaders.GetAliveCount() == 0)
        {
            score += 1000;
            SetScore(score + invader.score);
            player.gameObject.SetActive(false);
            StageClearUI.SetActive(true);
            StartCoroutine(NewRoundCoroutine());
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        SetScore(score + mysteryShip.score);
    }

    public void OnBoundaryReached()
    {
        if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false);

            player.gameObject.SetActive(false);
            GameOver();
        }
    }
}

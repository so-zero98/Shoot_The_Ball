using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeAttackGameManager : MonoBehaviour
{
    public enum gameState { ready, game, over };
    gameState whatState = gameState.ready;

    enum homeState { paused, result };
    homeState whatHomeState = homeState.paused;

    public GameObject mainPanel;
    public GameObject countdownPanel;
    public GameObject resultPanel;
    public GameObject pausedPanel;
    public GameObject quitPanel;
    public GameObject askGoToHomePanel;
    public GameObject orbitObject;

    public float score = 0;

    public FrontRim front;
    public LeftRim left;
    public RightRim right;
    public BackRim back;
    public FirstZone first;
    public DataProcessing processing;
    public PlayerController player;
    public SoundManager soundManager;

    public Text scoreText;
    public Text timeText;
    public Text countdownText;
    public Text finalscoreText;

    public Slider powerBar;
    public Slider orbitBar;

    public Image pressureFill;

    AudioSource audioSource;
    public AudioClip countDownSound;
    public AudioClip startSound;
    public AudioClip getPointSound;
    public AudioClip buttonSound;

    [SerializeField] float playTime = 60f;
    float countdownTime = 4.5f;
    bool isPaused;
    bool isAudioPlayed;

    void Awake()
    {
        front = GameObject.Find("Rim").GetComponentInChildren<FrontRim>();
        left = GameObject.Find("Rim").GetComponentInChildren<LeftRim>();
        right = GameObject.Find("Rim").GetComponentInChildren<RightRim>();
        back = GameObject.Find("Rim").GetComponentInChildren<BackRim>();
        first = GameObject.Find("ShootZone").GetComponentInChildren<FirstZone>();
        processing = GameObject.Find("DataProcessing").GetComponent<DataProcessing>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(soundManager.SelectBGM("TimeAttackScene"));
        mainPanel.SetActive(false);
        resultPanel.SetActive(false);
        pausedPanel.SetActive(false);
        quitPanel.SetActive(false);
        askGoToHomePanel.SetActive(false);
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            whatHomeState = homeState.paused;
            Time.timeScale = 0;
            soundManager.soundPaused = true;
            isPaused = true;
            pausedPanel.SetActive(true);
            return;
        }

        switch (whatState)
        {
            case gameState.ready:
                if (Mathf.Ceil(countdownTime) == 0)
                {
                    StartCoroutine(PlayCountdownSound());
                    countdownText.text = "START";
                    countdownTime -= Time.deltaTime;
                }
                else if (Mathf.Ceil(countdownTime) > 3f)
                {
                    countdownTime -= Time.deltaTime;
                }
                else if (countdownTime > 0 && countdownTime <= 3f)
                {
                    StartCoroutine(PlayCountdownSound());
                    countdownText.text = Mathf.Ceil(countdownTime).ToString();
                    countdownTime -= Time.deltaTime;
                }
                else if (countdownTime < 0)
                {
                    countdownPanel.SetActive(false);
                    whatState = gameState.game;
                    Debug.Log("게임시작");
                }
                break;

            case gameState.game:
                score = 0;
                StartCoroutine(player.ControllerOperating());

                if (playTime > 0)
                {
                    mainPanel.SetActive(true);
                    timeText.text = Mathf.Ceil(playTime).ToString();
                    playTime -= Time.deltaTime;
                }
                else if (Mathf.Ceil(playTime) == 0)
                {
                    whatState = gameState.over;
                }

                orbitBar.value = Mathf.Abs(orbitObject.transform.rotation.x / 7) * 10f;
                powerBar.value = 1;

                if (powerBar.value >= 0.99f)
                    pressureFill.color = new Color(0, 255f / 255f, 0);
                else
                    pressureFill.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);

                if (first.firstInClear && first.firstExitClear)
                {
                    if (front.onFrontCollision || left.onLeftCollision || right.onRightCollision || back.onBackCollision)
                    {
                        audioSource.clip = getPointSound;
                        audioSource.volume = 0.1f;
                        audioSource.Play();
                        Debug.Log("Not Perfect");
                        score += 10f;
                        scoreText.text = score.ToString();
                    }
                    else if (!front.onFrontCollision && !left.onLeftCollision && !right.onRightCollision && !back.onBackCollision)
                    {
                        audioSource.clip = getPointSound;
                        audioSource.volume = 0.1f;
                        audioSource.Play();
                        Debug.Log("Perfect");
                        score += 20f;
                        scoreText.text = score.ToString();
                    }

                    first.firstInClear = false;
                    first.firstExitClear = false;
                }
                break;

            case gameState.over:
                whatHomeState = homeState.result;
                finalscoreText.text = score.ToString();
                mainPanel.SetActive(false);
                resultPanel.SetActive(true);
                Debug.Log("gameover");
                break;
        }
    }

    IEnumerator PlayCountdownSound()
    {
        audioSource.volume = 0.3f;

        if (Mathf.Ceil(countdownTime) > 2 && Mathf.Ceil(countdownTime) <= 3 && isAudioPlayed == false)
        {
            audioSource.PlayOneShot(countDownSound);
            isAudioPlayed = true;
        }
        else if (Mathf.Ceil(countdownTime) > 1 && Mathf.Ceil(countdownTime) <= 2 && isAudioPlayed == true)
        {
            audioSource.PlayOneShot(countDownSound);
            isAudioPlayed = false;
        }
        else if (Mathf.Ceil(countdownTime) > 0 && Mathf.Ceil(countdownTime) <= 1 && isAudioPlayed == false)
        {
            audioSource.PlayOneShot(countDownSound);
            isAudioPlayed = true;
        }
        else if (Mathf.Ceil(countdownTime) <= 0 && isAudioPlayed == true)
        {
            audioSource.PlayOneShot(startSound);
            isAudioPlayed = false;
        }

        yield return null;
    }

    public void onPausedButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();
        Time.timeScale = 0;
        soundManager.soundPaused = true;
        isPaused = true;
        pausedPanel.SetActive(true);
        return;
    }

    public void onRetryButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void onBackButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();
        pausedPanel.SetActive(false);
        soundManager.soundPaused = false;
        Time.timeScale = 1;
        isPaused = false;
        return;
    }

    public void onHomeButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();

        if (whatHomeState == homeState.paused)
        {
            pausedPanel.SetActive(false);
            askGoToHomePanel.SetActive(true);
        }
        else if (whatHomeState == homeState.result)
        {
            whatState = gameState.over + 1;
            resultPanel.SetActive(false);
            askGoToHomePanel.SetActive(true);
        }
    }

    public void onYesHomeButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void onNoHomeButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();

        if (whatHomeState == homeState.result && whatState == gameState.over + 1)
        {
            askGoToHomePanel.SetActive(false);
            resultPanel.SetActive(true);
            whatState = gameState.over;
        }
        else if (whatHomeState == homeState.paused)
        {
            askGoToHomePanel.SetActive(false);
            pausedPanel.SetActive(true);
        }
    }

    public void onQuitButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();
        pausedPanel.SetActive(false);
        quitPanel.SetActive(true);
    }

    public void onYesButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();
        Application.Quit();
    }

    public void onNoButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();
        quitPanel.SetActive(false);
        pausedPanel.SetActive(true);
    }
}

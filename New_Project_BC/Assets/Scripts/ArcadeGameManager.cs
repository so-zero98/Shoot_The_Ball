using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArcadeGameManager : MonoBehaviour
{
    public enum gameState { ready, game, over };
    gameState whatState = gameState.ready;

    enum homeState { paused, result };
    homeState whatHomeState = homeState.paused;

    public static float arcadeScore = 0;
    public static float arcadeStage = 0;

    public HoopController hoopController;

    public GameObject mainPanel;
    public GameObject countdownPanel;
    public GameObject resultPanel;
    public GameObject pausedPanel;
    public GameObject quitPanel;
    public GameObject askGoToHomePanel;
    public GameObject orbitObject;

    public float currentStage = 1;
    public float currentScore = 0;
    public float goalScore = 0; 
    public float pressureValue;

    public bool setDirection = false;

    public FrontRim front;
    public LeftRim left;
    public RightRim right;
    public BackRim back;
    public FirstZone first;
    public DataProcessing processing;
    public PlayerController player;
    public SoundManager soundManager;

    public Text currentScoreText;
    public Text goalScoreText;
    public Text stageText;
    public Text countdownText;
    public Text resultStageText;
    public Text resultScoreText;

    public Slider orbitBar;
    public Slider powerBar;

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
        hoopController = GameObject.Find("Hoop").GetComponent<HoopController>();
        processing = GameObject.Find("DataProcessing").GetComponent<DataProcessing>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(soundManager.SelectBGM("ArcadeScene"));
        mainPanel.SetActive(false);
        resultPanel.SetActive(false);
        pausedPanel.SetActive(false);
        quitPanel.SetActive(false);
        askGoToHomePanel.SetActive(false);
        currentScoreText.text = currentScore.ToString();
        goalScoreText.text = goalScore.ToString();
        stageText.text = currentStage.ToString();
        StartCoroutine(hoopController.AppearDisappear(1));
        goalScore = DataManager.arcadeGoalScore;
        currentStage = DataManager.arcadeStage;
        StartCoroutine(DataManager.arcadeData(currentStage));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            whatHomeState = homeState.paused;
            Time.timeScale = 0;
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
                    mainPanel.SetActive(true);
                    whatState = gameState.game;
                    Debug.Log("게임시작");
                    stageText.text = DataManager.arcadeStage.ToString();
                    goalScoreText.text = DataManager.arcadeGoalScore.ToString();
                }
                break;

            case gameState.game:
                currentScoreText.text = currentScore.ToString();
                StartCoroutine(player.ControllerOperating());

                if (currentScore >= DataManager.arcadeGoalScore)
                {
                    currentScoreText.text = DataManager.arcadeGoalScore.ToString();
                    whatState = gameState.over;
                }
                
                if (currentStage == 4)  // 1만큼 양옆으로 움직이고 10점
                { 
                    hoopController.moveSpeed = 1f;
                    StartCoroutine(hoopController.MoveLeftRight());
                }
                else if (currentStage == 5)  // 1만큼 양옆으로 움직이고 13점
                {
                    hoopController.moveSpeed = 1f;
                    StartCoroutine(hoopController.MoveLeftRight());
                }
                else if (currentStage == 6)  // 1만큼 위아래로 움직이고 10점
                {
                    hoopController.moveSpeed = 1f;
                    StartCoroutine(hoopController.MoveUpDown());
                }
                else if (currentStage == 7)  // 1만큼 위아래로 움직이고 13점
                {
                    hoopController.moveSpeed = 1f;
                    StartCoroutine(hoopController.MoveUpDown());
                }
                else if (currentStage == 8)  // 2만큼 양옆으로 움직이고 10점
                {
                    hoopController.moveSpeed = 2f;
                    StartCoroutine(hoopController.MoveLeftRight());
                }
                else if (currentStage == 9)  // 2만큼 양옆으로 움직이고 13점
                {
                    hoopController.moveSpeed = 2f;
                    StartCoroutine(hoopController.MoveLeftRight());
                }
                else if (currentStage == 10)  // 2만큼 위아래로 움직이고 10점
                { 
                    hoopController.moveSpeed = 2f;
                    StartCoroutine(hoopController.MoveUpDown());
                }
                else if (currentStage == 11)  // 2만큼 위아래로 움직이고 13점
                {
                    hoopController.moveSpeed = 2f;
                    StartCoroutine(hoopController.MoveUpDown());
                }
                else if (currentStage == 12) // 나타났다 사라졌다 10점
                {
                    hoopController.disappearHoop = true;
                }
                else if (currentStage == 13) // 나타났다 사라졌다 13점
                {
                    hoopController.disappearHoop = true;
                }
                else if (currentStage == 14) // 나타났다 사라지면서 양옆으로 1만큼 움직이고 10점
                {
                    hoopController.disappearHoop = true;
                    hoopController.moveSpeed = 1f;
                    StartCoroutine(hoopController.MoveLeftRight());
                }
                else if (currentStage == 15) // 나타났다 사라지면서 양옆으로 2만큼 움직이고 13점
                {
                    hoopController.disappearHoop = true;
                    hoopController.moveSpeed = 2f;
                    StartCoroutine(hoopController.MoveLeftRight());
                }

                orbitBar.value = Mathf.Abs(orbitObject.transform.rotation.x / 7) * 10;
                powerBar.value = processing.pressure_val / 100f;

                if (powerBar.value >= 0.99f)
                    pressureFill.color = new Color(0, 255f / 255f, 0);
                else
                    pressureFill.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);

                if (first.firstInClear && first.firstExitClear)
                {
                    if (front.onFrontCollision || left.onLeftCollision || right.onRightCollision || back.onBackCollision)
                    {
                        Debug.Log("Not Perfect");
                        audioSource.clip = getPointSound;
                        audioSource.volume = 0.1f;
                        audioSource.Play();
                        currentScore += 10f;
                        currentScoreText.text = currentScore.ToString();
                    }
                    else if (!front.onFrontCollision && !left.onLeftCollision && !right.onRightCollision && !back.onBackCollision)
                    {
                        Debug.Log("Perfect");
                        audioSource.clip = getPointSound;
                        audioSource.volume = 0.1f;
                        audioSource.Play();
                        currentScore += 20f;
                        currentScoreText.text = currentScore.ToString();
                    }

                    first.firstInClear = false;
                    first.firstExitClear = false;
                }
                break;

            case gameState.over:
                whatHomeState = homeState.result;
                resultStageText.text = currentStage.ToString();
                resultScoreText.text = currentScore.ToString();
                mainPanel.SetActive(false);
                resultPanel.SetActive(true);
                Debug.Log("gameover");
                break;
        }
    }

    IEnumerator PlayCountdownSound()
    {
        audioSource.volume = 0.1f;

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
        SceneManager.LoadScene(2);
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

    public void onNextButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();
        resultPanel.SetActive(false);
        DataManager.arcadeStage += 1;
        DataManager.arcadeData(DataManager.arcadeStage);
        SceneManager.LoadScene(2);
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
}

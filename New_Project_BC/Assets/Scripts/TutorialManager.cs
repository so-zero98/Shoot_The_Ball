using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject explanationPanel;
    public GameObject mainPanel;
    public GameObject pausedPanel;
    public GameObject quitPanel;
    public GameObject askGoToHomePanel;
    public GameObject hoop;
    public GameObject controller;
    public GameObject controllerBackPanel;
    
    public Material pressureButtonColor;

    [SerializeField] Gradient textGradient;
    [Range(0, 1)] float gradientValue;

    public Animator controllerAnimator;

    public Text explanationText;

    public Slider powerBar;
    public Slider orbitBar;

    public Image pressureFill;

    [SerializeField] float time = 0f;
    [SerializeField] float setTime =5f;
    [SerializeField] bool orderStart = false;
    [SerializeField] bool increaseValue = false;
    [SerializeField] bool isChangeColor = false;
    [SerializeField] int done = 0;
    [SerializeField] int tutorialOrder = 0;
    [SerializeField] AudioClip completeSound;
    [SerializeField] AudioClip buttonSound;

    public DataProcessing processing;
    public PlayerController player;
    public SoundManager soundManager;

    AudioSource audioSource;

    bool isPaused;
    bool isSetActive;

    void Awake()
    {
        controllerAnimator = GameObject.Find("ControllerObj").GetComponent<Animator>();
        processing = GameObject.Find("DataProcessing").GetComponent<DataProcessing>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(soundManager.SelectBGM("TutorialScene"));;
        explanationPanel.SetActive(false);
        mainPanel.SetActive(false);
        pausedPanel.SetActive(false);
        quitPanel.SetActive(false);
        askGoToHomePanel.SetActive(false);
        hoop.SetActive(false);
        controller.SetActive(false);
        controllerBackPanel.SetActive(false);
        StartCoroutine(Explanation(1));
        gradientValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            soundManager.soundPaused = true;
            isPaused = true;
            pausedPanel.SetActive(true);
            controller.SetActive(false);
            controllerBackPanel.SetActive(false);
            return;
        }

        if (processing.pressure_val >= 99 && tutorialOrder == 1 && orderStart == true)
        {
            done = 1;
            controller.SetActive(false);
            controllerBackPanel.SetActive(false);
            audioSource.clip = completeSound;
            audioSource.volume = 0.1f;
            audioSource.Play();
            StartCoroutine(Explanation(2));
        }

        if (done == 1 && tutorialOrder == 2 && orderStart == true)
        {
            orbitBar.value = Mathf.Abs(transform.rotation.x / 7) * 10;
            powerBar.value = processing.pressure_val * Time.deltaTime * 0.5f;

            if (powerBar.value >= 0.99f)
                pressureFill.color = new Color(0, 255f / 255f, 0);
            else
                pressureFill.color = new Color(255f / 255f, 255f / 255f, 255f / 255f);

            StartCoroutine(player.ControllerOperating());

            if (player.onBall == false)
            {
                done = 2;
                controllerBackPanel.SetActive(false);
                controller.SetActive(false);
                audioSource.clip = completeSound;
                audioSource.volume = 0.1f;
                audioSource.Play();
                StartCoroutine(Explanation(3));
            }
        }

        if (isChangeColor == true)
            StartCoroutine(DelayChangeColor());
        else if (isChangeColor == false)
            pressureButtonColor.color = new Color(0, 0, 0);
    }

    IEnumerator Explanation(int order)
    {
        if (order == 1)
        {
            tutorialOrder = order;
            yield return new WaitForSeconds(1.5f);
            explanationPanel.SetActive(true);
            explanationText.text = "�ȳ��ϼ���";
            yield return new WaitForSeconds(3f);
            explanationText.text = "�������� ���ӿ� �ռ�";
            yield return new WaitForSeconds(3f);
            explanationText.text = "���� ����� �˷� �帮�ڽ��ϴ�";
            yield return new WaitForSeconds(3f);
            explanationText.text = "��Ʈ�ѷ� ������ ��ư�� �� ����������";
            yield return new WaitForSeconds(3f);
            explanationPanel.SetActive(false);
            controllerBackPanel.SetActive(true);
            isSetActive = true;
            controller.SetActive(true);
            controllerAnimator.SetBool("isRotate", true);
            isChangeColor = true;
            orderStart = true;
        }
        else if (order == 2 && done == 1)
        {
            orderStart = false;
            tutorialOrder = order;
            isChangeColor = false;
            isSetActive = false;
            yield return new WaitForSeconds(1.5f);
            explanationPanel.SetActive(true);
            explanationText.text = "�����ϴ�";
            yield return new WaitForSeconds(3f);
            explanationText.text = "��� ������ ��ư�� �Ƿ°��� �����մϴ�";
            yield return new WaitForSeconds(3f);
            explanationText.text = "�̹����� �� �����⸦ ������ڽ��ϴ�";
            yield return new WaitForSeconds(3f);
            explanationText.text = "��Ʈ�ѷ� ������ ��ư�� ������ ���鼭";
            yield return new WaitForSeconds(3f);
            explanationText.text = "��Ʈ�ѷ��� ������ ����̸� ���� ���� �� �ֽ��ϴ�";
            yield return new WaitForSeconds(3f);
            explanationPanel.SetActive(false);
            controllerBackPanel.SetActive(true);
            isSetActive = true;
            controller.SetActive(true);
            controllerAnimator.SetBool("isThrow", true);
            mainPanel.SetActive(true);
            orderStart = true;
        }
        else if (order == 3 && done == 2)
        {
            orderStart = false;
            isSetActive = false;
            tutorialOrder = order;
            yield return new WaitForSeconds(1.5f);
            explanationPanel.SetActive(true);
            explanationText.text = "�����ϴ�";
            yield return new WaitForSeconds(3f);
            explanationText.text = "ȭ�� �ϴ� ���� �ٴ� ������ �����Դϴ�";
            yield return new WaitForSeconds(3f);
            explanationText.text = "ȭ�� �ϴ� ������ �ٴ� �Ƿ� ��ư ���Դϴ�";
            yield return new WaitForSeconds(3f);
            explanationText.text = "�Ƿ°��� 100 �̻��̸� ���� ���� �� �ֽ��ϴ�";
            yield return new WaitForSeconds(3f);
            explanationText.text = "���ϴ� �������� �Ƿ� ��ư�� ���� ���� ����������";
            yield return new WaitForSeconds(3f);
            explanationText.text = "Ʃ�丮���� ��������Դϴ�";
            yield return new WaitForSeconds(3f);
            explanationText.text = "���� �޴��� ���ư��ϴ�"; 
            yield return new WaitForSeconds(3f);
            explanationText.text = "���ϴ� ������ ���� �غ�����";
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator DelayChangeColor()
    {
        if (increaseValue == true)
        {
            if (gradientValue >= 1)
            {
                gradientValue = 1;
                increaseValue = false;
            }
            gradientValue += Time.deltaTime * 0.7f;
        }
        else if (increaseValue == false)
        {
            if (gradientValue <= 0)
            {
                gradientValue = 0;
                increaseValue = true;
            }
            gradientValue -= Time.deltaTime * 0.7f;
        }

        pressureButtonColor.color = textGradient.Evaluate(gradientValue);

        yield return null;
    }

    public void onPausedButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();
        Time.timeScale = 0;
        soundManager.soundPaused = true;

        if (controller.activeSelf == true || controllerBackPanel.activeSelf == true)
        {
            controller.SetActive(false);
            controllerBackPanel.SetActive(false);
        }

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

        if (isSetActive == true)
        {
            if (controller.activeSelf == false && controllerBackPanel.activeSelf == false)
            {
                controller.SetActive(true);
                controllerBackPanel.SetActive(true);
            }
        }

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

    public void onHomeButtonClick()
    {
        audioSource.clip = buttonSound;
        audioSource.volume = 0.3f;
        audioSource.Play();
        pausedPanel.SetActive(false);
        askGoToHomePanel.SetActive(true);
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
        askGoToHomePanel.SetActive(false);
        pausedPanel.SetActive(true);
    }
}

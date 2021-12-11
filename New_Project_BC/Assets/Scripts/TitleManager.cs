using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject gametypePanel;
    public GameObject selectStagePanel;
    public GameObject quitPanel;

    public SoundManager soundManager;

    public enum backType { start, selectStage, option };

    backType back = backType.start;

    AudioSource audioSource;
    [SerializeField] AudioClip buttonSound;

    void Awake()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(soundManager.SelectBGM("TitleScene"));
        audioSource.clip = buttonSound;
        titlePanel.SetActive(true);
        gametypePanel.SetActive(false);
        quitPanel.SetActive(false);
        selectStagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            quitPanel.SetActive(true);
    }

    public void onStartButtonClick()
    {
        audioSource.Play();
        back = backType.start;
        titlePanel.SetActive(false);
        gametypePanel.SetActive(true);
    }

    public void onTimeAttackButtonClick()
    {
        audioSource.Play();
        titlePanel.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void onArcadeButtonClick()
    {
        audioSource.Play();
        back = backType.selectStage;
        gametypePanel.SetActive(false);
        selectStagePanel.SetActive(true);
    }

    public void onTutorialButtonClick()
    {
        audioSource.Play();
        SceneManager.LoadScene(3);
    }

    public void onBackButtonClick()
    {
        audioSource.Play();

        if (back == backType.selectStage)
        {
            selectStagePanel.SetActive(false);
            gametypePanel.SetActive(true);
            back = backType.selectStage + 2;
        }
        else
        {
            gametypePanel.SetActive(false);
            titlePanel.SetActive(true);
        }
    }

    public void onQuitButtonClick()
    {
        audioSource.Play();
        titlePanel.SetActive(false);
        quitPanel.SetActive(true);
    }

    public void onYesButtonClick()
    {
        audioSource.Play();
        Application.Quit();
    }

    public void onNoButtonClick()
    {
        audioSource.Play();
        quitPanel.SetActive(false);
        titlePanel.SetActive(true);
    }

    public void onSelectStageButtonClick()
    {
        audioSource.Play();
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;

        if (clickedButton.name == "Stage1Button")
        {
            StartCoroutine(DataManager.arcadeData(1));
        }
        else if (clickedButton.name == "Stage2Button")
        {
            StartCoroutine(DataManager.arcadeData(2));
        }
        else if (clickedButton.name == "Stage3Button")
        {
            StartCoroutine(DataManager.arcadeData(3));
        }
        else if (clickedButton.name == "Stage4Button")
        {
            StartCoroutine(DataManager.arcadeData(4));
        }
        else if (clickedButton.name == "Stage5Button")
        {
            StartCoroutine(DataManager.arcadeData(5));
        }
        else if (clickedButton.name == "Stage6Button")
        {
            StartCoroutine(DataManager.arcadeData(6));
        }
        else if (clickedButton.name == "Stage7Button")
        {
            StartCoroutine(DataManager.arcadeData(7));
        }
        else if (clickedButton.name == "Stage8Button")
        {
            StartCoroutine(DataManager.arcadeData(8));
        }
        else if (clickedButton.name == "Stage9Button")
        {
            StartCoroutine(DataManager.arcadeData(9));
        }
        else if (clickedButton.name == "Stage10Button")
        {
            StartCoroutine(DataManager.arcadeData(10));
        }
        else if (clickedButton.name == "Stage11Button")
        {
            StartCoroutine(DataManager.arcadeData(11));
        }
        else if (clickedButton.name == "Stage12Button")
        {
            StartCoroutine(DataManager.arcadeData(12));
        }
        else if (clickedButton.name == "Stage13Button")
        {
            StartCoroutine(DataManager.arcadeData(13));
        }
        else if (clickedButton.name == "Stage14Button")
        {
            StartCoroutine(DataManager.arcadeData(14));
        }
        else if (clickedButton.name == "Stage15Button")
        {
            StartCoroutine(DataManager.arcadeData(15));
        }

        SceneManager.LoadScene(2);
    }
}

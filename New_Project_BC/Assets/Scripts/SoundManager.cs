using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip titleBgm;
    public AudioClip tutorialBgm;
    public AudioClip timeAttackBgm;
    public AudioClip arcadeBgm;

    public bool soundPaused = false;
    bool playOnce;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (soundPaused == true && playOnce == false)
        {
            audioSource.Pause();
            playOnce = true;
        }
        else if (soundPaused == false && playOnce == true)
        {
            audioSource.Play();
            playOnce = false;
        }
    }

    public IEnumerator SelectBGM(string name)
    {
        if (name == "TitleScene")
        {
            audioSource.clip = titleBgm;
            yield return new WaitForSeconds(0.2f);
            audioSource.Play();
        }
        else if (name == "TutorialScene")
        {
            audioSource.clip = tutorialBgm;
            yield return new WaitForSeconds(0.2f);
            audioSource.Play();
        }
        else if (name == "TimeAttackScene")
        {
            audioSource.clip = timeAttackBgm;
            yield return new WaitForSeconds(0.2f);
            audioSource.Play();
        }
        else if (name == "ArcadeScene")
        {
            audioSource.clip = arcadeBgm;
            yield return new WaitForSeconds(0.2f);
            audioSource.Play();
        }
    }
}

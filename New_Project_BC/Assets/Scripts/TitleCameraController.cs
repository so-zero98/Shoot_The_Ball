using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraController : MonoBehaviour
{
    Animator animator;

    int whatAnimation;

    bool once;
    bool primaryCalled;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelectAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectNextAnimation()
    {
        StartCoroutine(SelectAnimation());
    }

    IEnumerator SelectAnimation()
    {
        switch (whatAnimation)
        {
            case 0:
                animator.SetBool("isLeftToRight", true);
                animator.SetBool("isUpToDown", false);
                animator.SetBool("isRightToLeft", false);
                whatAnimation = 1;
                break;
            case 1:
                animator.SetBool("isLeftToRight", false);
                animator.SetBool("isUpToDown", true);
                animator.SetBool("isRightToLeft", false);
                whatAnimation = 2;
                break;
            case 2:
                animator.SetBool("isLeftToRight", false);
                animator.SetBool("isUpToDown", false);
                animator.SetBool("isRightToLeft", true);
                whatAnimation = 0;
                break;
        }

        yield return null;
    }
}
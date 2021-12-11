using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    public GameObject leftPosition;
    public GameObject RightPosition;
    public GameObject UpperPosition;
    public GameObject LowerPosition;
    public GameObject backboard;
    public GameObject rims;
    public GameObject stick;
    public GameObject shootZone;

    public bool gotoLeft;
    public bool gotoRight;
    public bool gotoUpper;
    public bool gotoLower;
    public bool disappearHoop;

    public float moveSpeed = 1f;

    bool disappearThis = false;

    // Start is called before the first frame update
    void Start()
    {
        gotoLeft = true;
        gotoLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator MoveLeftRight()
    {
        Debug.Log("»£√‚¡ﬂ");
        if (gotoLeft)
            transform.position = Vector3.MoveTowards(transform.position, leftPosition.transform.position, moveSpeed * Time.deltaTime);
        else if (gotoRight)
            transform.position = Vector3.MoveTowards(transform.position, RightPosition.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, RightPosition.transform.position) <= 0.001f)
        {
            gotoRight = false;
            gotoLeft = true;
        }
        else if (Vector3.Distance(transform.position, leftPosition.transform.position) <= 0.001f)
        {
            gotoLeft = false;
            gotoRight = true;
        }

        yield return null;
    }

    public IEnumerator MoveUpDown()
    {
        if (gotoUpper)
            transform.position = Vector3.MoveTowards(transform.position, UpperPosition.transform.position, moveSpeed * Time.deltaTime);
        else if (gotoLower)
            transform.position = Vector3.MoveTowards(transform.position, LowerPosition.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, LowerPosition.transform.position) <= 0.001f)
        {
            gotoLower = false;
            gotoUpper = true;
        }
        else if (Vector3.Distance(transform.position, UpperPosition.transform.position) <= 0.001f)
        {
            gotoUpper = false;
            gotoLower = true;
        }

        yield return null;
    }

    public IEnumerator AppearDisappear(float delayTime)
    {
        if (disappearHoop)
        {
            yield return new WaitForSeconds(2);
            backboard.SetActive(disappearThis);
            rims.SetActive(disappearThis);
            stick.SetActive(disappearThis);
            shootZone.SetActive(disappearThis);
            disappearThis = !disappearThis;
        }
        else
            yield return new WaitForSeconds(2);

        StartCoroutine(AppearDisappear(2));
    }
}

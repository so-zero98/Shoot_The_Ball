using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstZone : MonoBehaviour
{
    public bool firstInClear = false;
    public bool firstExitClear = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CurrentBall") && (other.gameObject.transform.position.y >= this.transform.position.y))
        {
            firstInClear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CurrentBall") && (other.gameObject.transform.position.y <= this.transform.position.y))
        {
            firstExitClear = true;
            other.tag = "PastBall";
            //other.gameObject.layer = 8;
        }
    }
}

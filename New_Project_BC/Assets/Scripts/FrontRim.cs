using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontRim : MonoBehaviour
{
    public bool onFrontCollision;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CurrentBall"))
            onFrontCollision = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackRim : MonoBehaviour
{
    public bool onBackCollision;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CurrentBall"))
            onBackCollision = true;
    }
}

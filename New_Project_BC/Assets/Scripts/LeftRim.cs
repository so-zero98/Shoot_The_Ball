using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRim : MonoBehaviour
{
    public bool onLeftCollision;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CurrentBall"))
            onLeftCollision = true;
    }
}

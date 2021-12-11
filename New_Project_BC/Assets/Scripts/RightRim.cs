using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRim : MonoBehaviour
{
    public bool onRightCollision;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CurrentBall"))
            onRightCollision = true;
    }
}

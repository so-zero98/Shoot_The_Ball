using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbitController : MonoBehaviour
{
    public bool setDirection = false;

    public float orbitSpeed = 100f;

    bool isIncrease = false;

    Vector3 nowOrbitPosition;

    float rotX;

    // Update is called once per frame
    void Update()
    {
        if (setDirection == false)
        {
            if (isIncrease == false)
            {
                if (rotX <= -90)
                {
                    rotX = -90;
                    this.transform.rotation = Quaternion.Euler(rotX, 0, 0);
                    nowOrbitPosition = transform.position;
                    isIncrease = true;
                }
                else
                {
                    rotX -= orbitSpeed * Time.deltaTime;
                    transform.rotation = Quaternion.Euler(rotX, 0, 0);
                    nowOrbitPosition = transform.position;
                }
            }
            else if (isIncrease == true)
            {
                if (rotX >= 0)
                {
                    rotX = 0;
                    transform.rotation = Quaternion.Euler(rotX, 0, 0);
                    nowOrbitPosition = transform.position;
                    isIncrease = false;
                }
                else
                {
                    rotX += orbitSpeed * Time.deltaTime;
                    transform.rotation = Quaternion.Euler(rotX, 0, 0);
                    nowOrbitPosition = transform.position;
                }
            }

        }
        else if (setDirection == true)
        {
            transform.rotation = Quaternion.Euler(rotX, 0, 0);
        }
    }
}

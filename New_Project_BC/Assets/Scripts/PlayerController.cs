using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public OrbitController orbit;

    public Transform orbitPos;  // 궤도
    public Transform ballPosition;  // 시작 위치

    public bool onBall = false;    // 손에 공이 없다
    bool handsOn = false;
    bool readyShooting = false;

    public float firstPitch;
    public float secondPitch;
    public float pitchDifference;

    Vector3 direction;

    public BallController ballController;

    public DataProcessing processing;

    void Awake()
    {
        orbit = GameObject.Find("Orbit").GetComponent<OrbitController>();
        processing = GameObject.Find("DataProcessing").GetComponent<DataProcessing>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator ControllerOperating()
    {
        if (onBall == false && handsOn == false)    // 손에 공이 없으면
        {
            var ball = ObjectPooling.GetObject();   // 하나 쥐켜서
            ballController = ball.GetComponent<BallController>();
            ball.tag = "CurrentBall";
            ball.gameObject.layer = 9;
            ball.transform.position = ballPosition.position;
            ball.GetComponent<Rigidbody>().useGravity = false;  // 안떨어지게 중력 0으로 설정하고
            onBall = true;  // 손에 공 있는걸로 바꿈
            handsOn = true;
        }
        else if (processing.pressure_val > 5 && handsOn == false && onBall == true)
        {
            handsOn = true;
            onBall = true;
            orbit.setDirection = true;
        }
        else if (Input.GetKeyDown(KeyCode.G) && handsOn == true && onBall == true)
        {
            firstPitch = processing.pitch_val;
            readyShooting = true;
            orbit.setDirection = true;
        }
        
        if (readyShooting == true && Input.GetKeyDown(KeyCode.G))
        {

            {
                secondPitch = processing.pitch_val;
                pitchDifference = Mathf.Abs(firstPitch - secondPitch);
            }

            if (pitchDifference >= 10f)
            {
                direction = orbitPos.position - ballController.gameObject.transform.position;
                StartCoroutine(ballController.ThrowBall(direction)); // 던지는 함수로 이동
                StartCoroutine(ballController.DestroyBall()); // 일정시간 있다가 없어지게
                handsOn = false;
                onBall = false;
                orbit.setDirection = false;
                readyShooting = false;
                pitchDifference = 0;
            }
        }



        if (Input.GetKeyDown(KeyCode.Space)/*pitchDifference > 10*/)
        {
            StartCoroutine(Delay());
            pitchDifference = 0;
        }


        yield return null;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
    }
}

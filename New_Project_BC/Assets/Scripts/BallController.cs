using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float throwingForce = 100f;

    public IEnumerator ThrowBall(Vector3 dir)
    {
        this.gameObject.layer = 8;
        //StartCoroutine(IgnoreCollider());   // 공이 겹쳐서 콜라이더 잠깐만 끄기
        GetComponent<Rigidbody>().useGravity = true;    // 던지는 동안은 중력있어야 하니까 다시 중력 설정
        GetComponent<Rigidbody>().AddForce(dir * throwingForce);   // 앞방향벡터*던지는힘
        yield return null;
    }

    public IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(6f);
        ObjectPooling.ReturnObject(this);   // 6초 있다가 삭제
    }

    public IEnumerator IgnoreCollider()
    {
        GetComponent<SphereCollider>().enabled = false; // 콜라이더 잠깐 껐다가
        yield return new WaitForSeconds(.2f);
        GetComponent<SphereCollider>().enabled = true;  // 다시 콜라이더 켜기
    }
}

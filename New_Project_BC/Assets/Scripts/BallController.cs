using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float throwingForce = 100f;

    public IEnumerator ThrowBall(Vector3 dir)
    {
        this.gameObject.layer = 8;
        //StartCoroutine(IgnoreCollider());   // ���� ���ļ� �ݶ��̴� ��� ����
        GetComponent<Rigidbody>().useGravity = true;    // ������ ������ �߷��־�� �ϴϱ� �ٽ� �߷� ����
        GetComponent<Rigidbody>().AddForce(dir * throwingForce);   // �չ��⺤��*��������
        yield return null;
    }

    public IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(6f);
        ObjectPooling.ReturnObject(this);   // 6�� �ִٰ� ����
    }

    public IEnumerator IgnoreCollider()
    {
        GetComponent<SphereCollider>().enabled = false; // �ݶ��̴� ��� ���ٰ�
        yield return new WaitForSeconds(.2f);
        GetComponent<SphereCollider>().enabled = true;  // �ٽ� �ݶ��̴� �ѱ�
    }
}

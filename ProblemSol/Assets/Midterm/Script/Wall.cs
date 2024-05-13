using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� �÷��̾ ������ Ȯ��
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            // �浹�� ��ü�� �̵��� ���
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}

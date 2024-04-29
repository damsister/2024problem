using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControll : MonoBehaviour
{
    // ī�޶� ������ ��� (�÷��̾�)
    public Transform target;
    // ȸ�� �ӵ�
    public float rotationSpeed = 5f;

    void LateUpdate()
    {
        if (target == null)
            return;

        // �÷��̾ ���� �̵�
        transform.position = target.position;

        // �÷��̾��� ȸ�� ������ ī�޶� ����
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);
    }
}

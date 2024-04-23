using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll1 : MonoBehaviour
{
    public float moveSpeed = 6f; // �̵� �ӵ�
    public float boundaryX = 10f; // X�� �̵� ����
    public float boundaryZ = 10f; // Z�� �̵� ����

    // Update is called once per frame
    void Update()
    {
        // �÷��̾��� �̵� �Է� ó��
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �̵� ���� ���
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // �̵� ���� ����
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -boundaryX, boundaryX);
        newPosition.z = Mathf.Clamp(newPosition.z, -boundaryZ, boundaryZ);

        // ���� �̵�
        transform.position = newPosition;
    }
}

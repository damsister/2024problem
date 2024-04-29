using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float boundaryX = 10f; // X�� �̵� ����
    public float boundaryZ = 10f; // Z�� �̵� ����


    void Update()
    {
        // �÷��̾� �̵� ó��
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized; // �̵� ���� ���
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // �̵� ���� ����
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -boundaryX, boundaryX);
        newPosition.z = Mathf.Clamp(newPosition.z, -boundaryZ, boundaryZ);

        // ���� �̵�
        transform.position = newPosition;

        // �÷��̾� ȸ�� ó��
        if (Input.GetKey(KeyCode.O))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.P))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }
}

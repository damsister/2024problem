using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public GameObject player;
    // �̵� �ӵ�
    public float moveSpeed = 5f;
    // ȸ�� ����
    public float rotationAngle = 90f;

    void Update()
    {
        // ȸ�� ó��
        HandleRotationInput();

        // �̵� ó��
        HandleMovementInput();
    }

    // ȸ�� �Է� ó��
    void HandleRotationInput()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            RotatePlayer(-rotationAngle);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            RotatePlayer(rotationAngle);
        }
    }

    // �÷��̾ �־��� ������ŭ ȸ����Ű�� �Լ�
    void RotatePlayer(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }

    // �̵� �Է� ó��
    void HandleMovementInput()
    {
        // ���� ȸ�� �������� �̵�
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}

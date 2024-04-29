using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public GameObject player;
    // 이동 속도
    public float moveSpeed = 5f;
    // 회전 각도
    public float rotationAngle = 90f;

    void Update()
    {
        // 회전 처리
        HandleRotationInput();

        // 이동 처리
        HandleMovementInput();
    }

    // 회전 입력 처리
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

    // 플레이어를 주어진 각도만큼 회전시키는 함수
    void RotatePlayer(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }

    // 이동 입력 처리
    void HandleMovementInput()
    {
        // 현재 회전 방향으로 이동
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}

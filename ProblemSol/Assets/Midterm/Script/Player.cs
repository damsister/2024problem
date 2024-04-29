using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float boundaryX = 10f; // X축 이동 제한
    public float boundaryZ = 10f; // Z축 이동 제한


    void Update()
    {
        // 플레이어 이동 처리
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized; // 이동 벡터 계산
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // 이동 범위 제한
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -boundaryX, boundaryX);
        newPosition.z = Mathf.Clamp(newPosition.z, -boundaryZ, boundaryZ);

        // 실제 이동
        transform.position = newPosition;

        // 플레이어 회전 처리
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

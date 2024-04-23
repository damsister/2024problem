using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll1 : MonoBehaviour
{
    public float moveSpeed = 6f; // 이동 속도
    public float boundaryX = 10f; // X축 이동 제한
    public float boundaryZ = 10f; // Z축 이동 제한

    // Update is called once per frame
    void Update()
    {
        // 플레이어의 이동 입력 처리
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 이동 벡터 계산
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // 이동 범위 제한
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -boundaryX, boundaryX);
        newPosition.z = Mathf.Clamp(newPosition.z, -boundaryZ, boundaryZ);

        // 실제 이동
        transform.position = newPosition;
    }
}

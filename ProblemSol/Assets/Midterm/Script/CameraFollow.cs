using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float chaseSpeed = 5.0f; // 플레이어를 쫓는 속도
    public float returnSpeed = 1.0f; // 원래 위치로 돌아가는 속도
    public float fieldOfViewAngle = 60.0f; // 카메라 시야 각도

    private Vector3 originalPosition; // 초기 카메라 위치
    private Quaternion originalRotation; // 초기 카메라 회전
    private bool isChasing = false; // 플레이어를 쫓고 있는지 여부
    private bool isReturning = false; // 원래 위치로 돌아가고 있는지 여부
    private float returnTimer = 0.0f; // 원래 위치로 돌아가는 타이머

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // 플레이어가 시야 범위 내에 있는지 확인
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer <= fieldOfViewAngle * 0.5f)
        {
            // 플레이어를 쫓기 시작
            isChasing = true;
            isReturning = false;
        }
        else if (isChasing)
        {
            // 플레이어를 쫓고 있었는데 범위를 벗어남
            isChasing = false;
            isReturning = true;
            returnTimer = 6f; // 두리번거리는 시간 초기화
        }

        if (isChasing)
        {
            // 플레이어를 쫓음
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
        }
        else if (isReturning)
        {
            // 원래 위치로 돌아감
            returnTimer -= Time.deltaTime;
            if (returnTimer <= 0)
            {
                isReturning = false;
                transform.position = originalPosition;
                transform.rotation = originalRotation;
            }
            else
            {
                // 두리번거리는 동작
                float t = Mathf.PingPong(Time.time * 0.5f, 1f); // 3초마다 왕복
                float angle = Mathf.Lerp(90f, 270f, t); // 90도에서 270도 사이로 두리번거리기
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 적이 감지 범위를 시각적으로 표시
        Gizmos.color = Color.yellow;
        Gizmos.DrawFrustum(transform.position, fieldOfViewAngle, fieldOfViewAngle, 0, 1);
    }
}

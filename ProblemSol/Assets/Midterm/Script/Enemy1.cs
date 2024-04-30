using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 3f; // 시야거리 (적의 캡슐 대각선 길이의 3배)
    public float viewAngle = 45f; // 시야각
    public float moveSpeed = 2f; // 이동 속도

    private Vector3 originalPosition; // 초기 위치
    private Quaternion originalRotation; // 초기 회전
    private bool isChasing = false; // 플레이어 추적 중인지 여부
    private bool isReturning = false; // 초기 위치로 복귀 중인지 여부
    private Vector3[] patrolPoints; // 순찰 지점
    private int currentPatrolIndex = 0; // 현재 순찰 지점 인덱스
    private float returnTimer = 0f; // 초기 위치 복귀 타이머
    private const float returnDuration = 6f; // 초기 위치 복귀 지속 시간

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        patrolPoints = new Vector3[]
        {
            originalPosition + Quaternion.Euler(0f, -90f, 0f) * transform.forward * detectionRange,
            originalPosition + Quaternion.Euler(0f, 180f, 0f) * transform.forward * detectionRange
        };
    }

    void Update()
    {
        // 플레이어가 시야 내에 있으면 추적
        if (CanSeePlayer())
        {
            isChasing = true;
            isReturning = false;
            returnTimer = 0f;
            ChasePlayer();
        }
        else
        {
            // 플레이어가 범위를 벗어나면 초기 위치로 복귀
            if (!isReturning)
            {
                isReturning = true;
                ReturnToOriginalPosition();
            }
            else
            {
                // 초기 위치에 도착했을 때, 다시 순찰 시작
                if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
                {
                    isReturning = false;
                    isChasing = false;
                    transform.rotation = originalRotation;
                }
            }
        }
    }

    // 플레이어가 시야 내에 있는지 확인
    bool CanSeePlayer()
    {
        if (player == null)
            return false;

        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        // 플레이어가 시야각 내에 있고 시야거리 이내에 있으면 true 반환
        if (angle < viewAngle / 2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, detectionRange))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    return true;
                }
            }
        }

        return false;
    }

    // 플레이어 추적
    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    // 초기 위치로 복귀
    void ReturnToOriginalPosition()
    {
        if (returnTimer < returnDuration)
        {
            // 6초 동안 90도 좌측으로 회전
            if (returnTimer < returnDuration / 2f)
            {
                transform.Rotate(0f, -90f * Time.deltaTime / (returnDuration / 2f), 0f);
            }
            // 다음 6초 동안 180도 우측으로 회전
            else
            {
                transform.Rotate(0f, 180f * Time.deltaTime / (returnDuration / 2f), 0f);
            }
            returnTimer += Time.deltaTime;
        }
        else
        {
            // 순찰 지점으로 이동
            Vector3 targetPosition = patrolPoints[currentPatrolIndex];
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;

            // 순찰 지점에 도착하면 다음 순찰 지점으로 이동
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            }
        }
    }
}

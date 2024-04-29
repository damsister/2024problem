using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minX; // X 좌표 최소값
    public float maxX; // X 좌표 최대값
    public float minZ; // Z 좌표 최소값
    public float maxZ; // Z 좌표 최대값

    private Vector3 targetPosition;

    private void Start()
    {
        // 시작할 때 무작위한 목표 위치 설정
        SetRandomTargetPosition();
    }

    private void Update()
    {
        // 현재 위치에서 목표 위치로 향하는 방향 계산
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // 이동 속도를 곱하여 이동량 계산
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

        // 이동량을 현재 위치에 더하여 이동
        transform.Translate(movement);

        // 만약 적이 목표 위치에 도달했다면 새로운 목표 위치 설정
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }

        // 적의 위치를 특정 범위 내로 제한
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }

    // 무작위한 목표 위치 설정
    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    private void OnDrawGizmos()
    {
        // 적의 이동 가능한 영역을 노란색으로 표시
        Gizmos.color = new Color(1f, 1f, 0f, 0.5f); // 노란색 + 반투명 설정
        Gizmos.DrawCube(new Vector3((minX + maxX) / 2, transform.position.y, (minZ + maxZ) / 2), new Vector3(maxX - minX, 0.1f, maxZ - minZ));
    }
}

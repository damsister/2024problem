using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillainControll : MonoBehaviour
{
    public float moveSpeed = 5f; // 악당의 이동 속도
    public float detectionRange = 10f; // 플레이어를 감지하는 범위

    private Transform player; // 플레이어의 Transform 컴포넌트

    void Start()
    {
        // 플레이어를 찾아서 Transform을 가져옴
        player = GameObject.FindGameObjectWithTag("player").transform;
    }

    void Update()
    {
        // 플레이어와의 거리를 계산
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 플레이어가 감지 범위 내에 있는지 확인
        if (distanceToPlayer <= detectionRange)
        {
            // 플레이어를 향해 이동
            transform.LookAt(player); // 플레이어를 바라봄
            transform.position += transform.forward * moveSpeed * Time.deltaTime; // 앞으로 이동
        }
    }
}

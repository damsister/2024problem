using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 3; // 시작 목숨
    private int currentHealth; // 현재 목숨

    void Start()
    {
        currentHealth = startingHealth; // 현재 목숨을 시작 목숨으로 초기화
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villain")) // 다른 오브젝트가 "Enemy" 태그를 가지고 있는지 확인
        {
            TakeDamage(); // 충돌 시 플레이어의 목숨 감소 함수 호출
        }
    }

    void TakeDamage()
    {
        currentHealth--; // 플레이어의 목숨 감소
        Debug.Log("Player health: " + currentHealth); // 현재 목숨을 로그에 출력

        if (currentHealth <= 0)
        {
            Die(); // 만약 목숨이 0 이하이면 Die 함수 호출
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // 여기에 플레이어 사망에 따른 처리를 추가할 수 있습니다. 예를 들어 게임 오버 화면 표시 등.
    }
}

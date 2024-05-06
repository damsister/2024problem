using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Floor : MonoBehaviour
{
    public Transform startLocation; // 시작 위치
    public Transform escapeLocation; // 탈출 위치

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 시작 위치에 진입했는지 확인
        if (other.CompareTag("Player") && other.transform.position == startLocation.position)
        {
            // 플레이어가 시작 위치에 도달했을 때 처리할 내용 작성
            Debug.Log("Player reached the starting point.");
        }

        // 플레이어가 탈출 위치에 도달했는지 확인
        if (other.CompareTag("Player") && other.transform.position == escapeLocation.position)
        {
            // 탈출 위치에 도달했을 때 게임 클리어 처리
            Debug.Log("Player reached the escape point. Game cleared!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬 다시 로드 (게임 리셋)
        }
    }
}

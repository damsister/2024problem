using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform player; // 플레이어의 위치를 나타내는 Transform
    public float moveSpeed = 3f; // 적의 이동 속도
    public float moveRangeWidth = 5f; // 적의 이동 범위 가로 길이
    public float moveRangeHeight = 5f; // 적의 이동 범위 세로 길이

    private Vector3 startPosition; // 적의 시작 위치
    private Vector3 targetPosition; // 적의 목표 위치
    private bool movingForward = true; // 적이 앞으로 이동 중인지 여부

    public float rotationDuration = 3f; // 회전에 걸리는 시간 (초)
    private float currentRotationTime = 0f; // 현재 회전 시간

    private Camera Ecamera; // 적이 사용하는 카메라
    bool isChasing = false; // 적이 플레이어를 추적 중인지 여부
    bool isRotating90 = false; // 적이 90도 회전 중인지 여부
    bool isRotating180 = false; // 적이 -180도 회전 중인지 여부

    public GameObject restartbtn;
    public TextMeshProUGUI gameovertext;
    bool gameover = false;

    private void Start()
    {
        startPosition = transform.position; // 적의 시작 위치 설정
        Ecamera = GetComponent<Camera>(); // 적의 카메라 가져오기
        ChooseRandomTargetPosition(); // 첫 번째 목표 위치 선택

        gameovertext.gameObject.SetActive(false);
        restartbtn.SetActive(false);
    }

    private void Update()
    {
        if (IsTargetVisible(Ecamera, player) == false) // 플레이어가 적의 시야에 없을 경우
        {
            EnemyRotate(); // 적이 회전 동작 수행
            if (isRotating90 == false && isRotating180 == false)
            {
                EnemyMove(); // 적이 이동 동작 수행
            }
        }
        else // 플레이어가 적의 시야에 있을 경우
        {
            playerchase(); // 적이 플레이어를 추적
            isRotating90 = true; // 90도 회전 상태 설정
            currentRotationTime = 0f; // 회전 시간 초기화
        }
    }

    // 시작 위치를 중심으로 랜덤한 목표 위치를 선택
    private void ChooseRandomTargetPosition()
    {
        float randomX = Random.Range(startPosition.x - moveRangeWidth / 2f, startPosition.x + moveRangeWidth / 2f);
        float randomZ = Random.Range(startPosition.z - moveRangeHeight / 2f, startPosition.z + moveRangeHeight / 2f);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    // 적의 회전 동작을 수행
    private void EnemyRotate()
    {
        if (isRotating90 || isRotating180)
        {
            currentRotationTime += Time.deltaTime;

            if (isRotating90)
            {
                // 90도 회전 중일 때
                float angle = Mathf.Lerp(0, 90, currentRotationTime / rotationDuration); // 회전 각도 보간
                transform.rotation = Quaternion.Euler(0, angle, 0); // 회전 각도 설정

                // 회전 완료 후 -180도 회전으로 상태 전환
                if (currentRotationTime >= rotationDuration)
                {
                    isRotating90 = false;
                    currentRotationTime = 0f;
                    isRotating180 = true;
                }
            }
            else if (isRotating180)
            {
                // -180도 회전 중일 때
                float angle = Mathf.Lerp(90, -90, currentRotationTime / rotationDuration); // 회전 각도 보간
                transform.rotation = Quaternion.Euler(0, angle, 0); // 회전 각도 설정

                // 회전 완료 후 회전 상태 해제
                if (currentRotationTime >= rotationDuration)
                {
                    isRotating180 = false;
                    currentRotationTime = 0f;
                }
            }
        }
    }

    // 적의 이동 동작을 수행
    private void EnemyMove()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0); // 적의 회전 초기화
        Vector3 movement = (targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime; // 이동 벡터 계산
        transform.Translate(movement); // 목표 위치로 이동

        // 목표 위치에 도달하면 새로운 목표 위치 선택
        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            movingForward = !movingForward;
            ChooseRandomTargetPosition();
        }
    }

    // 플레이어가 적의 카메라 시야에 있는지 확인
    public bool IsTargetVisible(Camera _camera, Transform _transform)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        var point = _transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }

    // 플레이어를 추적
    private void playerchase()
    {
        //isChasing = true;
        Vector3 playerDirection = (player.position - transform.position).normalized; // 플레이어 방향 계산
        Quaternion targetRotation = Quaternion.LookRotation(playerDirection); // 목표 회전 계산
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 30f * Time.deltaTime); // 플레이어를 향해 회전

        Vector3 movement = playerDirection * 5f * Time.deltaTime; // 플레이어를 향해 이동
        transform.Translate(movement);
    }

    // 에디터 상에서 이동 범위를 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; // 노란색으로 이동 범위 표시
        Vector3 topLeft = startPosition + new Vector3(-moveRangeWidth / 2f, 0f, moveRangeHeight / 2f);
        Vector3 topRight = startPosition + new Vector3(moveRangeWidth / 2f, 0f, moveRangeHeight / 2f);
        Vector3 bottomLeft = startPosition + new Vector3(-moveRangeWidth / 2f, 0f, -moveRangeHeight / 2f);
        Vector3 bottomRight = startPosition + new Vector3(moveRangeWidth / 2f, 0f, -moveRangeHeight / 2f);
        Gizmos.DrawLine(topLeft, topRight); // 이동 범위의 상단 선
        Gizmos.DrawLine(topRight, bottomRight); // 이동 범위의 오른쪽 선
        Gizmos.DrawLine(bottomRight, bottomLeft); // 이동 범위의 하단 선
        Gizmos.DrawLine(bottomLeft, topLeft); // 이동 범위의 왼쪽 선
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("player"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameover = true;
        gameovertext.gameObject.SetActive(true);
        gameovertext.text = "GameOver";
        restartbtn.SetActive(true);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(0);
    }
}

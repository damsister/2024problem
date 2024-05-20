using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerControll : MonoBehaviour
{
    public GameObject player;
    public Transform start;
    public TextMeshProUGUI cleartext;
    public GameObject restartbtn;

    // 이동 속도
    public float moveSpeed = 5f;
    // 회전 각도
    public float rotationAngle = 90f;

    public float rotationSpeed = 100f;
    public float boundaryX = 10f; // X축 이동 제한
    public float boundaryZ = 10f; // Z축 이동 제한

    bool end = false;


    void Start()
    {
        // 시작 지점으로 플레이어 이동
        transform.position = new Vector3(start.position.x, 1f, start.position.z);
        cleartext.gameObject.SetActive(false);
        restartbtn.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!end)
        {
            HandleRotationInput(); //회전처리
            HandleMovementInput(); //이동처리
        }
    }

    // 회전 입력 처리
    void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.O))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.P))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }

    // 이동 입력 처리
    void HandleMovementInput()
    {
        // 현재 회전 방향으로 이동
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("End"))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        end = true;
        cleartext.gameObject.SetActive(true);
        cleartext.text = "Clear!!!";
        restartbtn.gameObject.SetActive(true);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(0);
    }
}

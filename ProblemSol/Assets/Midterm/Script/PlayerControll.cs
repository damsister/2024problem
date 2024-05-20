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

    // �̵� �ӵ�
    public float moveSpeed = 5f;
    // ȸ�� ����
    public float rotationAngle = 90f;

    public float rotationSpeed = 100f;
    public float boundaryX = 10f; // X�� �̵� ����
    public float boundaryZ = 10f; // Z�� �̵� ����

    bool end = false;


    void Start()
    {
        // ���� �������� �÷��̾� �̵�
        transform.position = new Vector3(start.position.x, 1f, start.position.z);
        cleartext.gameObject.SetActive(false);
        restartbtn.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!end)
        {
            HandleRotationInput(); //ȸ��ó��
            HandleMovementInput(); //�̵�ó��
        }
    }

    // ȸ�� �Է� ó��
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

    // �̵� �Է� ó��
    void HandleMovementInput()
    {
        // ���� ȸ�� �������� �̵�
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

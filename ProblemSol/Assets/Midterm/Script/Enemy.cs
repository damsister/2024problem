using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform player; // �÷��̾��� ��ġ�� ��Ÿ���� Transform
    public float moveSpeed = 3f; // ���� �̵� �ӵ�
    public float moveRangeWidth = 5f; // ���� �̵� ���� ���� ����
    public float moveRangeHeight = 5f; // ���� �̵� ���� ���� ����

    private Vector3 startPosition; // ���� ���� ��ġ
    private Vector3 targetPosition; // ���� ��ǥ ��ġ
    private bool movingForward = true; // ���� ������ �̵� ������ ����

    public float rotationDuration = 3f; // ȸ���� �ɸ��� �ð� (��)
    private float currentRotationTime = 0f; // ���� ȸ�� �ð�

    private Camera Ecamera; // ���� ����ϴ� ī�޶�
    bool isChasing = false; // ���� �÷��̾ ���� ������ ����
    bool isRotating90 = false; // ���� 90�� ȸ�� ������ ����
    bool isRotating180 = false; // ���� -180�� ȸ�� ������ ����

    public GameObject restartbtn;
    public TextMeshProUGUI gameovertext;
    bool gameover = false;

    private void Start()
    {
        startPosition = transform.position; // ���� ���� ��ġ ����
        Ecamera = GetComponent<Camera>(); // ���� ī�޶� ��������
        ChooseRandomTargetPosition(); // ù ��° ��ǥ ��ġ ����

        gameovertext.gameObject.SetActive(false);
        restartbtn.SetActive(false);
    }

    private void Update()
    {
        if (IsTargetVisible(Ecamera, player) == false) // �÷��̾ ���� �þ߿� ���� ���
        {
            EnemyRotate(); // ���� ȸ�� ���� ����
            if (isRotating90 == false && isRotating180 == false)
            {
                EnemyMove(); // ���� �̵� ���� ����
            }
        }
        else // �÷��̾ ���� �þ߿� ���� ���
        {
            playerchase(); // ���� �÷��̾ ����
            isRotating90 = true; // 90�� ȸ�� ���� ����
            currentRotationTime = 0f; // ȸ�� �ð� �ʱ�ȭ
        }
    }

    // ���� ��ġ�� �߽����� ������ ��ǥ ��ġ�� ����
    private void ChooseRandomTargetPosition()
    {
        float randomX = Random.Range(startPosition.x - moveRangeWidth / 2f, startPosition.x + moveRangeWidth / 2f);
        float randomZ = Random.Range(startPosition.z - moveRangeHeight / 2f, startPosition.z + moveRangeHeight / 2f);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    // ���� ȸ�� ������ ����
    private void EnemyRotate()
    {
        if (isRotating90 || isRotating180)
        {
            currentRotationTime += Time.deltaTime;

            if (isRotating90)
            {
                // 90�� ȸ�� ���� ��
                float angle = Mathf.Lerp(0, 90, currentRotationTime / rotationDuration); // ȸ�� ���� ����
                transform.rotation = Quaternion.Euler(0, angle, 0); // ȸ�� ���� ����

                // ȸ�� �Ϸ� �� -180�� ȸ������ ���� ��ȯ
                if (currentRotationTime >= rotationDuration)
                {
                    isRotating90 = false;
                    currentRotationTime = 0f;
                    isRotating180 = true;
                }
            }
            else if (isRotating180)
            {
                // -180�� ȸ�� ���� ��
                float angle = Mathf.Lerp(90, -90, currentRotationTime / rotationDuration); // ȸ�� ���� ����
                transform.rotation = Quaternion.Euler(0, angle, 0); // ȸ�� ���� ����

                // ȸ�� �Ϸ� �� ȸ�� ���� ����
                if (currentRotationTime >= rotationDuration)
                {
                    isRotating180 = false;
                    currentRotationTime = 0f;
                }
            }
        }
    }

    // ���� �̵� ������ ����
    private void EnemyMove()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0); // ���� ȸ�� �ʱ�ȭ
        Vector3 movement = (targetPosition - transform.position).normalized * moveSpeed * Time.deltaTime; // �̵� ���� ���
        transform.Translate(movement); // ��ǥ ��ġ�� �̵�

        // ��ǥ ��ġ�� �����ϸ� ���ο� ��ǥ ��ġ ����
        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            movingForward = !movingForward;
            ChooseRandomTargetPosition();
        }
    }

    // �÷��̾ ���� ī�޶� �þ߿� �ִ��� Ȯ��
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

    // �÷��̾ ����
    private void playerchase()
    {
        //isChasing = true;
        Vector3 playerDirection = (player.position - transform.position).normalized; // �÷��̾� ���� ���
        Quaternion targetRotation = Quaternion.LookRotation(playerDirection); // ��ǥ ȸ�� ���
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 30f * Time.deltaTime); // �÷��̾ ���� ȸ��

        Vector3 movement = playerDirection * 5f * Time.deltaTime; // �÷��̾ ���� �̵�
        transform.Translate(movement);
    }

    // ������ �󿡼� �̵� ������ ǥ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; // ��������� �̵� ���� ǥ��
        Vector3 topLeft = startPosition + new Vector3(-moveRangeWidth / 2f, 0f, moveRangeHeight / 2f);
        Vector3 topRight = startPosition + new Vector3(moveRangeWidth / 2f, 0f, moveRangeHeight / 2f);
        Vector3 bottomLeft = startPosition + new Vector3(-moveRangeWidth / 2f, 0f, -moveRangeHeight / 2f);
        Vector3 bottomRight = startPosition + new Vector3(moveRangeWidth / 2f, 0f, -moveRangeHeight / 2f);
        Gizmos.DrawLine(topLeft, topRight); // �̵� ������ ��� ��
        Gizmos.DrawLine(topRight, bottomRight); // �̵� ������ ������ ��
        Gizmos.DrawLine(bottomRight, bottomLeft); // �̵� ������ �ϴ� ��
        Gizmos.DrawLine(bottomLeft, topLeft); // �̵� ������ ���� ��
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

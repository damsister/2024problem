using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float chaseSpeed = 5.0f; // �÷��̾ �Ѵ� �ӵ�
    public float returnSpeed = 1.0f; // ���� ��ġ�� ���ư��� �ӵ�
    public float fieldOfViewAngle = 60.0f; // ī�޶� �þ� ����

    private Vector3 originalPosition; // �ʱ� ī�޶� ��ġ
    private Quaternion originalRotation; // �ʱ� ī�޶� ȸ��
    private bool isChasing = false; // �÷��̾ �Ѱ� �ִ��� ����
    private bool isReturning = false; // ���� ��ġ�� ���ư��� �ִ��� ����
    private float returnTimer = 0.0f; // ���� ��ġ�� ���ư��� Ÿ�̸�

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // �÷��̾ �þ� ���� ���� �ִ��� Ȯ��
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer <= fieldOfViewAngle * 0.5f)
        {
            // �÷��̾ �ѱ� ����
            isChasing = true;
            isReturning = false;
        }
        else if (isChasing)
        {
            // �÷��̾ �Ѱ� �־��µ� ������ ���
            isChasing = false;
            isReturning = true;
            returnTimer = 6f; // �θ����Ÿ��� �ð� �ʱ�ȭ
        }

        if (isChasing)
        {
            // �÷��̾ ����
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
        }
        else if (isReturning)
        {
            // ���� ��ġ�� ���ư�
            returnTimer -= Time.deltaTime;
            if (returnTimer <= 0)
            {
                isReturning = false;
                transform.position = originalPosition;
                transform.rotation = originalRotation;
            }
            else
            {
                // �θ����Ÿ��� ����
                float t = Mathf.PingPong(Time.time * 0.5f, 1f); // 3�ʸ��� �պ�
                float angle = Mathf.Lerp(90f, 270f, t); // 90������ 270�� ���̷� �θ����Ÿ���
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ���� ���� ������ �ð������� ǥ��
        Gizmos.color = Color.yellow;
        Gizmos.DrawFrustum(transform.position, fieldOfViewAngle, fieldOfViewAngle, 0, 1);
    }
}

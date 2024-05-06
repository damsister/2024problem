using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{//�þ߰Ÿ��� 3�� ��� = 6.708204
    public Transform player; // �÷��̾��� Transform
    public float chaseSpeed = 5.0f; // �÷��̾ �Ѵ� �ӵ�
    public float returnSpeed = 1.0f; // ���� ��ġ�� ���ư��� �ӵ�
    public float fieldOfViewMultiplier = 3.0f; // �þ� �Ÿ��� ���

    private Vector3 originalPosition; // �ʱ� ī�޶� ��ġ
    private Quaternion originalRotation; // �ʱ� ī�޶� ȸ��
    private bool isChasing = false; // �÷��̾ �Ѱ� �ִ��� ����
    private bool isReturning = false; // ���� ��ġ�� ���ư��� �ִ��� ����
    private float returnTimer = 0.0f; // ���� ��ġ�� ���ư��� Ÿ�̸�

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        Camera.main.fieldOfView *= fieldOfViewMultiplier; // �þ� �Ÿ� ����
    }

    void Update()
    {
        // �÷��̾ �þ� ���� ���� �ִ��� Ȯ��
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        if (distanceToPlayer <= Camera.main.farClipPlane * fieldOfViewMultiplier)
        {
            Vector3 forward = transform.forward;
            forward.y = 0; // ���� �������θ� ���
            float angleToPlayer = Vector3.Angle(forward, directionToPlayer);

            if (angleToPlayer <= 45.0f * 0.5f)
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
                returnTimer = 6.0f; // �θ����Ÿ��� �ð� �ʱ�ȭ
            }
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
                float t = Mathf.PingPong(Time.time * 0.5f, 1.0f); // 3�ʸ��� �պ�
                float angle = Mathf.Lerp(-90.0f, 90.0f, t); // -90������ 90�� ���̷� �θ����Ÿ���
                transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            }
        }
    }
}

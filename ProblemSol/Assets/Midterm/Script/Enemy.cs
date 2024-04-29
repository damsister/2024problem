using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minX; // X ��ǥ �ּҰ�
    public float maxX; // X ��ǥ �ִ밪
    public float minZ; // Z ��ǥ �ּҰ�
    public float maxZ; // Z ��ǥ �ִ밪

    private Vector3 targetPosition;

    private void Start()
    {
        // ������ �� �������� ��ǥ ��ġ ����
        SetRandomTargetPosition();
    }

    private void Update()
    {
        // ���� ��ġ���� ��ǥ ��ġ�� ���ϴ� ���� ���
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // �̵� �ӵ��� ���Ͽ� �̵��� ���
        Vector3 movement = moveDirection * moveSpeed * Time.deltaTime;

        // �̵����� ���� ��ġ�� ���Ͽ� �̵�
        transform.Translate(movement);

        // ���� ���� ��ǥ ��ġ�� �����ߴٸ� ���ο� ��ǥ ��ġ ����
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }

        // ���� ��ġ�� Ư�� ���� ���� ����
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }

    // �������� ��ǥ ��ġ ����
    private void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    private void OnDrawGizmos()
    {
        // ���� �̵� ������ ������ ��������� ǥ��
        Gizmos.color = new Color(1f, 1f, 0f, 0.5f); // ����� + ������ ����
        Gizmos.DrawCube(new Vector3((minX + maxX) / 2, transform.position.y, (minZ + maxZ) / 2), new Vector3(maxX - minX, 0.1f, maxZ - minZ));
    }
}

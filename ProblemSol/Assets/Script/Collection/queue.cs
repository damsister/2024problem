using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queue : MonoBehaviour
{
    private Queue<GameObject> bulletQueue = new Queue<GameObject>(); // LinkedList�� �ƴ� Queue�� ���
    public int ballCount = 1;
    private bool is_start = false;
    public GameObject ballPrefab;
    public Transform target;
    private int shotCount = 10; // �߻� Ƚ��
    private bool isShooting = false; // �߻� ������ ����

    void Start()
    {
        for (int i = 0; i < 10; i++) // �ʱ⿡ 10���� �Ѿ��� �����Ͽ� Queue�� �߰�
        {
            Enqueue(CreateBullet());
        }
        transform.position = new Vector3(-11, 0, 0);
    }

    void Update()
    {
        if (is_start && bulletQueue.Count > 0 && shotCount > 0)
        {
            if (!isShooting)
            {
                GameObject bullet = Dequeue(); // Dequeue �Լ��� ����Ͽ� Queue���� �Ѿ��� ������
                bullet.transform.position = transform.position;
                StartCoroutine(MoveBullet(bullet));
                shotCount--; // �߻� Ƚ�� ����
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.is_start = true;
        }
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(ballPrefab);
        bullet.SetActive(false);
        return bullet;
    }

    private void Enqueue(GameObject bullet)
    {
        bulletQueue.Enqueue(bullet); // Queue�� �Ѿ��� �߰��ϴ� Enqueue �Լ�
    }

    private GameObject Dequeue()
    {
        return bulletQueue.Dequeue(); // Queue���� �Ѿ��� ������ Dequeue �Լ�
    }

    IEnumerator MoveBullet(GameObject bullet)
    {
        isShooting = true;
        bullet.SetActive(true);

        while (Vector3.Distance(bullet.transform.position, target.position) > 0.1f)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.position, Time.deltaTime * 8.0f);
            yield return null;
        }

        bullet.SetActive(false);
        Enqueue(bullet); // �Ѿ��� ������ �̵��� �� �ٽ� Queue�� �߰�
        isShooting = false;
    }
}
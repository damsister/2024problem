using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Node Ŭ���� ����
public class Node<T>
{
    public T data; // ����� ������
    public Node<T> next; // ���� ��带 ����Ű�� ������

    // ������
    public Node(T data)
    {
        this.data = data;
        this.next = null;
    }
}

// Queue Ŭ���� ����
public class Queue<T>
{
    private Node<T> front; // ť�� �� �� ��带 ����Ű�� ������
    private Node<T> rear; // ť�� �� �� ��带 ����Ű�� ������
    private int count; // ť�� ��� ������ �����ϴ� ����

    // ������
    public Queue()
    {
        front = null;
        rear = null;
        count = 0;
    }

    // ť�� ������ �߰� (Enqueue)
    public void Enqueue(T data)
    {
        Node<T> newNode = new Node<T>(data); // ���ο� ��� ����
        if (rear == null)
        {
            front = newNode; // ť�� ������� ��� ���ο� ��带 �� �տ� �߰�
            rear = newNode;
        }
        else
        {
            rear.next = newNode; // ������ ����� ���� ���� ���ο� ��� �߰�
            rear = newNode; // ���ο� ��带 rear�� ����
        }
        count++; // ť�� ��� ���� ����
    }

    // ť���� ������ ���� �� ��ȯ (Dequeue)
    public T Dequeue()
    {
        if (front == null)
        {
            Debug.LogError("Queue is empty"); // ť�� ����ִ� ��� ���� �޽��� ���
            return default(T);
        }

        T data = front.data; // ������ ����� ������ ����
        front = front.next; // front�� ���� ���� �̵�
        if (front == null)
        {
            rear = null; // ť�� ������ ��Ҹ� ������ ��� rear�� null�� ����
        }
        count--; // ť�� ��� ���� ����
        return data; // ������ ������ ��ȯ
    }

    // ť�� ���� ��� ���� ��ȯ
    public int Count
    {
        get { return count; }
    }
}

public class queue : MonoBehaviour
{
    private Queue<GameObject> bulletQueue = new Queue<GameObject>(); // �Ѿ��� �����ϴ� ť
    public int ballCount = 1;
    private bool is_start = false;
    public GameObject ballPrefab; // �Ѿ� ������
    public Transform target; // ��ǥ ����
    private int shotCount = 10; // �߻� Ƚ��
    private bool isShooting = false; // �߻� ������ ����

    void Start()
    {
        for (int i = 0; i < 10; i++) // �ʱ⿡ 10���� �Ѿ��� �����Ͽ� ť�� �߰�
        {
            Enqueue(CreateBullet());
        }
        transform.position = new Vector3(-11, 0, 0); // ���� ��ġ ����
    }

    void Update()
    {
        if (is_start && bulletQueue.Count > 0 && shotCount > 0)
        {
            if (!isShooting)
            {
                GameObject bullet = Dequeue(); // ť���� �Ѿ� ��������
                bullet.transform.position = transform.position; // �Ѿ� ��ġ ����
                StartCoroutine(MoveBullet(bullet)); // �Ѿ� �̵� �ڷ�ƾ ����
                shotCount--; // �߻� Ƚ�� ����
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.is_start = true; // ���콺 Ŭ�� �� �߻� ����
        }
    }

    // �Ѿ� ����
    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(ballPrefab); // �������� �̿��Ͽ� �Ѿ� ����
        bullet.SetActive(false); // ��Ȱ��ȭ ���·� ����
        return bullet;
    }

    // ť�� �Ѿ� �߰�
    private void Enqueue(GameObject bullet)
    {
        bulletQueue.Enqueue(bullet);
    }

    // ť���� �Ѿ� ���� �� ��ȯ
    private GameObject Dequeue()
    {
        return bulletQueue.Dequeue();
    }

    // �Ѿ� �̵� �ڷ�ƾ
    IEnumerator MoveBullet(GameObject bullet)
    {
        isShooting = true;
        bullet.SetActive(true); // �Ѿ� Ȱ��ȭ

        while (Vector3.Distance(bullet.transform.position, target.position) > 0.1f)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.position, Time.deltaTime * 8.0f); // �Ѿ��� ��ǥ ����(������ ����)���� �̵�
            yield return null;
        }

        bullet.SetActive(false); // ��ǥ������ ������ �Ѿ� ��Ȱ��ȭ
        Enqueue(bullet); // �Ѿ��� �ٽ� ť�� �߰�
        isShooting = false;
    }
}
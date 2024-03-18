using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Node 클래스 정의
public class Node<T>
{
    public T data; // 노드의 데이터
    public Node<T> next; // 다음 노드를 가리키는 포인터

    // 생성자
    public Node(T data)
    {
        this.data = data;
        this.next = null;
    }
}

// Queue 클래스 정의
public class Queue<T>
{
    private Node<T> front; // 큐의 맨 앞 노드를 가리키는 포인터
    private Node<T> rear; // 큐의 맨 뒤 노드를 가리키는 포인터
    private int count; // 큐의 요소 개수를 저장하는 변수

    // 생성자
    public Queue()
    {
        front = null;
        rear = null;
        count = 0;
    }

    // 큐에 데이터 추가 (Enqueue)
    public void Enqueue(T data)
    {
        Node<T> newNode = new Node<T>(data); // 새로운 노드 생성
        if (rear == null)
        {
            front = newNode; // 큐가 비어있을 경우 새로운 노드를 맨 앞에 추가
            rear = newNode;
        }
        else
        {
            rear.next = newNode; // 마지막 노드의 다음 노드로 새로운 노드 추가
            rear = newNode; // 새로운 노드를 rear로 설정
        }
        count++; // 큐의 요소 개수 증가
    }

    // 큐에서 데이터 제거 후 반환 (Dequeue)
    public T Dequeue()
    {
        if (front == null)
        {
            Debug.LogError("Queue is empty"); // 큐가 비어있는 경우 에러 메시지 출력
            return default(T);
        }

        T data = front.data; // 제거할 노드의 데이터 저장
        front = front.next; // front를 다음 노드로 이동
        if (front == null)
        {
            rear = null; // 큐의 마지막 요소를 제거한 경우 rear를 null로 설정
        }
        count--; // 큐의 요소 개수 감소
        return data; // 제거한 데이터 반환
    }

    // 큐의 현재 요소 개수 반환
    public int Count
    {
        get { return count; }
    }
}

public class queue : MonoBehaviour
{
    private Queue<GameObject> bulletQueue = new Queue<GameObject>(); // 총알을 저장하는 큐
    public int ballCount = 1;
    private bool is_start = false;
    public GameObject ballPrefab; // 총알 프리팹
    public Transform target; // 목표 지점
    private int shotCount = 10; // 발사 횟수
    private bool isShooting = false; // 발사 중인지 여부

    void Start()
    {
        for (int i = 0; i < 10; i++) // 초기에 10개의 총알을 생성하여 큐에 추가
        {
            Enqueue(CreateBullet());
        }
        transform.position = new Vector3(-11, 0, 0); // 시작 위치 설정
    }

    void Update()
    {
        if (is_start && bulletQueue.Count > 0 && shotCount > 0)
        {
            if (!isShooting)
            {
                GameObject bullet = Dequeue(); // 큐에서 총알 가져오기
                bullet.transform.position = transform.position; // 총알 위치 설정
                StartCoroutine(MoveBullet(bullet)); // 총알 이동 코루틴 시작
                shotCount--; // 발사 횟수 감소
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.is_start = true; // 마우스 클릭 시 발사 시작
        }
    }

    // 총알 생성
    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(ballPrefab); // 프리팹을 이용하여 총알 생성
        bullet.SetActive(false); // 비활성화 상태로 설정
        return bullet;
    }

    // 큐에 총알 추가
    private void Enqueue(GameObject bullet)
    {
        bulletQueue.Enqueue(bullet);
    }

    // 큐에서 총알 제거 후 반환
    private GameObject Dequeue()
    {
        return bulletQueue.Dequeue();
    }

    // 총알 이동 코루틴
    IEnumerator MoveBullet(GameObject bullet)
    {
        isShooting = true;
        bullet.SetActive(true); // 총알 활성화

        while (Vector3.Distance(bullet.transform.position, target.position) > 0.1f)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.position, Time.deltaTime * 8.0f); // 총알을 목표 지점(빨간색 상자)으로 이동
            yield return null;
        }

        bullet.SetActive(false); // 목표지점에 닿으면 총알 비활성화
        Enqueue(bullet); // 총알을 다시 큐에 추가
        isShooting = false;
    }
}
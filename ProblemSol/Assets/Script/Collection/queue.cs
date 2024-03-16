using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queue : MonoBehaviour
{
    private Queue<GameObject> bulletQueue = new Queue<GameObject>(); // LinkedList가 아닌 Queue를 사용
    public int ballCount = 1;
    private bool is_start = false;
    public GameObject ballPrefab;
    public Transform target;
    private int shotCount = 10; // 발사 횟수
    private bool isShooting = false; // 발사 중인지 여부

    void Start()
    {
        for (int i = 0; i < 10; i++) // 초기에 10개의 총알을 생성하여 Queue에 추가
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
                GameObject bullet = Dequeue(); // Dequeue 함수를 사용하여 Queue에서 총알을 가져옴
                bullet.transform.position = transform.position;
                StartCoroutine(MoveBullet(bullet));
                shotCount--; // 발사 횟수 감소
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
        bulletQueue.Enqueue(bullet); // Queue에 총알을 추가하는 Enqueue 함수
    }

    private GameObject Dequeue()
    {
        return bulletQueue.Dequeue(); // Queue에서 총알을 꺼내는 Dequeue 함수
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
        Enqueue(bullet); // 총알이 끝까지 이동한 후 다시 Queue에 추가
        isShooting = false;
    }
}
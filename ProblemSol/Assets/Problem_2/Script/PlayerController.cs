using DataStrucuture4;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Bullet;
    public Queue<GameObject> queue;

    // Start is called before the first frame update
    void Start()
    {
        Bullet.SetActive(false);
        queue = new Queue<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(Bullet);
            obj.GetComponent<BulletMove>().Init(transform.position, queue); // 여기서 총알의 초기 위치를 플레이어의 위치로 설정
            queue.Enqueue(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        GameObject Bul = queue.Dequeue();
        Bul.GetComponent<BulletMove>().Init(transform.position, queue);
        Bul.SetActive(true);
    }
}

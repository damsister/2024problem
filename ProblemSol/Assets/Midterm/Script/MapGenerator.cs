using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // 벽 프리팹
    public GameObject doorPrefab; // 문 프리팹
    public GameObject keyPrefab; // 열쇠 프리팹
    public TextAsset mapCSV; // 맵 정보를 담은 CSV 파일
    public Vector2 cellSize; // 셀의 크기
    public Transform planeTransform; // Plane의 Transform

    private int[,] mapData; // 맵 정보를 저장할 배열
    private Vector2Int mapSize = new Vector2Int(10, 10); // 고정된 맵의 가로세로 크기

    private bool hasKey = false; // 열쇠를 가졌는지 여부

    void Start()
    {
        // CSV 파일을 읽어와 맵 데이터를 초기화
        LoadMapFromCSV();

        // 맵 생성
        GenerateMap();
    }

    void LoadMapFromCSV()
    {
        string[] lines = mapCSV.text.Split('\n');

        // 맵 데이터 배열 생성
        mapData = new int[mapSize.y, mapSize.x];

        // CSV 파일에서 맵 데이터를 읽어와 배열에 저장
        for (int i = 0; i < mapSize.y; i++)
        {
            string[] row = lines[i].Trim().Split(',');
            for (int j = 0; j < mapSize.x; j++)
            {
                mapData[i, j] = int.Parse(row[j]);
            }
        }
    }

    void GenerateMap()
    {
        // Plane의 크기를 가져옴
        Vector3 planeScale = planeTransform.localScale;

        // 미로의 시작 위치 계산
        Vector3 mazeStartPosition = new Vector3(-mapSize.x * cellSize.x / 2f + cellSize.x / 2f, 0f, mapSize.y * cellSize.y / 2f - cellSize.y / 2f);

        // 랜덤한 위치에 열쇠 생성
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                if (mapData[i, j] == 4)
                {
                    // 위치 계산
                    Vector3 position = mazeStartPosition + new Vector3(j * cellSize.x, 0f, -i * cellSize.y);

                    // 열쇠 생성
                    GameObject key = Instantiate(keyPrefab, position + new Vector3(0f, 0.2f, 0f), Quaternion.identity);
                    key.transform.parent = planeTransform;
                }
            }
        }

        // 맵 데이터를 기반으로 벽과 문을 생성하여 배치
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                // 현재 위치의 맵 데이터 확인
                int cellType = mapData[i, j];

                // 벽 또는 문을 생성하여 배치
                if (cellType == 1 || cellType == 2 || cellType == 3)
                {
                    // 위치 계산
                    Vector3 position = mazeStartPosition + new Vector3(j * cellSize.x, 0f, -i * cellSize.y);

                    if (cellType == 1 || cellType == 2)
                    {
                        // 벽 생성
                        GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
                        wall.transform.localScale = new Vector3(cellSize.x, cellType == 2 ? 1.3f * cellSize.y : cellSize.y, cellSize.y);
                        wall.transform.parent = planeTransform;
                    }
                    else if (cellType == 3)
                    {
                        // 문 생성
                        GameObject door = Instantiate(doorPrefab, position, Quaternion.identity);
                        // 문을 90도로 회전시킴
                        door.transform.Rotate(Vector3.up, 90f);

                        door.transform.localScale = new Vector3(cellSize.x, cellSize.y, 0.1f);
                        door.transform.parent = planeTransform;

                        // 문에 열쇠를 할당
                        if (!hasKey && mapData[i, j] == 4)
                        {
                            GameObject key = Instantiate(keyPrefab, position + new Vector3(0f, cellSize.y / 2f, 0f), Quaternion.identity);
                            key.transform.parent = door.transform;
                            hasKey = true;
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌한 경우
        {
            Destroy(gameObject); // 열쇠 오브젝트 파괴
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth; // 맵의 가로 크기
    public int mapHeight; // 맵의 세로 크기
    public GameObject floorObject; // 맵 바닥을 나타내는 오브젝트

    public GameObject lowWallPrefab; // 낮은 벽 프리팹
    public GameObject highWallPrefab; // 높은 벽 프리팹
    public GameObject pathPrefab; // 통로 프리팹

    // CSV 파일의 경로
    public string mapDataPath = "Assets/Midterm/MapDataPath.csv";

    void Start()
    {
        GenerateMap(); // 맵 생성 함수 호출
        GenerateMazeFromCSV(); // CSV 파일로부터 미로 생성 함수 호출
    }

    void GenerateMap()
    {
        // Plane의 크기를 맵의 크기에 맞게 조정
        Transform planeTransform = GetComponentInChildren<Transform>(); // 자식 오브젝트 중 첫 번째로 발견된 Transform을 가져옴
        planeTransform.localScale = new Vector3(mapWidth, 1, mapHeight); // 가로, 세로 크기 조정
    }

    void GenerateMazeFromCSV()
    {
        string[] lines = File.ReadAllLines(mapDataPath);

        // 바닥 가운데의 시작 위치 계산
        float startX = -(mapWidth / 2.0f) + 0.5f;
        float startZ = -(mapHeight / 2.0f) + 0.5f;

        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            string[] cells = line.Split(',');

            for (int x = 0; x < cells.Length; x++)
            {
                int cellValue = int.Parse(cells[x]);

                // 벽이 겹치지 않는지 확인
                bool shouldInstantiateWall = true;
                Collider[] colliders = Physics.OverlapSphere(new Vector3(startX + x, 0, startZ - y), 0.1f);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Wall"))
                    {
                        shouldInstantiateWall = false;
                        break;
                    }
                }

                // 벽을 생성
                if (shouldInstantiateWall)
                {
                    // 낮은 벽인 경우
                    if (cellValue == 1)
                    {
                        Instantiate(lowWallPrefab, new Vector3(startX + x, 2f, startZ - y), Quaternion.identity);
                    }
                    // 높은 벽인 경우
                    else if (cellValue == 2)
                    {
                        Instantiate(highWallPrefab, new Vector3(startX + x, 1f, startZ - y), Quaternion.identity);
                    }
                    // 통로인 경우
                    else if (cellValue == 0)
                    {
                        Instantiate(pathPrefab, new Vector3(startX + x, 0, startZ - y), Quaternion.identity);
                    }
                }
            }
        }
    }
}

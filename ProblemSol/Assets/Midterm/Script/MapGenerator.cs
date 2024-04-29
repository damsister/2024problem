using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth = 10; // 맵의 가로 크기
    public int mapHeight = 10; // 맵의 세로 크기
    public GameObject floorObject; // 맵 바닥을 나타내는 오브젝트

    //public string mapDataPath; // 맵 데이터가 저장된 CSV 파일의 경로
    public GameObject lowWallPrefab; // 낮은 벽 프리팹
    public GameObject highWallPrefab; // 높은 벽 프리팹


    void Start()
    {
        GenerateMap();
        GenerateMapFromCSV();
    }

    void GenerateMap()
    {
        // 바닥의 크기 조정
        transform.localScale = new Vector3(mapWidth, 1, mapHeight);
    }

    void GenerateMapFromCSV()
    {
        string mapDataPath = "D:\\김도현-2\\유한대학교\\유한대3\\2024problem\\ProblemSol\\Assets\\Midterm\\Resources"; // 불러올 파일의 경로를 지정
        // CSV 파일 읽기
        string[] lines = File.ReadAllLines(mapDataPath);
        int rowCount = lines.Length;

        // 각 줄을 파싱하여 2차원 배열로 변환
        int[,] mapData = new int[rowCount, lines[0].Split(',').Length];
        for (int y = 0; y < rowCount; y++)
        {
            string[] values = lines[y].Split(',');
            for (int x = 0; x < values.Length; x++)
            {
                int.TryParse(values[x], out mapData[y, x]);
            }
        }

        // 맵 생성
        for (int y = 0; y < rowCount; y++)
        {
            for (int x = 0; x < mapData.GetLength(1); x++)
            {
                Vector3 spawnPosition = new Vector3(x, 0, -y); // y 좌표를 반전하여 맵을 정상적으로 생성
                if (mapData[y, x] == 1)
                {
                    Instantiate(lowWallPrefab, spawnPosition, Quaternion.identity);
                }
                else if (mapData[y, x] == 2)
                {
                    Instantiate(highWallPrefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}

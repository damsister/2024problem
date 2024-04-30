using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth = 10; // 맵의 가로 크기
    public int mapHeight = 10; // 맵의 세로 크기
    public GameObject floorObject; // 맵 바닥을 나타내는 오브젝트

    public GameObject lowWallPrefab; // 낮은 벽 프리팹
    public GameObject highWallPrefab; // 높은 벽 프리팹
    public GameObject pathPrefab; // 통로 프리팹

    public string MapDataPath = "MapDataPath"; // CSV 파일의 경로


    void Start()
    {
        GenerateMap();
        GenerateMazeFromCSV();
    }

    void GenerateMap()
    {
        // 바닥의 크기 조정
        transform.localScale = new Vector3(mapWidth, 1, mapHeight);
    }

    void GenerateMazeFromCSV()
    {
        string[] lines = Resources.Load<TextAsset>(MapDataPath).text.Split(',');


        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            string[] cells = line.Split(',');

            for (int x = 0; x < cells.Length; x++)
            {
                int cellValue = int.Parse(cells[x]);

                //낮은 벽인 경우
                if (cellValue == 2)
                {
                    Instantiate(lowWallPrefab, new Vector3(x, 0, -y), Quaternion.identity);
                }

                // 벽인 경우
                else if (cellValue == 1)
                {
                    Instantiate(highWallPrefab, new Vector3(x, 0, -y), Quaternion.identity);
                }

                // 통로인 경우
                else if (cellValue == 0)
                {
                    Instantiate(pathPrefab, new Vector3(x, 0, -y), Quaternion.identity);
                }
            }
        }
    }
}

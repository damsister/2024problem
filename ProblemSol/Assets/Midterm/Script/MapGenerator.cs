using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]

public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapGenerator script = (MapGenerator)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.Start();
        }
    }
}

public class MapGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // 벽 프리팹
    public TextAsset mapCSV; // 맵 정보를 담은 CSV 파일
    public Vector2 cellSize; // 셀의 크기
    public Transform planeTransform; // Plane의 Transform

    private int[,] mapData; // 맵 정보를 저장할 배열
    private Vector2Int mapSize = new Vector2Int(10, 10); // 고정된 맵의 가로세로 크기

    public void Start()
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
                }
            }
        }
    }
}


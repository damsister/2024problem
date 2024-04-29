using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth = 10; // ���� ���� ũ��
    public int mapHeight = 10; // ���� ���� ũ��
    public GameObject floorObject; // �� �ٴ��� ��Ÿ���� ������Ʈ

    //public string mapDataPath; // �� �����Ͱ� ����� CSV ������ ���
    public GameObject lowWallPrefab; // ���� �� ������
    public GameObject highWallPrefab; // ���� �� ������


    void Start()
    {
        GenerateMap();
        GenerateMapFromCSV();
    }

    void GenerateMap()
    {
        // �ٴ��� ũ�� ����
        transform.localScale = new Vector3(mapWidth, 1, mapHeight);
    }

    void GenerateMapFromCSV()
    {
        string mapDataPath = "D:\\�赵��-2\\���Ѵ��б�\\���Ѵ�3\\2024problem\\ProblemSol\\Assets\\Midterm\\Resources"; // �ҷ��� ������ ��θ� ����
        // CSV ���� �б�
        string[] lines = File.ReadAllLines(mapDataPath);
        int rowCount = lines.Length;

        // �� ���� �Ľ��Ͽ� 2���� �迭�� ��ȯ
        int[,] mapData = new int[rowCount, lines[0].Split(',').Length];
        for (int y = 0; y < rowCount; y++)
        {
            string[] values = lines[y].Split(',');
            for (int x = 0; x < values.Length; x++)
            {
                int.TryParse(values[x], out mapData[y, x]);
            }
        }

        // �� ����
        for (int y = 0; y < rowCount; y++)
        {
            for (int x = 0; x < mapData.GetLength(1); x++)
            {
                Vector3 spawnPosition = new Vector3(x, 0, -y); // y ��ǥ�� �����Ͽ� ���� ���������� ����
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

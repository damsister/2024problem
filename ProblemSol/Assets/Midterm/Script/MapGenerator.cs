using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth = 10; // ���� ���� ũ��
    public int mapHeight = 10; // ���� ���� ũ��
    public GameObject floorObject; // �� �ٴ��� ��Ÿ���� ������Ʈ

    public GameObject lowWallPrefab; // ���� �� ������
    public GameObject highWallPrefab; // ���� �� ������
    public GameObject pathPrefab; // ��� ������

    public string MapDataPath = "MapDataPath"; // CSV ������ ���


    void Start()
    {
        GenerateMap();
        GenerateMazeFromCSV();
    }

    void GenerateMap()
    {
        // �ٴ��� ũ�� ����
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

                //���� ���� ���
                if (cellValue == 2)
                {
                    Instantiate(lowWallPrefab, new Vector3(x, 0, -y), Quaternion.identity);
                }

                // ���� ���
                else if (cellValue == 1)
                {
                    Instantiate(highWallPrefab, new Vector3(x, 0, -y), Quaternion.identity);
                }

                // ����� ���
                else if (cellValue == 0)
                {
                    Instantiate(pathPrefab, new Vector3(x, 0, -y), Quaternion.identity);
                }
            }
        }
    }
}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = new int[5000]; //�迭 ����
        for(int i = 1; i < 5000; i++) //���� �ѹ� ã��
        {
            int gene = getGenerator(i); //���ʷ����� ���ϱ�
            if(gene < 5000) //���ʷ�����(������)�� 5000���� ���� ��쿡�� �迭�� ����
            {
                array[gene] = gene;
            }
        }
        //���� �ѹ� �հ�
        int sum = 0;
        for(int i = 1; i < 5000; i++)
        {
            if (array[i] == 0) //�迪�� ����� ���� 0�� ��쿡�� �ջ�
            {
                sum += i;
            }
        }
        Debug.Log(sum); //��� ���
    }

    //���ʷ����� ���ϱ�
    private int getGenerator(int integer)
    {
        int chpher = getCipher(integer); //�־��� ������ �ڸ����� ����
        int[] array = new int[chpher]; //�� �ڸ����� ������ �迭 ����

        //������ �� �ڸ����� �и��Ͽ� �迭�� ����
        int nTemp = integer;
        for(int i = 0; i < chpher; i++)
        {
            array[i] = nTemp % 10;
            nTemp /= 10;
        }
        //�迭�� ����� �� �ڸ����� ���� ����Ͽ� ��ȯ
        int sum = 0;
        for(int i = 0; i < chpher; i++)
        {
            sum += array[i];
        }
        return sum + integer;
    }

    //�ڸ��� ���ϱ�
    private int getCipher(int integer)
    {
        //�ڸ����� ������ ���� �ʱ�ȭ
        int count = 0;
        int nCheck = 1;

        //������ 10���� �����鼭 �ڸ����� ���
        while(integer / nCheck > 0)
        {
            nCheck *= 10;
            count++;
        }
        return count;
    }
}

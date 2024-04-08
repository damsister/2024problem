using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = new int[5000]; //배열 생성
        for(int i = 1; i < 5000; i++) //셀프 넘버 찾기
        {
            int gene = getGenerator(i); //제너레이터 구하기
            if(gene < 5000) //제너레이터(생성자)가 5000보다 작은 경우에만 배열에 저장
            {
                array[gene] = gene;
            }
        }
        //셀프 넘버 합계
        int sum = 0;
        for(int i = 1; i < 5000; i++)
        {
            if (array[i] == 0) //배역에 저장된 값이 0인 경우에만 합산
            {
                sum += i;
            }
        }
        Debug.Log(sum); //결과 출력
    }

    //제너레이터 구하기
    private int getGenerator(int integer)
    {
        int chpher = getCipher(integer); //주어진 정수의 자릿수를 구함
        int[] array = new int[chpher]; //각 자릿수를 저장할 배열 생성

        //정수를 각 자리수로 분리하여 배열에 저장
        int nTemp = integer;
        for(int i = 0; i < chpher; i++)
        {
            array[i] = nTemp % 10;
            nTemp /= 10;
        }
        //배열에 저장된 각 자리수의 합을 계산하여 반환
        int sum = 0;
        for(int i = 0; i < chpher; i++)
        {
            sum += array[i];
        }
        return sum + integer;
    }

    //자릿수 구하기
    private int getCipher(int integer)
    {
        //자릿수를 저장할 변수 초기화
        int count = 0;
        int nCheck = 1;

        //정수를 10으로 나누면서 자릿수를 세어감
        while(integer / nCheck > 0)
        {
            nCheck *= 10;
            count++;
        }
        return count;
    }
}

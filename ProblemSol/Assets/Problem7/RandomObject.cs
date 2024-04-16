using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR //편집기
using UnityEditor;
#endif
//#if ~ #endif : 전처리기 지시문

public class RandomObject : MonoBehaviour
{
    public GameObject targetObject;
    public int objectNumber = 0;
} //타겟
#if UNITY_EDITOR //개발할 때만 들어감

//GizomsSelected() //오버라이드함

//Matrix4x4 : 4*4 행렬
//TRS(Trans, Rot, Scal) : 이동행렬, 회전행렬, 크기변환행렬
//using(new Handles.DrawingScope(cubeTransfrom))//cubeTransfrom을 그릴 때 실행한다.
//using에 transfrom을 올려주세요
#endif
//자료구조 : tree
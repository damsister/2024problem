using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR //������
using UnityEditor;
#endif
//#if ~ #endif : ��ó���� ���ù�

public class RandomObject : MonoBehaviour
{
    public GameObject targetObject;
    public int objectNumber = 0;
} //Ÿ��
#if UNITY_EDITOR //������ ���� ��

//GizomsSelected() //�������̵���

//Matrix4x4 : 4*4 ���
//TRS(Trans, Rot, Scal) : �̵����, ȸ�����, ũ�⺯ȯ���
//using(new Handles.DrawingScope(cubeTransfrom))//cubeTransfrom�� �׸� �� �����Ѵ�.
//using�� transfrom�� �÷��ּ���
#endif
//�ڷᱸ�� : tree
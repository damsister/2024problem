using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR //편집기
using UnityEditor;
#endif
//#if ~ #endif : 전처리기 지시문

public class RandomObject : MonoBehaviour
{
    public GameObject TargetObject;
    public int ObjectNumber = 0;

#if UNITY_EDITOR //개발할 때만 들어감
    [CustomEditor(typeof(RandomObject))]
    public class RandomObjectGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RandomObject generator = (RandomObject)target;
            if (GUILayout.Button("Generate Objects"))
            {
                generator.GenerateObjects();
            }
        }
    }
#endif

    public void GenerateObjects()
    {
        // 이 곳에 Object를 생성하고 배치하는 코드를 작성하세요.
        for (int i = 0; i < ObjectNumber; i++)
        {
            // 랜덤한 위치를 생성합니다.
            Vector3 randomPosition = new Vector3(
                Random.Range(transform.position.x - transform.localScale.x * 5.0f, transform.position.x + transform.localScale.x * 5.0f),
                Random.Range(transform.position.y - transform.localScale.y * 5.0f, transform.position.y + transform.localScale.y * 5.0f),
                Random.Range(transform.position.z - transform.localScale.z * 5.0f, transform.position.z + transform.localScale.z * 5.0f)
            );

            // TargetObject를 생성하고 랜덤한 위치에 배치합니다.
            GameObject newObject = Instantiate(TargetObject, randomPosition, Quaternion.identity);
        }

    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() //GizomsSelected() //오버라이드함
    {
        // Box 형태의 가이드라인을 그립니다.
        Handles.color = Color.yellow;

        Matrix4x4 cubeTransform = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        //Matrix4x4 : 4*4 행렬
        //TRS(Trans, Rot, Scal) : 이동행렬, 회전행렬, 크기변환행렬

        using (new Handles.DrawingScope(cubeTransform)) //cubeTransfrom을 그릴 때 실행한다. //using에 transfrom을 올려주세요
        {
            Handles.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
#endif
}
//자료구조 : tree
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR //편집기
using UnityEditor;
#endif
//#if ~ #endif : 전처리기 지시문

public class villain : MonoBehaviour
{
    public GameObject villains;
    public int ObjectNumber = 0;
    public float spawnInterval = 10.0f; // 악당 생성 간격
    public Vector3 minBounds;
    public Vector3 maxBounds;

    /*#if UNITY_EDITOR //개발할 때만 들어감
        [CustomEditor(typeof(villain))]
        public class villainGeneratorEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                villain v = (villain)target;

                // 악당 생성 간격을 조절할 수 있는 필드를 추가합니다.
                v.spawnInterval = EditorGUILayout.FloatField("악당 생성 간격", v.spawnInterval);

                // 악당 생성 버튼 대신 삭제합니다.
                // if(GUILayout.Button("villain Generate"))
                // {
                //     v.GenerateObject();
                // }
            }
        }
    #endif*/

    //Plane의 범위 내에서 공 랜덤한 위치에 생성
    // 이 곳에 Object를 생성하고 배치하는 코드를 작성하세요.
    public void GenerateObject()
    {
        // 이 곳에 Object를 생성하고 배치하는 코드를 작성하세요.
        for (int i = 0; i < ObjectNumber; i++)
        {
            // 지정된 범위 내에서 랜덤 위치를 생성합니다.
            Vector3 randomPosition = new Vector3(
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(minBounds.y, maxBounds.y),
                Random.Range(minBounds.z, maxBounds.z)
            );

            // TargetObject를 생성하고 랜덤한 위치에 배치합니다.
            GameObject newObject = Instantiate(villains, randomPosition, Quaternion.identity);
        }

        /* // Plane을 식별하고 해당 평면의 범위 내에서 랜덤 위치를 생성합니다.
         GameObject plane = GameObject.FindGameObjectWithTag("plane");
         if (plane != null)
         {
             Renderer planeRenderer = plane.GetComponent<Renderer>();

             for (int i = 0; i < ObjectNumber; i++)
             {
                 Vector3 randomPosition = new Vector3(
                     Random.Range(planeRenderer.bounds.min.x, planeRenderer.bounds.max.x),
                     Random.Range(planeRenderer.bounds.min.y, planeRenderer.bounds.max.y),
                     Random.Range(planeRenderer.bounds.min.z, planeRenderer.bounds.max.z)
                 );

                 // TargetObject를 생성하고 랜덤한 위치에 배치합니다.
                 GameObject newObject = Instantiate(villains, randomPosition, Quaternion.identity);
             }
         }
         else
         {
             Debug.Log("Plane not found. Cannot generate objects.");
         }*/
    }

    private void Start()
    {
        // 일정한 간격으로 GenerateObject 함수를 호출하는 코루틴 시작
        StartCoroutine(SpawnVillains());
    }

    private IEnumerator SpawnVillains()
    {
        while (true)
        {
            GenerateObject(); // 악당 생성 함수 호출

            yield return new WaitForSeconds(spawnInterval); // 일정한 시간 간격을 기다림
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
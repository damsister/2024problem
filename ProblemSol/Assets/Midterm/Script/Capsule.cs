using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    float capsuleHeight = 2;
    float capsuleRadius = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        float diagonalLength = Mathf.Sqrt(capsuleHeight * capsuleHeight + 4 * capsuleRadius * capsuleRadius);
        float diagonalLength1 = diagonalLength * 3;
        Debug.Log(diagonalLength);
        Debug.Log(diagonalLength1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

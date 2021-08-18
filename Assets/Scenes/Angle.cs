using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour
{
    public GameObject Cube;
    // Start is called before the first frame update
    void Awake()
    {
        Cube = GameObject.Find("Cube");

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Cube.transform.position;
        float angle =  Vector2.SignedAngle(Vector2.right, pos);
        Vector2 a = new Vector2(100f*Mathf.Cos(angle), 100f*Mathf.Sin(angle));
        Debug.Log(a);
    }
}

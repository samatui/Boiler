using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Position_Follow : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject targetObject;

    void LateUpdate()
    {
        Vector3 mainCamPos = mainCam.transform.position.normalized;
        Vector3 targetPos = targetObject.transform.position;
        Vector2 pos = new Vector2(mainCamPos.x, mainCamPos.z);
        float angle = Vector2.SignedAngle(new Vector2(mainCamPos.x, mainCamPos.z), Vector2.up);

        //Vector2 a = new Vector2(100f * Mathf.Cos(angle), 100f * Mathf.Sin(angle));
        
        Vector3 newpos = new Vector3(targetPos.x + 12f * Mathf.Sin(angle * Mathf.Deg2Rad), 
            5, targetPos.z + 12f * Mathf.Cos(angle * Mathf.Deg2Rad));
        this.transform.position = newpos;
        Debug.Log(Mathf.Sin(angle));
        Debug.Log(newpos);
    }
}

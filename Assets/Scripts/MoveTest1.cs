using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class MoveTest1 : MonoBehaviour
{
    private Vector3 targetPos;
    protected bool m_bRaycastHit;
    protected const float m_fDefaultDistance = 5.0f;
    protected const float m_fDefaultWheelSpeed = 5.0f;
    public float m_fDistance = m_fDefaultDistance;
    public float m_fXSpeed = 350.0f;
    public float m_fYSpeed = 300.0f;
    public float m_fWheelSpeed = m_fDefaultWheelSpeed;

    public float m_fYMinLimit = -90f;
    public float m_fYMaxLimit = 90f;

    private float m_fDistanceMin = 0.10f;
    private float m_fDistanceMax = 500;

    public int m_nMoveInputIndex = 1;
    public int m_nRotInputIndex = 0;

    public float m_fXRot = 0.0f;
    public float m_fYRot = 0.0f;

    protected bool m_bHandEnable = true;
    protected Vector3 m_MovePostion;
    protected Vector3 m_OldMousePos;
    protected bool m_bLeftClick;
    protected bool m_bRightClick;
    public GameObject cam;



    public static bool isMove = false;

    void OnEnable()
    {
        //m_fDistance = PlayerPrefs.GetFloat("FxmTestMouse.m_fDistance", m_fDistance);
        targetPos = this.transform.position;//Vector3.zero;  //m_TargetTrans.position;
    }

    void Start()
    {
        if (Camera.main == null)
            return;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        Debug.Log(m_fXRot + "\t" + m_fYRot);
    }
    bool IsGUIMousePosition()
    {
        Vector2 pos = GetGUIMousePosition();
        if (new Rect(0, 0, Screen.width, Screen.height / 10 + 30).Contains(pos))
            return true;
        if (new Rect(0, 0, 40, Screen.height).Contains(pos))
            return true;
        return false;
    }

    public static Vector2 GetGUIMousePosition()
    {
        return new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
    }

    void Update()
    {
        if (IsGUIMousePosition() && (m_bLeftClick == false && m_bRightClick == false))
            return;
        //if (isMove)
        //{
            UpdateCamera(false);
        //}
    }

    public void UpdateCamera(bool bOnlyZoom)
    {

        
        if (Camera.main == null)
            return;

        if (m_fWheelSpeed < 0)
            m_fWheelSpeed = m_fDefaultWheelSpeed;

        float fDistRate = m_fDistance / m_fDefaultDistance;

        float fOldDistance = m_fDistance;

        if (true)
        {
            m_fDistance = Mathf.Clamp(m_fDistance - Input.GetAxis("Mouse ScrollWheel") * m_fWheelSpeed * fDistRate, m_fDistanceMin, m_fDistanceMax);

            if (!bOnlyZoom && m_bRightClick && Input.GetMouseButton(m_nRotInputIndex))
            {
                m_fXRot += Input.GetAxis("Mouse X") * m_fXSpeed * 0.02f;
                m_fYRot -= Input.GetAxis("Mouse Y") * m_fYSpeed * 0.02f;
            }

            if (!bOnlyZoom && Input.GetMouseButtonDown(m_nRotInputIndex))
                m_bRightClick = true;
            if (!bOnlyZoom && Input.GetMouseButtonUp(m_nRotInputIndex))
                m_bRightClick = false;

            m_fYRot = ClampAngle(m_fYRot, m_fYMinLimit, m_fYMaxLimit); 
        }
        Quaternion rotation = Quaternion.Euler(m_fYRot, m_fXRot, 0); 

        Debug.Log(m_fXRot + "\t" + m_fYRot+"\t"+rotation);

     
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -m_fDistance);
        Vector3 position = rotation * negDistance + targetPos;

        Camera.main.transform.rotation = rotation;
        Camera.main.transform.position = position;
        cam.transform.position = position;
        cam.transform.rotation = rotation;
        //UpdatePosition(Camera.main.transform);
        // save
        if (fOldDistance != m_fDistance)
            PlayerPrefs.SetFloat("FxmTestMouse.m_fDistance", m_fDistance);
    }


    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    void UpdatePosition(Transform camera)
    {
        //if (m_bHandEnable)
        //{
        //    if (Input.GetMouseButtonDown(m_nMoveInputIndex))
        //    {
        //        m_OldMousePos = Input.mousePosition;
        //        m_bLeftClick = true;
        //    }

        //    if (m_bLeftClick && Input.GetMouseButton(m_nMoveInputIndex))
        //    {
        //        Vector3 currMousePos = Input.mousePosition;
        //        float worldlen = GetWorldPerScreenPixel(targetPos);

        //        m_MovePostion += (m_OldMousePos - currMousePos) * worldlen;
        //        m_OldMousePos = currMousePos;
        //    }
        //    if (Input.GetMouseButtonUp(m_nMoveInputIndex))
        //        m_bLeftClick = false;
        //}

        camera.Translate(m_MovePostion, Space.Self);
    }

    //public static float GetWorldPerScreenPixel(Vector3 worldPoint)
    //{
    //    Camera cam = Camera.main;
    //    if (cam == null)
    //        return 0;
    //    Plane nearPlane = new Plane(cam.transform.forward, cam.transform.position);
    //    float dist = nearPlane.GetDistanceToPoint(worldPoint);
    //    float sample = 100;
    //    return Vector3.Distance(cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2 - sample / 2, dist)), cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2 + sample / 2, dist))) / sample;
    //}

}

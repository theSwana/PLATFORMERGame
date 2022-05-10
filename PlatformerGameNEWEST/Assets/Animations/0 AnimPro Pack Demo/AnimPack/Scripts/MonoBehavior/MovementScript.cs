using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementScript : MonoBehaviour
{

    public Camera cam;

    public Animator animator;

    public bool useCustomRotation;
    public float rotationYSpeed = 1f;
    public float rotationXSpeed = 150f;


    private const string verticalAxis = "mouse y";
    private const string horizontalAxis = "mouse x";

    private float defaultYMaxSpeed;
    private float defaultXMaxSpeed;
    // Use this for initialization
    void Start ()
    {/*
        if (cinemachine != null && !useCustomRotation)
        {
            defaultYMaxSpeed = cinemachine.m_YAxis.m_MaxSpeed;
            defaultXMaxSpeed = cinemachine.m_XAxis.m_MaxSpeed;
        }else
        {
            cinemachine.m_YAxis.m_MaxSpeed = rotationYSpeed;
            cinemachine.m_XAxis.m_MaxSpeed = rotationXSpeed;
        }*/


	}
	
	// Update is called once per frame
	void Update ()
    {

        var rotationV = (-Input.GetAxis(verticalAxis) * Time.deltaTime) * rotationYSpeed;
        var rotationH = (-Input.GetAxis(horizontalAxis) * Time.deltaTime) * rotationXSpeed;

        //if (Input.GetButton(verticalAxis) || Input.GetButton(horizontalAxis))
        //{
        //    Debug.Log("ROTACIONANDO V" + rotationV);
        //    transform.Rotate(new Vector3 ( rotationV,rotationH ));
        //}


        cam.transform.LookAt(transform);

        if (Input.GetKey(KeyCode.Mouse0) )
        {
            if (useCustomRotation)
            {
                cam.transform.Translate(new Vector3(rotationH, 0));
                //cinemachine.m_XAxis.m_MaxSpeed = rotationXSpeed;
            }else
            {
                //cinemachine.m_XAxis.m_MaxSpeed = defaultXMaxSpeed;
            }
        }
        else
        {
            
            //cinemachine.m_XAxis.m_MaxSpeed = 0;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (useCustomRotation)
            {
                cam.transform.Translate(new Vector3(0, rotationV));
                //cinemachine.m_YAxis.m_MaxSpeed = rotationYSpeed;
            }
            else
            {
                //cinemachine.m_YAxis.m_MaxSpeed = defaultYMaxSpeed;
            }
        }else
        {
            //cinemachine.m_YAxis.m_MaxSpeed = 0;
        }
            cam.transform.localPosition = new Vector3(Mathf.Clamp(cam.transform.position.x, -5, 5), Mathf.Clamp(cam.transform.position.y, -5, 5), Mathf.Clamp(cam.transform.position.x, -2.5f,2.6f));
    }

    public void ResetTransform()
    {
        transform.position = Vector3.zero;
    }
}

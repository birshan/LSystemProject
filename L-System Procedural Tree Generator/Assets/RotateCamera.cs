using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(target!=null)
     {
         //look at target with the UIconfig vertical offset
        // transform.LookAt(target.position + Vector3.up * UIConfig.cameraVerticalOffset);

        transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
     }   
    }
}

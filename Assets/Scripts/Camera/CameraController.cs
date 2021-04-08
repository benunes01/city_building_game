using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;

    public float minXRot;
    public float maxXRot;

    private float curXRot;

    public float minZoom;
    public float maxZoom;
    public float zoomSpeed;
    public float rotateSpeed;

    private float curZoom;
    private Camera cam;

    void Start() {
        cam = Camera.main;
        curZoom = cam.transform.localPosition.y;
        curXRot = -50;
    }

    void Update() {
        // zooming
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);

        cam.transform.localPosition = Vector3.up * curZoom;

        // Camera look
        if(Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            curXRot += -y * rotateSpeed;
            curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);

            transform.eulerAngles = new Vector3(curXRot, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);
        }

        // Movement
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        Vector3 right = cam.transform.right;
 
        // get keyboard inputs
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
 
        // get a local direction
        Vector3 dir = forward * moveZ + right * moveX;
        dir.Normalize();
 
        // convert it from frames per second to meters per second
        dir *= moveSpeed * Time.deltaTime;
 
        // apply it to the camera anchor
        transform.position += dir;
    }   


}

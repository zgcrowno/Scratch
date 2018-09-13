using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2,
        MouseZ = 3
    }

    public RotationAxis axes = RotationAxis.MouseX;

    public float minVert = -90.0f;
    public float maxVert = 90.0f;

    // Sensitivities
    public float sensHorizontal = 10.0f;
    public float sensVertical = 10.0f;

    public float rotationX = 0;
	
	// Update is called once per frame
	void Update () {
		if(axes == RotationAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
        }
        else if(axes == RotationAxis.MouseY)
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensVertical;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert); //Clamps the vertical angle within the min and max limits

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
	}
}

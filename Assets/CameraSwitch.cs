using UnityEngine;
using System.Collections;
using System.Threading;

public class CameraSwitch : MonoBehaviour {
    public Camera cameraR;
    public Camera cameraL;

    void Start(){}

    public void ShowRightView()
    {
        cameraR.enabled = true;
        cameraL.enabled = false;
    }

    public void ShowLeftView()
    {
        cameraR.enabled = false;
        cameraL.enabled = true;
    }

    public void SwitchCamera(int camId)
    {
        if (camId == 0)
        {
            ShowRightView();
        }
        else if (camId == 1)
        {
            ShowLeftView();
        }
    }

    void Update()
    {
        if (Input.GetKey("a"))
            ShowRightView();
        
        if (Input.GetKey("e"))
            ShowLeftView();
    }
}
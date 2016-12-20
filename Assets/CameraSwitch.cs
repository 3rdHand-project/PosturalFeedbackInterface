using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {
    public Camera cameraR;
    public Camera cameraL;

    void Start()
    {
        ShowLeftView();
    }

    private void ShowRightView()
    {
        cameraR.enabled = true;
        cameraL.enabled = false;
    }

    public void ShowLeftView()
    {
        cameraR.enabled = false;
        cameraL.enabled = true;
    }

    void Update()
    {
        if (Input.GetKey("a"))
            ShowRightView();

        if (Input.GetKey("e"))
            ShowLeftView();
    }
}
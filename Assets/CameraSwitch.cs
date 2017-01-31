using UnityEngine;
using System.Collections;
using System.Threading;

public class CameraSwitch : MonoBehaviour {
    public Camera cameraR;
    public Camera cameraM;
    public Camera cameraL;
    public int initialCamID;

    private Camera[] cameras;
    private int activeCamID;

    void Awake()
    {
        cameras = new Camera[] { cameraR, cameraM, cameraL };
        activeCamID = initialCamID;
    }

    void Start()
    {
        ShowView(initialCamID);
    }

    public Camera GetActiveCamera()
    {
        return cameras[activeCamID];
    }

    public void ShowView(int camID)
    {
        cameras[activeCamID].enabled = false;
        cameras[camID].enabled = true;
        activeCamID = camID;
    }

    void Update()
    {
        if (Input.GetKey("a"))
            ShowView(0);
        if (Input.GetKey("z"))
            ShowView(1);
        if (Input.GetKey("e"))
            ShowView(2);
    }
}
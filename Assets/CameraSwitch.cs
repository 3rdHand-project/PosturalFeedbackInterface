using UnityEngine;
using System.Collections;
using System.Threading;

public class CameraSwitch : MonoBehaviour {
    public GameObject cameraR;
    public GameObject cameraM;
    public GameObject cameraL;
    public int initialCamID;

    private GameObject[] cameras;
    private int activeCamID;

    void Awake()
    {
        cameras = new GameObject[] { cameraR, cameraM, cameraL };
        activeCamID = initialCamID;
    }

    void Start()
    {
        for(int i=0; i<cameras.Length; ++i)
        {
            ActivateCamera(i, false);
        }
        ShowView(initialCamID);
    }

    public GameObject GetActiveCamera()
    {
        return cameras[activeCamID];
    }

    public void ActivateCamera(int camID, bool activate)
    {
        Camera[] Cams = cameras[camID].GetComponentsInChildren<Camera>();
        for (int i = 0; i < Cams.Length; ++i)
        {
            Cams[i].enabled = activate;
        }
    }

    public void ShowView(int camID)
    {
        ActivateCamera(activeCamID, false);
        ActivateCamera(camID, true);
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
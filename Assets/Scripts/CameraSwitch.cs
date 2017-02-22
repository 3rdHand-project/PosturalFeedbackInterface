using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;

public class CameraSwitch : MonoBehaviour {
    public GameObject cameraF;
    public GameObject cameraB;
    public GameObject cameraR;
    public GameObject cameraL;
    public int initialCamID;

    private GameObject[] cameras;
    private int activeCamID;
    private Text textButton;
    private string[] texts;

    void Awake()
    {
        cameras = new GameObject[] { cameraF, cameraB, cameraR, cameraL };
        activeCamID = initialCamID;
        textButton = GameObject.Find("Canvas/Canvas/ButtonCamera").GetComponentInChildren<Text>();
        texts = new string[] { "Back", "Front", "Back", "Back" };
    }

    void Start()
    {
        // deactivate all cameras
        for(int i=0; i<cameras.Length; ++i)
        {
            Camera[] Cams = cameras[i].GetComponentsInChildren<Camera>();
            for (int j = 0; j < Cams.Length; ++j)
            {
                Cams[j].enabled = false;
            }
            Light light = cameras[i].GetComponentInChildren<Light>();
            light.enabled = false;
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

        Light light = cameras[camID].GetComponentInChildren<Light>();
        light.enabled = activate;
    }

    public void ShowView(int camID)
    {
        ActivateCamera(activeCamID, false);
        ActivateCamera(camID, true);
        activeCamID = camID;
        textButton.text = texts[activeCamID];
    }

    void Update()
    {
        if (Input.GetKey("a"))
            ShowView(2);
        if (Input.GetKey("z"))
            ShowView(0);
        if (Input.GetKey("e"))
            ShowView(3);
    }
}
using UnityEngine;
using System.Collections;
using System.Threading;

public class CameraSwitch : MonoBehaviour {
    public Camera cameraR;
    public Camera cameraL;

    private ModelController modelM;
    private ModelController modelF;

    void Start()
    {
        ShowLeftView();
        modelM = GameObject.Find("CharacterMale").GetComponent<ModelController>();
        modelF = GameObject.Find("CharacterFemale").GetComponent<ModelController>();
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

        if (Input.GetKey("q"))
        {
            modelM.showModel();
            modelF.hideModel();
        }

        if (Input.GetKey("d"))
        {
            modelM.hideModel();
            modelF.showModel();
        }
    }
}
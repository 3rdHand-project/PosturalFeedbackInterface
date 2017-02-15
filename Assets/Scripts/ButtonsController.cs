using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonsController : MonoBehaviour
{
    private ModelSwitch modSwitch;
    private CameraSwitch camSwitch;
    private Button buttonMod;
    private Button buttonCam;
    private Button buttonQuit;
    
    void Start()
    {
        buttonMod = GameObject.Find("Canvas/Canvas/ButtonModel").GetComponent<Button>();
        buttonMod.onClick.AddListener(OnClickModel);

        buttonCam = GameObject.Find("Canvas/Canvas/ButtonCamera").GetComponent<Button>();
        buttonCam.onClick.AddListener(OnClickCamera);

        buttonQuit = GameObject.Find("Canvas/Canvas/ButtonQuit").GetComponent<Button>();
        buttonQuit.onClick.AddListener(OnClickQuit);

        modSwitch = GetComponent<ModelSwitch>();
        camSwitch = GetComponent<CameraSwitch>();
    }

    void OnClickCamera()
    {
        Text buttonText = buttonCam.GetComponentInChildren<Text>();
        if (buttonText.text.Equals("Front"))
            camSwitch.ShowView(0);
        else
            camSwitch.ShowView(1);
    }

    void OnClickModel()
    {
        Text buttonText = buttonMod.GetComponentInChildren<Text>();
        if (buttonText.text.Equals("Male"))
            modSwitch.ShowModel(0);
        else
            modSwitch.ShowModel(1);
    }

    void OnClickQuit()
    {
        Debug.Log("Quitting application");
        Application.Quit();
    }
}

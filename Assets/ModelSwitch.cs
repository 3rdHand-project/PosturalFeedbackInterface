using UnityEngine;
using System.Collections;
using System.Threading;

public class ModelSwitch : MonoBehaviour
{
    public ModelController modelM;
    public ModelController modelF;

    private ModelController activeModel;

    void Start() {}

    public ModelController getActiveModel()
    {
        return activeModel;
    }

    public void ShowMen()
    {
        modelM.showModel();
        modelF.hideModel();
        activeModel = modelM;
    }

    public void ShowWomen()
    {
        modelM.hideModel();
        modelF.showModel();
        activeModel = modelF;
    }

    public void SwitchModel(int modId)
    {
        if (modId == 0)
        {
            ShowMen();
        }
        else if (modId == 1)
        {
            ShowWomen();
        }
    }

    void Update()
    {
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
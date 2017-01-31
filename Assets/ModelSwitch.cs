using UnityEngine;
using System.Collections;
using System.Threading;

public class ModelSwitch : MonoBehaviour
{
    public ModelController modelM;
    public ModelController modelF;
    public int initialModID;

    private ModelController[] models;
    private int activeModID;

    void Awake()
    {
        models = new ModelController[] { modelM, modelF };
        activeModID = initialModID;
    }

    void Start()
    {
        ShowModel(initialModID);
    }

    public ModelController GetActiveModel()
    {
        return models[activeModID];
    }

    public void ShowModel(int modID)
    {
        models[activeModID].HideModel();
        models[modID].ShowModel();
        activeModID = modID;
    }

    void Update()
    {
        if (Input.GetKey("q"))
            ShowModel(0);
        if (Input.GetKey("d"))
            ShowModel(1);
    }

    public void SetMuscleValue(ref float[] newMuscles)
    {
        foreach (ModelController m in models)
        {
            m.SetMuscleValue(ref newMuscles);
        }
    }
}
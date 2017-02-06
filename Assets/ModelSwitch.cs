using UnityEngine;
using System.Collections;
using System.Threading;

public class ModelSwitch : MonoBehaviour
{
    public int initialModID;
    public ModelController controlM;
    public RiskFeedback feedbackM;
    public ModelController controlF;
    public RiskFeedback feedbackF;

    private ModelController[] models;
    private RiskFeedback[] feedbacks;
    private int activeModID;
    private string[] activeFeedbackPoints;

    void Awake()
    {
        models = new ModelController[] { controlM, controlF };
        feedbacks = new RiskFeedback[] { feedbackM, feedbackF };
        activeModID = initialModID;
        activeFeedbackPoints = new string[] { };
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
        ShowFeedback();
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
            m.SetMuscleValue(ref newMuscles);
    }

    private void ShowFeedback()
    {
        // deactivate all feedback
        foreach (RiskFeedback f in feedbacks)
            f.DeactivateAll();
        // only activate active model
        feedbacks[activeModID].ActivatePoint(ref activeFeedbackPoints, true);
    }

    public void ShowFeedback(ref string[] feedbackPoints)
    {
        activeFeedbackPoints = feedbackPoints;
        ShowFeedback();        
    }
}
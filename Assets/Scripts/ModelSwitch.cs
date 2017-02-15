using UnityEngine;
using UnityEngine.UI;
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
    private float[] activeFeedbackPoints;
    private Text textButton;
    private string[] texts;

    void Awake()
    {
        models = new ModelController[] { controlM, controlF };
        feedbacks = new RiskFeedback[] { feedbackM, feedbackF };
        activeModID = initialModID;
        textButton = GameObject.Find("Canvas/Canvas/ButtonModel").GetComponentInChildren<Text>();
        texts = new string[] { "Female", "Male" };
    }

    void Start()
    {
        int nb_feedback = feedbacks[initialModID].GetNumberOfFeedbackPoints();
        activeFeedbackPoints = new float[nb_feedback];
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
        textButton.text = texts[activeModID];
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
        feedbacks[activeModID].ActivatePoint(ref activeFeedbackPoints);
    }

    public void ShowFeedback(ref float[] feedbackPoints)
    {
        activeFeedbackPoints = feedbackPoints;
        ShowFeedback();        
    }

    public int GetNumberOfFeedbackPoints()
    {
        return feedbacks[activeModID].GetNumberOfFeedbackPoints();
    }
}
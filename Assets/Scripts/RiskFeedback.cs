using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiskFeedback : MonoBehaviour {
    public GameObject model;
    private Dictionary<string, GameObject> spheres;
    private List<string> sphereNames;

    void Awake ()
    {
        sphereNames = new List<string>() { "Spine", "Neck", "LeftArm", "RightArm", "LeftForeArm", "RightForeArm", "LeftHand", "RightHand" };
        spheres = new Dictionary<string, GameObject>();

        Transform[] allChilds = GetComponentsInChildren<Transform>();
        Dictionary<string, Transform> bodyT = new Dictionary<string, Transform>();
        foreach (Transform t in allChilds)
        {
            string childName = t.name;
            int startName = childName.IndexOf(":");
            if (startName != -1)
            {
                childName = childName.Remove(0, startName + 1);
                if (sphereNames.Contains(childName))
                {
                    bodyT.Add(childName, t);
                }
            }

        }
        foreach (string str in sphereNames)
        {
            GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            s.layer = 8;
            s.transform.parent = bodyT[str];
            s.transform.localPosition = new Vector3(0, 0, 0); 
            s.transform.localScale = new Vector3(.15f, .15f, .15f);
            RenderSphere(s, false);
            spheres.Add(str, s);
        }
    }

    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {}

    private void RenderSphere(GameObject s, float color_value)
    {
        Renderer r = s.GetComponentInChildren<Renderer>();
        r.material.color = Color.Lerp(Color.green, Color.red, color_value);
        r.enabled = true;
    }

    private void RenderSphere(GameObject s, bool activate)
    {
        Renderer r = s.GetComponentInChildren<Renderer>();
        r.enabled = activate;
    }

    public void ActivatePoint(string pointName,  bool activate)
    {
        RenderSphere(spheres[pointName], activate);
    }

    public void ActivatePoint(ref string[] pointNames, bool activate)
    {
        foreach(string str in pointNames)
            ActivatePoint(str, activate);
    }

    public void ActivatePoint(string pointName, float pointValue)
    {
        RenderSphere(spheres[pointName], pointValue);
    }

    public void ActivatePoint(ref float[] pointValues)
    {
        for (int i =0; i<pointValues.Length; ++i)
        {
            string pointName = sphereNames[i];
            ActivatePoint(pointName, pointValues[i]);
        }
    }

    public void DeactivateAll()
    {
        foreach (KeyValuePair<string, GameObject> entry in spheres)
        {
            RenderSphere(entry.Value, false);   
        }
    }

    public int GetNumberOfFeedbackPoints()
    {
        return sphereNames.Count;
    }
}

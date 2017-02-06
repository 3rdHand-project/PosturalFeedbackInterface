using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiskFeedback : MonoBehaviour {
    public GameObject model;
    private Dictionary<string, GameObject> spheres;

    void Awake ()
    {
        List<string> sphereNames = new List<string>() { "Spine", "Neck", "LeftArm", "RightArm", "LeftForeArm", "RightForeArm", "LeftHand", "RightHand" };
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
            s.transform.localScale = new Vector3(.1f, .1f, .1f);
            RenderSphere(s, false);
            spheres.Add(str, s);
        }
    }

    // Use this for initialization
    void Start () {}
	
	// Update is called once per frame
	void Update () {}

    private void RenderSphere(GameObject s, bool activate)
    {
        Renderer r = s.GetComponentInChildren<Renderer>();
        r.enabled = activate;
    }

    public void ActivatePoint(string pointName, bool activate)
    {
        RenderSphere(spheres[pointName], activate);
    }

    public void ActivatePoint(ref string[] pointNames, bool activate)
    {
        foreach(string str in pointNames)
        {
            ActivatePoint(str, activate);
        }
    }

    public void DeactivateAll()
    {
        foreach (KeyValuePair<string, GameObject> entry in spheres)
        {
            RenderSphere(entry.Value, false);   
        }
    }
}

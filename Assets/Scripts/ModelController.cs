using UnityEngine;
using System.Collections;

public class ModelController: MonoBehaviour {
    static readonly object lockObject = new object();

    private HumanPoseHandler hph;
    private bool poseModified;
    private HumanPose[] targetPoses;
    private int poseStep;
    private HumanPose currentPose;

    public Transform root;

    void Awake()
    {
        // add some transparency
        // AddTransparency();
        // by default hide the model
        HideModel();
    }

    // Use this for initialization
    void Start () {
        hph = new HumanPoseHandler(GetComponent<Animator>().avatar, root);
        currentPose = new HumanPose();
        hph.GetHumanPose(ref currentPose);
        poseModified = false;
        poseStep = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (poseModified) {
            lock (lockObject) {
                if (poseStep < targetPoses.Length)
                {
                    hph.SetHumanPose(ref targetPoses[poseStep]);
                    currentPose = targetPoses[poseStep];
                    poseStep += 1;
                }
                else
                {
                    poseModified = false;
                    poseStep = 0;
                }
            }
        }

    }

    public void PrintPose()
    {
        hph.GetHumanPose(ref currentPose);
        Debug.Log("------");
        for (int i=0; i < currentPose.muscles.Length; ++i)
        {
            Debug.Log(i);
            Debug.Log(currentPose.muscles[i]);
            Debug.Log("------------");
        }
        Debug.Log(currentPose.muscles.Length);
    }
    
    private HumanPose MusclesToPose(ref float[] muscles_array)
    {
        HumanPose pose = new HumanPose();
        pose.bodyPosition = currentPose.bodyPosition;
        pose.bodyRotation = currentPose.bodyRotation;
        pose.muscles = new float[currentPose.muscles.Length];
        System.Array.Copy(muscles_array, pose.muscles, muscles_array.Length);
        return pose;
    }

    public void SetMuscleValue(ref float[] newMuscles) {
        lock (lockObject) {
            targetPoses = new HumanPose[1];
            targetPoses[0] = MusclesToPose(ref newMuscles);
            poseModified = true;
        }
    }

    public void SetPath(ref float[][] newPath)
    {
        int pathLenght = newPath.Length;
        lock(lockObject)
        {
            targetPoses = new HumanPose[pathLenght];
            for (int i=0; i<pathLenght; ++i)
            {
                targetPoses[i] = MusclesToPose(ref newPath[i]);
            }
            poseModified = true;
        }
    }

    private void AddTransparency()
    {
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            r.material.shader = Shader.Find("Transparent/Diffuse");
            Color c = r.material.color;
            c.a = 1.0f;
            r.material.color = c;
        }
    }

    public void ShowModel() {
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = true;
    }

    public void HideModel() {
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = false;
    }
}

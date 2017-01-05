using UnityEngine;
using System.Collections;

public class ModelController: MonoBehaviour {
    static readonly object lockObject = new object();
    private HumanPoseHandler hph;
    private HumanPose hp;
    private bool poseModified;
    private HumanPose targetPose;

    public Transform root;

    // Use this for initialization
    void Start () {
        hph = new HumanPoseHandler(GetComponent<Animator>().avatar, root);
        hp = new HumanPose();
        poseModified = false;

        // initialize targetPose
        lock (lockObject) {
            hph.GetHumanPose(ref hp);
            targetPose = new HumanPose();
            targetPose.bodyPosition = hp.bodyPosition;
            targetPose.bodyRotation = hp.bodyRotation;
            targetPose.muscles = new float[HumanTrait.MuscleName.Length];
            System.Array.Copy(hp.muscles, targetPose.muscles, hp.muscles.Length);
        }

        // by default hide the model
        hideModel();
    }
	
	// Update is called once per frame
	void Update () { 
        if (poseModified) {
            poseModified = false;
            lock (lockObject) {
                hph.SetHumanPose(ref targetPose);
            }
        }
    }

    void PrintPose()
    {
        hph.GetHumanPose(ref hp);
        Debug.Log("------");
        for (int i=0; i < hp.muscles.Length; ++i)
        {
            Debug.Log(i);
            Debug.Log(hp.muscles[i]);
            Debug.Log("------------");
        }
        Debug.Log(hp.muscles.Length);
    }
    
    public void setMuscleValue(float[] newMuscles) {
        lock (lockObject) {
            System.Array.Copy(newMuscles, targetPose.muscles, newMuscles.Length);
            poseModified = true;
        }
    }

    public void showModel() {
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = true;
    }

    public void hideModel() {
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = false;
    }
}

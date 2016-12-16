using UnityEngine;
using System.Collections;

public class ModelController: MonoBehaviour {
    static readonly object lockObject = new object();
    private HumanPoseHandler hph;
    private HumanPose hp;
    private bool poseModified;
    private HumanPose targetPose;

    // Use this for initialization
    void Start () {
        hph = new HumanPoseHandler(GetComponent<Animator>().avatar, GetComponent<Transform>());
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
        Debug.Log(hp.bodyPosition);
        Debug.Log(hp.bodyRotation);
        foreach (float muscle in hp.muscles)
        {
            Debug.Log(muscle);
        }
        Debug.Log(hp.muscles.Length);
    }
    
    void setMuscleValue(float[] newMuscles) {
        lock (lockObject) {
            System.Array.Copy(newMuscles, targetPose.muscles, newMuscles.Length);
            poseModified = true;
        }
    }
}

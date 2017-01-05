using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPManager : MonoBehaviour {
    private static UdpClient udp;
    private Thread thread;
    private ModelController model;

    public string activeModelId;
    private int muscleLength;
    private float[] muscles;

    // Use this for initialization
    void Start () {
        udp = new UdpClient(5005);
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
        muscleLength = HumanTrait.MuscleName.Length;
        muscles = new float[muscleLength];
        //model = GameObject.Find("CharacterMale").GetComponent<ModelController>();
        
    }
	
	// Update is called once per frame
	void Update () {}

    void OnApplicationQuit(){
        udp.Close();
        thread.Abort();
    }

    void setActiveModel(string id_model) {
        Debug.Log("------");
        Debug.Log("ready to activate model");
        model = GameObject.Find("CharacterMale").GetComponent<ModelController>();
        model.showModel();
        Debug.Log("done");
    }

    private void ThreadMethod(){
        while (true) {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint);
            for (int i=0; i < muscleLength; ++i) {
                muscles[i] = System.BitConverter.ToSingle(receiveBytes, i * 4);
            }
            model.setMuscleValue(muscles);
        }
    }
}

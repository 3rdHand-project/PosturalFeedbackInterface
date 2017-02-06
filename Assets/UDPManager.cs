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
    private ModelSwitch modSwitch;
    private CameraSwitch camSwitch;

    private bool isCamSwitched;
    private bool isModSwitched;
    private bool isFeedback;

    private int activeCamID;
    private int activeModID;
    private string[] activeFeedbackPoints;

    private int muscleLength;
    private float[] muscles;

    // Use this for initialization
    void Start()
    {
        udp = new UdpClient(5005);
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
        muscleLength = HumanTrait.MuscleName.Length;
        muscles = new float[muscleLength];

        camSwitch = GetComponent<CameraSwitch>();
        modSwitch = GetComponent<ModelSwitch>();

        isCamSwitched = false;
        isModSwitched = false;
        isFeedback = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCamSwitched)
        {
            camSwitch.ShowView(activeCamID);
            isCamSwitched = false;
        }
        if (isModSwitched)
        {
            modSwitch.ShowModel(activeModID);
            isModSwitched = false;
        }
        if (isFeedback)
        {
            modSwitch.ShowFeedback(ref activeFeedbackPoints);
            isFeedback = false;
        }
    }

    void OnApplicationQuit()
    {
        udp.Close();
        thread.Abort();
    }

    private void ReadMusclesArray(ref byte[] bytes)
    {
        for (int i = 0; i < muscleLength; ++i)
        {
            muscles[i] = System.BitConverter.ToSingle(bytes, i * 4);
        }
        modSwitch.SetMuscleValue(ref muscles);
    }

    private void ReadActiveCamera(ref byte[] bytes)
    {
        int id_camera = System.BitConverter.ToInt32(bytes, 0);
        activeCamID = id_camera;
        isCamSwitched = true;
    }

    private void ReadActiveModel(ref byte[] bytes)
    {
        int id_model = System.BitConverter.ToInt32(bytes, 0);
        activeModID = id_model;
        isModSwitched = true;
    }

    private void ReadRiskArray(ref byte[] bytes)
    {
        string riskPointsStr = Encoding.UTF8.GetString(bytes);
        activeFeedbackPoints = riskPointsStr.Split('|');
        isFeedback = true;
    }

    private void ThreadMethod()
    {
        while (true) {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint);
            string result = Encoding.UTF8.GetString(receiveBytes);
            // read arguments that should be directly sent after
            byte[] argBytes = udp.Receive(ref RemoteIpEndPoint);
            // according to the channel call the correct function
            if (result.Equals("/camera"))
                ReadActiveCamera(ref argBytes);
            else if (result.Equals("/model"))
                ReadActiveModel(ref argBytes);
            else if (result.Equals("/posture"))
                ReadMusclesArray(ref argBytes);
            else if (result.Equals("/risk"))
                ReadRiskArray(ref argBytes);
        }
    }
}

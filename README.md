# UnityAvatarControl

## Introduction

This repository contains Unity codes and C# scripts to animate a humanoid model with joint values sent as UDP messages.
This graphical interface is part of a bigger project that analyzes the risk of body postures using ergonomic techniques. 
Joints at risk are displayed with a red sphere as illustrated in the following picture.

## Usage

This application requires Unity 5.5.1 to run.
It can be compiled as windows executable or an android application.
At the moment, it cannot be compiled as a WebGL application as UDP socket are unavailable.
When launched, it waits for messages sent as UDP byte array on the port **5005**.
The following block of code is an example in python to update the joint values of the humanoid model.

```python
import socket
import struct

ip = your_ip # the ip of the machine where the Unity application is running
port = 5005

channel = 1 # Number of the channel to orient the message correctly (see below)
vect = joint_vector # put here is the joint values to send. It is waiting for an array of 92 floats.

packer = struct.Struct(('f' * len(vect))
sent_vect = [channel]
packed_data = packer.pack(*sent_vect)
packed_data += data
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)  # UDP
sock.sendto(packed_data, (ip, port))
```

Similarly you can also update the values of the joint spheres.
The color is determined by a float value ranging from 0 (green) to 1 (red).
Updating the color can be performed by sending an array of **8 floats** on channel **4**.
The spheres are in the following order:
**Spine**, **Neck**, **LeftArm**, **RightArm**, **LeftForeArm**, **RightForeArm**, **LeftHand**, **RightHand**.
Each name corresponds to the name of the transformation in the humanoid model.
In the file [RiskFeedback.cs](Assets/Scripts/RiskFeedback.cs) at line 12 you can also change the list to add more spheres or change their order

```C#
sphereNames = new List<string>() { "Spine", "Neck", "LeftArm", "RightArm", "LeftForeArm", "RightForeArm", "LeftFoot", "RightFoot"};
```

Given that the names are in the humanoid hiearchy this will automatically generate spheres on the corresponding joints.

## List of UDP channels

Here is the list of possible messages you can send by udp:
- **posture (channel 1)**: A float vector of size 92 with values ranging from [-pi, pi]
- **camera (channel 2)**: A single integer to change the camera view. Four views are available: front(0), back(1), front left(2), front right(3)
- **model (channel 3)**: A single integer to change the model from male(0) to female(1)
- **risk (channel 4)**: A float vector with values ranging from 0 to 1 to change the color of the joint spheres

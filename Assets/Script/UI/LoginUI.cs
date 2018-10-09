﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{

    
    public InputField Host;
    public InputField Room;
    public InputField ID;

    public InputField HTTPRequest;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateRoom()
    {
        StartCoroutine(HttpGet());
    }

    public void JoinRoom()
    {
        var addr = Host.text.Split(':');

        Network.Instance.client.Connect(addr[0],int.Parse(addr[1]));
        Network.Instance.client.Start();


        var b = pb.C2S_ConnectMsg.CreateBuilder();
        
        b.SetToken(string.Format("{0},{1}", Room.text, ID.text));
        Network.Instance.client.Send(PacketWraper.NewPacket(pb.ID.MSG_Connect, b.Build()));
    }

    IEnumerator HttpGet()
    {
        WWW getData = new WWW(HTTPRequest.text);
        yield return getData;
        if (getData.error != null)
        {
            Debug.Log(getData.error);
        }
        else
        {
            Debug.Log(getData.text);
        }

    }
}
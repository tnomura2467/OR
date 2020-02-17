using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;


public class NumPublish : MonoBehaviour {
    private RosSocket rosSocket;
    public int UpdateTime = 0;
    public int[] Frame;
    public int[] id;
    public int[] Xmin;
    public int[] Ymin;
    public int[] Width;
    public int[] Height;
    public int[] Depth;
    public int[] WhatNo;
    public int cnt;
    public GameObject Plane0;
    public GameObject Plane1;
    public GameObject Plane2;

    public GameObject[] Plane;

    public bool num_check_topic = false;
    void Start()
    {
        Plane = new GameObject[3];
        Plane[0] = Plane0;
        Plane[1] = Plane1;
        Plane[2] = Plane2;
        Invoke("Init", 1.0f);
    }

    public void Init()
    {
        rosSocket = GetComponent<RosConnector>().RosSocket;

        rosSocket.Subscribe("/shelfDB", "detect_object/DBinfo", NumRes, UpdateTime);

       
    }

    void NumRes(Message message)
    {
        DBinfo datas = (DBinfo)message;
        //Frame = datas.Frame;
        id = datas.id;
        Xmin = datas.Xmin;
        Ymin = datas.Ymin;
        Width = datas.Width;
        Height = datas.Height;
        Depth = datas.Depth;
        //WhatNo = datas.WhatNo;
        cnt = datas.cnt;


        num_check_topic = true;

    }
	
	// Update is called once per frame
	void Update () {
        if (num_check_topic)
        {
            PlaneChange();
        }

    }

    void PlaneChange()
    {
        for (int i = 0; i < cnt; i++)
        {
            int count;
            count = WhatNo[i];
            Plane[i].transform.localScale = new Vector3(Width[count] * 0.001f, 0.1f, Height[count] * 0.001f);
            Plane[i].transform.localPosition = new Vector3((Xmin[count] * 0.01f) - 2.4f + (Width[count] * 0.005f), (Ymin[count] * 0.01f * -1) + 1.35f - (Height[count] * 0.005f), 10f + Depth[count] * 0.00001f);
        }
        /*Plane2.transform.localScale = new Vector3(Width[2] * 0.001f, 0.1f, Height[2] * 0.001f);
        Plane2.transform.localPosition = new Vector3((Xmin[2] * 0.01f) - 2.4f + (Width[2] * 0.005f), (Ymin[2] * 0.01f * -1) + 1.35f - (Height[2] * 0.005f), 10f+Depth[2]*0.00001f);
        Plane3.transform.localScale = new Vector3(Width[5] * 0.001f, 0.1f, Height[5] * 0.001f);
        Plane3.transform.localPosition = new Vector3((Xmin[5] * 0.01f) - 2.4f + (Width[5] * 0.005f), (Ymin[5] * 0.01f * -1) + 1.35f - (Height[5] * 0.005f), 10f + Depth[5] * 0.00001f);*/

        num_check_topic = false;

    }

}

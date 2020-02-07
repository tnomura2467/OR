using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;


public class DBSubscribe : MonoBehaviour
{
    private RosSocket rosSocket;
    public int UpdateTime = 0;
    public int[] Frame;
    public int[] id;
    public int[] Xmin;
    public int[] Ymin;
    public int[] Width;
    public int[] Height;
    public int[] Depth;
    public int[] FrameTake;
    public int cnt;

    public int MaxFrame;
    public GameObject Plane0;
    public GameObject Plane1;
    public GameObject Plane2;
    public GameObject Plane3;
    public GameObject Plane4;
    public GameObject Plane5;

    public GameObject[] Plane;

    public GameObject slider;
    public float Slidery;

    public bool num_check_topic = false;
    void Start()
    {
        Plane = new GameObject[6];
        Plane[0] = Plane0;
        Plane[1] = Plane1;
        Plane[2] = Plane2;
        Plane[3] = Plane3;
        Plane[4] = Plane4;
        Plane[5] = Plane5;
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
        Frame = datas.Frame;
        id = datas.id;
        Xmin = datas.Xmin;
        Ymin = datas.Ymin;
        Width = datas.Width;
        Height = datas.Height;
        Depth = datas.Depth;
        FrameTake = datas.WhatNo;
        cnt = datas.cnt;

        MaxFrame = 0;
        num_check_topic = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (num_check_topic)
        {
            PlaneChange();
        }

        Slidery = slider.transform.localPosition.y;
        Slidery = Slidery - 1;
        Slidery = Slidery * (-1);
        if (Slidery > 2)
        {
            Slidery = 2;
        }
        if (Slidery <0)
        {
            Slidery = 0;
        }
        //Debug.Log(Slidery);
        Slidery = Slidery * MaxFrame/2f;
        
        for (int i = 0; i < cnt; i++)
        {
            if (FrameTake[i] == 0)
            {
                FrameTake[i] = MaxFrame;
            }

            if (Frame[i]<=Slidery&&Slidery<=FrameTake[i])
            {
                Plane[i].SetActive(true);
            }
            else
            {
                Plane[i].SetActive(false);
            }
        }

    }

    void PlaneChange()
    {
        for (int i = 0; i < cnt; i++)
        {   
            Plane[i].transform.localScale = new Vector3(Width[i] * 0.001f, 0.1f, Height[i] * 0.001f);
            Plane[i].transform.localPosition = new Vector3((Xmin[i] * 0.01f) - 2.4f + (Width[i] * 0.005f), (Ymin[i] * 0.01f * -1) + 1.35f - (Height[i] * 0.005f), 10f + Depth[i] * 0.00001f);

            MaxFrame = Frame[i];
            if (MaxFrame < FrameTake[i])
            {
                MaxFrame = FrameTake[i];
            }

            

        }

        num_check_topic = false;

    }

}

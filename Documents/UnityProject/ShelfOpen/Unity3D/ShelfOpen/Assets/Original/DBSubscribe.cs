using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;


public class DBSubscribe : MonoBehaviour
{
    private RosSocket rosSocket;
    public int UpdateTime = 0;
    public int[] FrameB;
    public int[] FrameT;
    public int[] TimeB;
    public int[] TimeT;
    public int[] id;
    public int[] Xmin;
    public int[] Ymin;
    public int[] Width;
    public int[] Height;
    public int[] Depth;
    public int[] Yobi;
    public int[] YobiYobi;
    public int cnt;

    public int MinTime;
    public int MaxTime;
    public int MinSec;
    public int MaxSec;
    public int[] BSec;
    public int[] TSec;

    public struct BeginTime
    {
        public int hour;
        public int min;
        public int sec;
    }
    public struct EndTime
    {
        public int hour;
        public int min;
        public int sec;
    }
    BeginTime bt = new BeginTime { hour = 0, min = 0, sec = 0 };
    EndTime et = new EndTime { hour = 0, min = 0, sec = 0 };

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
        BSec = new int[20];
        TSec = new int[20];

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
        FrameB = datas.FrameB;
        FrameT = datas.FrameT;
        TimeB = datas.TimeB;
        TimeT = datas.TimeT;
        id = datas.id;
        Xmin = datas.Xmin;
        Ymin = datas.Ymin;
        Width = datas.Width;
        Height = datas.Height;
        Depth = datas.Depth;
        Yobi = datas.Yobi;
        YobiYobi = datas.YobiYobi;
        cnt = datas.cnt;

        MaxTime = 0;
        MinTime = 0;



        num_check_topic = true;

    }

    public int ChangeTime(int time)
    {
        int ctime;
        int hour;
        int min;
        int sec;
        hour = time / 10000;
        min = time / 100 - (hour * 100);
        sec = time - (hour * 10000) - (min * 100);

        ctime = hour * 3600 + min * 60 + sec;

        return ctime;
    }

    // Update is called once per frame
    void Update()
    {
        if (num_check_topic)
        {
            PlaneChange();
        }


        MaxSec = ChangeTime(MaxTime);
        MinSec = ChangeTime(MinTime);

        Slidery = slider.transform.localPosition.y;
        Slidery = Slidery - 1;
        Slidery = Slidery * (-1);
        if (Slidery > 2)
        {
            Slidery = 2;
        }
        if (Slidery < 0)
        {
            Slidery = 0;
        }
 
        Slidery = Slidery * (MaxSec - MinSec) / 2f;

        for (int i = 0; i < cnt; i++)
        {

            BSec[i] = ChangeTime(TimeB[i]);
            TSec[i] = ChangeTime(TimeT[i]);
            Debug.Log(BSec[i]);
            Debug.Log(TSec[i]);

            if (TSec[i] == 0)
            {
                TSec[i] = MaxSec;
            }

            

            if ((BSec[i] - MinSec) <= Slidery && Slidery <= (TSec[i] - MinSec))
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


            MinTime = TimeB[0]; //Beginning is bringed first objects 
            MaxTime = TimeB[i];
            if (MaxTime < TimeT[i])
            {
                MaxTime = TimeT[i];
            }
      


        }


        bt.hour = MinTime / 10000;
        bt.min = (MinTime / 100) - (bt.hour * 100);
        bt.sec = MinTime - (bt.hour * 10000) - (bt.min * 100);

        et.hour = MaxTime / 10000;
        et.min = (MaxTime / 100) - (et.hour * 100);
        et.sec = MaxTime - (et.hour * 10000) - (et.min * 100);



        num_check_topic = false;

    }

}

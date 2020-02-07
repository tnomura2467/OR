using RosSharp.RosBridgeClient;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Runtime.InteropServices;
//HSRのナビゲーションゴールを表示するスクリプト

public class ByteImagePublish : MonoBehaviour
{

    public string FrameId = "Camera";
    public int resolutionWidth = 1300;//2048;
    public int resolutionHeight = 780;//1152;
    [Range(0, 100)]
    public int qualityLevel = 50;
    //private Messages.Sensor.CompressedImage message;
    private Texture2D texture2D;
    private Rect rect;

    private SensorCompressedImage message;
    private RosSocket rosSocket;
    private string advertise_id;
    int width = 1300;//2048;
    int height = 780;//1152;
    int fps = 30;
    Texture2D texture;
    WebCamTexture webcamTexture;
    Color32[] colors = null;
    //public GameObject score_object;
    private int i;
    Text score_text;
    IEnumerator Init()
    {
        while (true)
        {
            if (webcamTexture.width > 16 && webcamTexture.height > 16)
            {
                colors = new Color32[webcamTexture.width * webcamTexture.height];
                texture = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.RGB24, false);
                //GetComponent<Renderer>().material.mainTexture = texture;
                break;
            }
            yield return null;
        }
    }
    // Use this for initialization
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(devices[0].name, this.width, this.height, this.fps);
        webcamTexture.Play();
        //score_text = score_object.GetComponent<Text>();
        i = 0;
        StartCoroutine(Init());


        rosSocket = GetComponent<RosConnector>().RosSocket;
        advertise_id = rosSocket.Advertise("/img_com/compressed", "sensor_msgs/CompressedImage");

        InitializeMessage();



    }



    void Update()
    {
        if (colors != null)
        {
            webcamTexture.GetPixels32(colors);
            //Color32ArrayToByteArray(colors);


            message.header.Update();

            message.data = texture.EncodeToJPG(qualityLevel);

            rosSocket.Publish(advertise_id, message);

            i = i + 1;
           // score_text.text = "count_:" + i;
            texture.SetPixels32(colors);
            //texture.Apply();
        }
    }
    private static byte[] Color32ArrayToByteArray(Color32[] colors)
    {
        if (colors == null || colors.Length == 0)
            return null;
        int lengthOfColor32 = Marshal.SizeOf(typeof(Color32));
        int length = lengthOfColor32 * colors.Length;
        byte[] bytes = new byte[length];
        GCHandle handle = default(GCHandle);
        try
        {
            handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();
            Marshal.Copy(ptr, bytes, 0, length);
        }
        finally
        {
            if (handle != default(GCHandle))
                handle.Free();
        }


        return bytes;
    }


    private void InitializeMessage()
    {
        message = new SensorCompressedImage();
        message.header.frame_id = FrameId;
        message.format = "jpeg";
    }

}
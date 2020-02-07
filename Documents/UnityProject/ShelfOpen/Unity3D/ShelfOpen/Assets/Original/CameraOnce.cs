using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnce : MonoBehaviour
{



    public bool cameraofff = false;
    public GameObject imagetarget;
    DefaultTrackableEventHandler DTE;
    //VuforiaBehaviour vb;
    public GameObject MC;

    private int i = 0;
    // Use this for initialization
    void Start()
    {

        DTE = imagetarget.GetComponent<DefaultTrackableEventHandler>();
        //MC.GetComponent<Vuforia>();

    }

    // Update is called once per frame
    void Update()
    {
        if (i == 0)
        {
            cameraofff = DTE.once;
        }
        if (cameraofff == true)
        {
            if (i == 0)
            {
               
                //this.transform.localEulerAngles = new Vector3(0, 0, 0);
                i++;
            }
        }
    }
}

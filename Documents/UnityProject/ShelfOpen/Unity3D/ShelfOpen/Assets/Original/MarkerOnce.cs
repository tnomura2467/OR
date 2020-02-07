using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerOnce : MonoBehaviour
{



    public bool cameraoff = false;
    public GameObject imagetarget;
    DefaultTrackableEventHandler DTE;
    //public GameObject PlaneOri;

    private int i = 0;
    // Use this for initialization
    void Start()
    {

        DTE = imagetarget.GetComponent<DefaultTrackableEventHandler>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (i == 0)
        {
            cameraoff = DTE.once;
        }
        if (cameraoff == true)
        {
            if (i == 0)
            {
                this.gameObject.transform.parent = null;
                //this.transform.localEulerAngles = new Vector3(0, 0, 0);
                //this.transform.localPosition  = new Vector3(0, 0, 0);
                //PlaneOri.transform.localEulerAngles = new Vector3(0, 0, 0);
                i++;
            }
        }
    }
}

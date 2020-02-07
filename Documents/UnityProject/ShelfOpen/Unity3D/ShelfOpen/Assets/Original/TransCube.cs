using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransCube : MonoBehaviour
{


    public Vector3 nowpos;
    public Vector3 nowlot;
    private float nowy;


    // Use this for initialization
    void Start()
    {
        nowpos = new Vector3(0,0,0);
        nowlot = new Vector3(0, 0, -90);
    }

    // Update is called once per frame
    void Update()
    {
        nowpos = this.transform.localPosition;
        nowy = nowpos.y;
        if (nowpos.y<-1)
        {
            nowy = -1.0f;
        }
        if (nowpos.y >1)
        {
            nowy = 1.0f;
        }

        this.transform.localPosition = new Vector3(0f, nowy, 0f);
        this.transform.localEulerAngles = nowlot;
    }
}

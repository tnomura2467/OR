using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxParent : MonoBehaviour {
    public GameObject CameraObject;

    private float CamDevi_x;
    private float CamDevi_y;
    private float CamDevi_z;
    public float CamRota_x;
    public float CamRota_y;
    public float CamRota_z;

    private Quaternion quaternion;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



        CamDevi_x = CameraObject.transform.position.x;    //メインカメラのx,y,z座標取得

        CamDevi_y = CameraObject.transform.position.y;

        CamDevi_z = CameraObject.transform.position.z;

        this.transform.localPosition = new Vector3(CamDevi_x, CamDevi_y, CamDevi_z);

        quaternion = CameraObject.transform.rotation;

        CamRota_x = quaternion.eulerAngles.x; //メインカメラのx,y,z座標取得

        CamRota_y = quaternion.eulerAngles.y;

        CamRota_z = quaternion.eulerAngles.z;



        //Debug.Log(CamRota_x);

        this.transform.rotation = Quaternion.Euler(CamRota_x, CamRota_y, CamRota_z);

    }
}

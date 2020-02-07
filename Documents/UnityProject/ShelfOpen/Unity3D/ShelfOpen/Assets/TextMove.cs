using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour {
    private float CamRota_x;
    private float CamRota_y;
    private float CamRota_z;
    MoveBoxParent moves;

    // Use this for initialization
    void Start () {
        moves = gameObject.GetComponent<MoveBoxParent>();
    }
	
	// Update is called once per frame
	void Update () {
 
        this.transform.rotation = Quaternion.Euler(moves.CamRota_x, moves.CamRota_y, moves.CamRota_z);
    }
}

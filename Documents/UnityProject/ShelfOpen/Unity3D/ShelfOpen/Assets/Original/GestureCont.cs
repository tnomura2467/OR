/*using HoloToolkit.Unity.InputModule;
using UnityEngine;
#if !UNITY_2017_2_OR_NEWER
using UnityEngine.VR.WSA.Input;
#else
using UnityEngine.XR.WSA.Input;
#endif
public class GestureController : MonoBehaviour
{

    private Vector3 prevPos;
    private bool isHold;
    private GameObject focusObj;

    // Use this for initialization
    void Start()
    {
        InteractionManager.InteractionSourcePressed += InteractionManager_SourcePressed;
        InteractionManager.InteractionSourceReleased += InteractionManager_SourceReleased;
        InteractionManager.InteractionSourceLost += InteractionManager_SourceLost;

        InteractionManager.InteractionSourceUpdated += InteractionManager_SourceUpdated;
    }

    // Update is called once per frame
    void Update()
    {
        var obj = GazeManager.Instance.HitObject;

        // ホールドしている時は、オブジェクト入れ替えない
        if (obj != null && !isHold)
        {
            // TagがInteractionのものだけを対象とする
            if (obj.tag == "Interaction")
            {
                focusObj = obj;
            }
        }
    }

    void InteractionManager_SourcePressed(InteractionSourcePressedEventArgs state)
    {
        if (focusObj == null) return;

        focusObj.GetComponent<Rigidbody>().useGravity = false;

        Vector3 handPosition;
        if (state.source.kind == InteractionSourceKind.Hand &&
            state.properties.location.TryGetPosition(out handPosition))
        {
            isHold = true;
            prevPos = handPosition;
        }
    }

   void InteractionManager_SourceReleased(InteractionSourceReleasedEventArgs state)
    {
        if (focusObj == null) return;

        focusObj.GetComponent<Rigidbody>().useGravity = true;
        isHold = false;
        focusObj = null;
    }

     void InteractionManager_SourceLost(InteractionSourceLostEventArgs state)
    {
        if (focusObj == null) return;

        focusObj.GetComponent<Rigidbody>().useGravity = true;
        isHold = false;
        focusObj = null;
    }

    void InteractionManager_SourceUpdated(InteractionSourceUpdatedEventArgs state)
    {
        if (!isHold || focusObj == null) return;

        Vector3 handPosition;
        //state.properties.location.TryGetPosition(out handPosition);

        if (state.source.kind == InteractionSourceKind.Hand &&
            state.properties.location.TryGetPosition(out handPosition))
        {
            var moveVector = Vector3.zero;
            moveVector = handPosition - prevPos;

            prevPos = handPosition;

            var handDistance = Vector3.Distance(Camera.main.transform.position, handPosition);
            var objectDistance = Vector3.Distance(Camera.main.transform.position, focusObj.transform.position);

            focusObj.transform.position += (moveVector * (objectDistance / handDistance));
        }
    }
}*/
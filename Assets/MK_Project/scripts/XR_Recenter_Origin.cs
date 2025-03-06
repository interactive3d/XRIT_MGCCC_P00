using UnityEngine;
using UnityEngine.XR.Interaction;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;

public class XR_Recenter_Origin : MonoBehaviour
{
    public InputActionProperty recenterButton;
    public Transform head;
    public Transform origin;
    public Transform target;


    private void Update() {
        if (recenterButton.action.WasPressedThisFrame()) {
            Recenter();
        }
    }


    public void Recenter() {

        /*
        XROrigin xrOrigin = GetComponent<XROrigin>();
        xrOrigin.MoveCameraToWorldLocation(target.position);
        xrOrigin.MatchOriginUpCameraForward(target.up, target.forward);
*/        
        Vector3 myOffset = head.position - origin.position;
        myOffset.y = 0;
        origin.position = target.position - myOffset;

        Vector3 targetForward = target.forward;
        targetForward.y = 0;
        Vector3 cameraForward = head.forward;
        cameraForward.y = 0;

        float angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);
        origin.RotateAround(head.position, Vector3.up, angle);
        

    }
}

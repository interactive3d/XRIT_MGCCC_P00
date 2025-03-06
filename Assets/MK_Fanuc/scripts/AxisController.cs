using UnityEngine;

[System.Serializable]
public class RobotAAxis {
    public GameObject axisObject; // The GameObject representing the axis
    public Vector3 freedomDirection = Vector3.up; // Default rotation axis (Y-axis)
    public float minRotation = -90f; // Minimum rotation limit
    public float maxRotation = 90f;  // Maximum rotation limit
    public float rotationSpeed = 50f; // Rotation speed

    private float currentRotation = 0f; // Internal tracking of rotation

    public void Rotate(float input) {
        if (axisObject == null) return;

        float rotationAmount = input * rotationSpeed * Time.deltaTime;
        float newRotation = Mathf.Clamp(currentRotation + rotationAmount, minRotation, maxRotation);

        float deltaRotation = newRotation - currentRotation;
        axisObject.transform.Rotate(freedomDirection, deltaRotation, Space.Self);

        currentRotation = newRotation;
    }
}

public class AxisController : MonoBehaviour {
    public RobotAxis[] axes = new RobotAxis[6]; // Array to hold the six axes

    void Update() {
        // Example controls using keyboard (extendable for joystick/controller input)
        if (Input.GetKey(KeyCode.Alpha1)) axes[0].Rotate(1f);
        if (Input.GetKey(KeyCode.Q)) axes[0].Rotate(-1f);

        if (Input.GetKey(KeyCode.Alpha2)) axes[1].Rotate(1f);
        if (Input.GetKey(KeyCode.W)) axes[1].Rotate(-1f);

        if (Input.GetKey(KeyCode.Alpha3)) axes[2].Rotate(1f);
        if (Input.GetKey(KeyCode.E)) axes[2].Rotate(-1f);

        if (Input.GetKey(KeyCode.Alpha4)) axes[3].Rotate(1f);
        if (Input.GetKey(KeyCode.R)) axes[3].Rotate(-1f);

        if (Input.GetKey(KeyCode.Alpha5)) axes[4].Rotate(1f);
        if (Input.GetKey(KeyCode.T)) axes[4].Rotate(-1f);

        if (Input.GetKey(KeyCode.Alpha6)) axes[5].Rotate(1f);
        if (Input.GetKey(KeyCode.Y)) axes[5].Rotate(-1f);
    }
}

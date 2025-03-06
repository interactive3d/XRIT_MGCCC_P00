using UnityEngine;


[System.Serializable]
public class RobotAxis {
    public GameObject axisObject; // The GameObject representing the axis
    public Vector3 freedomDirection = Vector3.up; // Default rotation axis (Y-axis)
    public float minRotation = -90f; // Minimum rotation limit
    public float maxRotation = 90f;  // Maximum rotation limit
    public float rotationSpeed = 50f; // Rotation speed
    public float length = 1f; // Length of the arm segment

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

public class AdvancedAxisController : MonoBehaviour {
    public RobotAxis[] axes = new RobotAxis[6]; // Array to hold the six axes
    public Transform targetPosition; // The desired target for movement
    public float movementSpeed = 1f; // Speed of movement towards the target

    private Vector3[] jointPositions; // Stores the positions of each joint

    void Start() {
        jointPositions = new Vector3[axes.Length + 1]; // 6 axes + end effector
    }

    void Update() {
        HandleManualControl();
        MoveToTarget();
    }

    void HandleManualControl() {
        // Example manual control using keyboard
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

    void MoveToTarget() {
        if (targetPosition == null) return;

        ComputeForwardKinematics(); // Compute positions of joints

        Vector3 endEffectorPosition = jointPositions[jointPositions.Length - 1];
        Vector3 direction = (targetPosition.position - endEffectorPosition).normalized;

        // Move each axis in small steps towards the target
        for (int i = 0; i < axes.Length; i++) {
            float rotationStep = movementSpeed * Time.deltaTime;
            axes[i].Rotate(Vector3.Dot(direction, axes[i].freedomDirection) * rotationStep);
        }
    }

    void ComputeForwardKinematics() {
        jointPositions[0] = axes[0].axisObject.transform.position;

        for (int i = 1; i < jointPositions.Length; i++) {
            RobotAxis prevAxis = axes[i - 1];
            jointPositions[i] = jointPositions[i - 1] + prevAxis.axisObject.transform.forward * prevAxis.length;
        }
    }
}


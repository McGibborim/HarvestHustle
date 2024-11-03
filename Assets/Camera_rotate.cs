using UnityEngine;

public class MenuCameraSpin : MonoBehaviour
{
    public Transform target; // The object around which the camera will spin
    public float spinSpeed = 10f; // Speed of the camera spin

    void Update()
    {
        // Rotate the camera around the target object
        transform.RotateAround(target.position, Vector3.up, spinSpeed * Time.deltaTime);
    }
}

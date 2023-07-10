using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room camera
    [SerializeField] private float speed; // Speed at which the camera moves horizontally
    private float currentPosX; // Current X position of the camera
    private Vector3 velocity = Vector3.zero; // Velocity used for smooth damping

    // Follow player
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private float aheadDistance; // Distance ahead of the player to move the camera
    [SerializeField] private float cameraSpeed; // Speed at which the camera follows the player
    private float lookAhead; // Current look-ahead distance of the camera

    private void Update()
    {
        // Room camera
        // Move the camera horizontally smoothly using SmoothDamp
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        // Follow player
        // Set the camera position to be slightly ahead of the player on the X-axis
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);

        // Calculate the new look-ahead distance based on the player's scale (direction)
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }
}

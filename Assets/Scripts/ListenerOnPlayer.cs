using UnityEngine;

public class ListenerOnPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraTransform;

    void LateUpdate()
    {
        if (player == null) return;

        // Follow the ball's position without inheriting its rolling rotation
        transform.position = player.position;

        // Match the camera's facing direction so left/right stereo matches the screen
        if (cameraTransform != null)
            transform.rotation = cameraTransform.rotation;
    }
}
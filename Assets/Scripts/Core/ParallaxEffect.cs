using UnityEngine;

public class InfiniteParallax : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("1.0 = Moves with Camera (Skybox), 0.0 = Static World Object")]
    public float parallaxFactor; 
    
    [Tooltip("Check this if you want the background to stay fixed vertically.")]
    public bool lockY = false; 

    private Transform cam;
    private Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        
        lastCamPos = cam.position; 
    }

    void LateUpdate()
    {
        if (Vector3.Distance(cam.position, lastCamPos) > 5f)
        {
            lastCamPos = cam.position;
            return;
        }

        Vector3 deltaMovement = cam.position - lastCamPos;

        float moveX = deltaMovement.x * parallaxFactor;
        float moveY = deltaMovement.y * parallaxFactor;

        transform.position += new Vector3(moveX, lockY ? 0 : moveY, 0);

        lastCamPos = cam.position;
    }
}
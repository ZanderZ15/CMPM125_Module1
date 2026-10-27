using UnityEngine;

public class RegisterCamera : MonoBehaviour
{
    void Start()
    {
        // Find the FOV script anywhere in the active scene
        fovController fovScript = Object.FindAnyObjectByType<fovController>();
        
        if (fovScript != null)
        {
            fovScript.AssignCamera(GetComponent<Camera>());
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class fovController : MonoBehaviour
{
    private Camera playerCam;
    public void Start() 
    {}
    public void SetFOV(float fov)
    {
        EventBus.Instance.OnFOVChange(fov*120f);
        playerCam.fieldOfView = GameManager.Instance.getFOV();
        
    }
    public void AssignCamera(Camera cameraToUse)
    {
        playerCam = cameraToUse;
    }
}
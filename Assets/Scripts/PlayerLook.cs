using Unity.Microsoft.GDK;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    private InputAction lookAction;
    private Vector2 mouseDelta;
    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouseDelta = lookAction.ReadValue<Vector2>();
        this.gameObject.transform.Rotate(0, mouseDelta.x/ 10, 0);
        camera.transform.Rotate(-mouseDelta.y/10, 0, 0);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    private InputAction lookAction;
    private Vector2 mouseDelta;
    private bool ingame;
    void Start()
    {
        EventBus.Instance.INGAME += Playing;
        EventBus.Instance.MENU += Idle;
        EventBus.Instance.GameEnd += Idle;
    }
    void Playing()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ingame = true;
    }
    void Idle()
    {
        lookAction = null;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ingame = false;
    }
    void OnDisable()
    {
        EventBus.Instance.INGAME -= Playing;
        EventBus.Instance.MENU -= Idle;
        EventBus.Instance.GameEnd -= Idle;
    }

    void Update()
    {
        if (ingame)
        {
            mouseDelta = lookAction.ReadValue<Vector2>();
            this.gameObject.transform.Rotate(0, mouseDelta.x/ 10, 0);
            camera.transform.Rotate(-mouseDelta.y/20, 0, 0);
        }
    }
}

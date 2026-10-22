using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    //Remember CanvasGroups
    [SerializeField] private Button play_button;

    void OnEnable()
    {
        play_button.onClick.AddListener(OnPlayButtonClicked);
    }

    void OnPlayButtonClicked()
    {
        Debug.Log("Pressed!");
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class MenuController : MonoBehaviour
{

    [SerializeField] private Button play_button;
    [SerializeField] private Button options_button;
    [SerializeField] private Button credits_button;
    [SerializeField] private Button[] back_buttons;
    [SerializeField] private Canvas[] screens;
    private Canvas current_screen;
    private AsyncOperation loadOperation;

    void OnEnable()
    {
        play_button.onClick.AddListener(OnPlayButtonClicked);
        options_button.onClick.AddListener(OnOptionsButtonClicked);
        credits_button.onClick.AddListener(OnCreditsButtonClicked);
        int c = 0;
        foreach (Canvas canvas in screens)
        {

            canvas.transform.localPosition = Vector3.zero;
            if(c > 0){canvas.enabled = false;}
            c++;
        }
        
    }
    void OnDisable()
    {
        play_button.onClick.RemoveAllListeners();
        options_button.onClick.RemoveAllListeners();
        credits_button.onClick.RemoveAllListeners();
    }

    private void LoadScene(string scenename)
    {
        Debug.Log("sceneName to load: " + scenename);
        SceneManager.LoadSceneAsync(scenename);
    }
    void OnPlayButtonClicked()
    {
        LoadScene("RLScene");
    }
    void OnOptionsButtonClicked()
    {
        //Remember CanvasGroups
        screens[0].enabled = false;
        screens[1].enabled = true;
        current_screen = screens[1];
        back_buttons[0].onClick.AddListener(OnBackButtonClicked);
    }
    void OnCreditsButtonClicked()
    {
        screens[0].enabled = false;
        screens[2].enabled = true;
        current_screen = screens[2];
        back_buttons[1].onClick.AddListener(OnBackButtonClicked);
    }
    void OnBackButtonClicked()
    {
        screens[0].enabled = true;
        current_screen.enabled = false;
    } 

}

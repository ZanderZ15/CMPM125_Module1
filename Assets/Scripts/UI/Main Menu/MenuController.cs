using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MenuController : MonoBehaviour
{

    [SerializeField] private Button play_button;
    [SerializeField] private Button options_button;
    [SerializeField] private Button credits_button;
    [SerializeField] private Button[] back_buttons;
    [SerializeField] private Canvas[] screens;
    [SerializeField] private GameObject[] sliders;
    [SerializeField] private CanvasGroup pausegroup;
    //[SerializeField] private EventSystem EventSystem;
    private Canvas current_screen;
    private AsyncOperation loadOperation;

    void Start()
    {
        EventBus.Instance.StateChange += StateChange;
        EventBus.Instance.GameEnd += GameOverScreen;
    }
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
            c++; //c++ mention!!!
        }
        
    }
    void OnDisable()
    {
        play_button.onClick.RemoveAllListeners();
        options_button.onClick.RemoveAllListeners();
        credits_button.onClick.RemoveAllListeners();
    }
    void OnPlayButtonClicked()
    {
        EventSystem.current.SetSelectedGameObject(null);

        screens[0].enabled = false;
        EventBus.Instance.OnPlayGame();
        //
    }
    void OnOptionsButtonClicked()
    {
        //Remember CanvasGroups; 
        // i made this note but honestly I dont remember what to do or how to use it here lmao
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

    void StateChange()
    {
        switch (GameManager.Instance.state)
        {
            case GameManager.GameState.INGAME:
            Resume();
            break;
            case GameManager.GameState.MENU:
            Pause();
            break;
        }
    }
    void Pause()
    {
        //TODO
        //bring menus back
        EventBus.Instance.OnMenuOpen();
        screens[3].enabled = true;
        UpdateSliders();
        pausegroup.interactable = true;

    }
    void Resume()
    {
        //Banish the menus
        EventSystem.current.SetSelectedGameObject(null);
        EventBus.Instance.GamePlaying();
        pausegroup.interactable = false;
        screens[3].enabled = false;
        
    }

    private void UpdateSliders()
    {
        sliders[0].GetComponent<Slider>().value = GameManager.Instance.getAudio();
        sliders[1].GetComponent<Slider>().value = GameManager.Instance.getFOV()/120f;
    }

    private void GameOverScreen()
    {
        screens[4].enabled = true;
        //TODO: need to do cleanup eventually
    }
    public void OkayButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If running as a standalone build
        Application.Quit();
        #endif
    }

}

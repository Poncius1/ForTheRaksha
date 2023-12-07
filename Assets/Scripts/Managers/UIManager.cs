using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject PausePanel;
    public GameObject OptionsPanel;
    public GameObject PlayPanel;
    public GameObject GameOverPanel;
    public GameObject CollectableIU;
    public GameObject audioPanel;
    public GameObject ControlsPanel;
    public Image Image;
    public Sprite keyboardImage;
    public Sprite controlImage;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowPausePanel()
    {
        ShowPanel(PausePanel);

       
    }

    public void ShowOptionsPanel()
    {
        ShowPanel(OptionsPanel);
    }

    public void ShowAudio()
    {
        ShowPanel(audioPanel);

    }
    public void ShowPlayPanel()
    {
        
        ShowPanel(PlayPanel);   
    }

    public void ShowGameOverPanel()
    {
        ShowPanel(GameOverPanel);
    }
    public void ShowControlsPanel()
    {
        ShowPanel(ControlsPanel);
    }
    public void ShowCollectablePanel()
    {
        ShowPanel(CollectableIU);
        Cursor.lockState= CursorLockMode.None;
    }


    private void ShowPanel(GameObject panel)
    {
        HideAllPanels();
        panel.SetActive(true);
        
    }

    private void HideAllPanels()
    {
        PausePanel.SetActive(false);
        OptionsPanel.SetActive(false);
        PlayPanel.SetActive(false);
        CollectableIU.SetActive(false);
        GameOverPanel.SetActive(false);
        audioPanel.SetActive(false);
        ControlsPanel.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.ChangeGameState(GameManager.GameState.Playing);
    }

    public void Keyboard()
    {
        Image.sprite = keyboardImage;
    }
    public void Joystick()
    {
        Image.sprite = controlImage;
    }
    public void ExitGame()
    {
        
        Application.Quit();
    }
}

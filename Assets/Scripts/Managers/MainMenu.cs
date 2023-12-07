using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject MenuPanel;
    public GameObject creditsPanel;
    public GameObject audioPanel;
    public GameObject ControlsPanel;
    public Image Image;
    public Sprite keyboardImage;
    public Sprite controlImage;




    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitGame()
    {

        Application.Quit();
    }

    public void OptionsMenu()
    {
        MenuPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }

    public void BackOptions()
    {
        creditsPanel.SetActive(false);
        OptionsPanel.SetActive(true);
        MenuPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        audioPanel.SetActive(false);
    }
    public void BackMenu()
    {
        creditsPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        MenuPanel.SetActive(true);
        ControlsPanel.SetActive(false);
        audioPanel.SetActive(false);
    }
    public void Credits()
    {
        ControlsPanel.SetActive(false);
        audioPanel.SetActive(false);
        creditsPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        MenuPanel.SetActive(false);
    }
    public void Controls()
    {
        ControlsPanel.SetActive(true);
        audioPanel.SetActive(false);
        OptionsPanel.SetActive(false);
        MenuPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }


    public void Keyboard()
    {
        Image.sprite = keyboardImage;
    }
    public void Joystick()
    {
        Image.sprite = controlImage;
    }

    public void Audio()
    {
        audioPanel.SetActive(true);
        OptionsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        MenuPanel.SetActive(false);
        creditsPanel.SetActive(false);

    }

}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ComputerUIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject computerUI;
    public GameObject tutorialPopup;
    public GameObject topBarPanel;

    [Header("Player")]
    public GameObject playerController;

    private bool isOpen = false;

    void Start()
    {
        computerUI.SetActive(false);
        tutorialPopup.SetActive(false);
        topBarPanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isOpen)
            {
                CloseComputer();
            }
            else
            {
                OpenComputer();
            }
        }
    }

    public void OpenComputer()
    {
        isOpen = true;

        computerUI.SetActive(true);

        // Desativa o player
        if (playerController != null)
        {
            playerController.SetActive(false);
        }
    }

    public void CloseComputer()
    {
        isOpen = false;

        tutorialPopup.SetActive(false);
        topBarPanel.SetActive(false);

        computerUI.SetActive(false);

        // Reativa o player
        if (playerController != null)
        {
            playerController.SetActive(true);
        }
    }

    public void ToggleTutorial()
    {
        tutorialPopup.SetActive(!tutorialPopup.activeSelf);
    }

    public void CloseTutorial()
    {
        tutorialPopup.SetActive(false);
    }

    public void ToggleTopBar()
    {
        topBarPanel.SetActive(!topBarPanel.activeSelf);
    }

    public void CloseTopBar()
    {
        topBarPanel.SetActive(false);
    }

    public void BackToGame()
    {
        CloseComputer();
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
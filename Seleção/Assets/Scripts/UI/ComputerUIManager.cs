using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ComputerUIManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject computerUI;
    public GameObject tutorialPopup;
    public GameObject topBarPanel;
    public GameObject OptionPanel;
    public GameObject EmailPanel;
    public GameObject MensagemPanel;

    [Header("Player")]
    public GameObject playerController;

    [Header("Foco Gamepad")]
    public FocoMenuGamepad focoTelaIcones; // Tutorial + Setinha (tela inicial)
    public FocoMenuGamepad focoMenuBar;    // Voltar / Opções / Menu (dentro da barra)
    public FocoGamepadFora focoForaJogo;   // o foco do papel na mesa (do jogo, fora do computador)

    private bool isOpen = false;

    void Start()
    {
        computerUI.SetActive(false);
        tutorialPopup.SetActive(false);
        topBarPanel.SetActive(false);
        OptionPanel.SetActive(false);
        EmailPanel.SetActive(false);
        MensagemPanel.SetActive(false);
    }

    void Update()
    {
        bool pediuAbrirFechar = Keyboard.current.escapeKey.wasPressedThisFrame ||
                                 (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame);

        if (pediuAbrirFechar)
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

        if (playerController != null)
        {
            playerController.SetActive(false);
        }

        // desliga o foco do jogo (papel na mesa) enquanto o computador tá aberto
        if (focoForaJogo != null)
            focoForaJogo.enabled = false;
    }

    public void CloseComputer()
    {
        isOpen = false;

        tutorialPopup.SetActive(false);
        topBarPanel.SetActive(false);

        computerUI.SetActive(false);

        if (playerController != null)
        {
            playerController.SetActive(true);
        }

        // religa o foco do jogo quando o computador fecha
        if (focoForaJogo != null)
            focoForaJogo.enabled = true;
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

        // desliga o foco dos ícones (Tutorial/Setinha) enquanto a barra estiver aberta
        if (focoTelaIcones != null)
            focoTelaIcones.enabled = !topBarPanel.activeSelf;
    }

    public void CloseTopBar()
    {
        topBarPanel.SetActive(false);

        if (focoTelaIcones != null)
            focoTelaIcones.enabled = true;
    }

    public void ToggleOptions()
    {
        OptionPanel.SetActive(!OptionPanel.activeSelf);

        if (focoMenuBar != null)
            focoMenuBar.enabled = !OptionPanel.activeSelf;
    }

    public void CloseOptions()
    {
        OptionPanel.SetActive(false);

        if (focoMenuBar != null)
            focoMenuBar.enabled = true;
    }

    public void ToggleEmail()
    {
        EmailPanel.SetActive(!EmailPanel.activeSelf);
        if (focoMenuBar != null)
            focoMenuBar.enabled = !EmailPanel.activeSelf;
    }

    public void CloseEmail()
    {
        EmailPanel.SetActive(false);
        if (focoMenuBar != null)
            focoMenuBar.enabled = true;
    }

    public void ToggleMensagem()
    {
        MensagemPanel.SetActive(!MensagemPanel.activeSelf);
        if (focoMenuBar != null)
            focoMenuBar.enabled = !MensagemPanel.activeSelf;
    }
    public void CloseMensagem()
    {
        MensagemPanel.SetActive(false);
        if (focoMenuBar != null)
            focoMenuBar.enabled = true;
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
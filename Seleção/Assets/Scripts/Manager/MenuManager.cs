using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject OptionsPanel;

    [Header("Foco Gamepad")]
    public FocoMenuGamepad focoMenuGamepad; // arraste o FocoMenuGamepad do menu principal aqui

    public void Start()
    {
        Time.timeScale = 1f;
        OptionsPanel.SetActive(false);
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Cutscenes 1"); // muda pro nome exato da sua cena de cutscene
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void ToggleOptions()
    {
        OptionsPanel.SetActive(!OptionsPanel.activeSelf);

        if (OptionsPanel.activeSelf)
        {
            Canvas.ForceUpdateCanvases(); // força recalcular o layout assim que o painel abre
        }

        // desliga o foco do menu principal enquanto as opções estão abertas
        if (focoMenuGamepad != null)
            focoMenuGamepad.enabled = !OptionsPanel.activeSelf;
    }

    public void CloseOptions()
    {
        OptionsPanel.SetActive(false);

        if (focoMenuGamepad != null)
            focoMenuGamepad.enabled = true;
    }
}
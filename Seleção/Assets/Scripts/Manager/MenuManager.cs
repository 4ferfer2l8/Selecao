using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject OptionsPanel;

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
    }

    public void CloseOptions()
    {
        OptionsPanel.SetActive(false);
    }
}
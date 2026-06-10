using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject OptionsPanel;

    public void Start()
    {
        OptionsPanel.SetActive(false);
    }
    public void Jogar()
    {
        SceneManager.LoadScene("SampleScene");
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

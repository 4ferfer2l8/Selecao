using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject tutorialPanel;

    public void Start()
    {
    }
    public void Jogar()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Sair()
    {

        Application.Quit();
    }

}

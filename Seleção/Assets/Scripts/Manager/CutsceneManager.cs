using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CutsceneManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += FinalizarCutscene;
    }

    void Update()
    {
        bool pediuPular = Keyboard.current.escapeKey.wasPressedThisFrame ||
                           (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame); // B

        if (pediuPular)
        {
            SceneManager.LoadScene("SampleScene"); // nome exato da cena do jogo
        }
    }

    void FinalizarCutscene(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainMenu"); // nome exato da cena do jogo
    }
}
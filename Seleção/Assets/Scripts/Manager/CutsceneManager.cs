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
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("SampleScene"); // nome exato da cena do jogo
        }
    }

    void FinalizarCutscene(VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene"); // nome exato da cena do jogo
    }
}
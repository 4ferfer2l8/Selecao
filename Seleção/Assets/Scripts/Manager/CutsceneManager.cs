using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += FinalizarCutscene;
    }

    void FinalizarCutscene(VideoPlayer vp)
    {
        SceneManager.LoadScene("SampleScene"); // nome exato da cena do jogo
    }
}
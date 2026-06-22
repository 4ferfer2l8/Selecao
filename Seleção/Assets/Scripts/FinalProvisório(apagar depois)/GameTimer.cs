using UnityEngine;
using UnityEngine.UI;
using TMPro; // remova se usar Text legacy

public class GameTimer : MonoBehaviour
{
    [Header("Configurações")]
    public float tempoTotal = 240f;
    public GameObject thankYouPanel;

    [Header("UI")]
    public TextMeshProUGUI textoTimer; 

    private float tempoRestante;
    private bool jogoEncerrado = false;

    void Start()
    {
        tempoRestante = tempoTotal;
        thankYouPanel.SetActive(false);
    }

    void Update()
    {
        if (jogoEncerrado) return;

        tempoRestante -= Time.deltaTime;
        AtualizarTextoTimer();

        if (tempoRestante <= 0f)
        {
            EncerrarJogo();
        }
    }

    void AtualizarTextoTimer()
    {
        float t = Mathf.Max(tempoRestante, 0f);
        int minutos = Mathf.FloorToInt(t / 60f);
        int segundos = Mathf.FloorToInt(t % 60f);
        textoTimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    void EncerrarJogo()
    {
        jogoEncerrado = true;
        Time.timeScale = 0f;
        thankYouPanel.SetActive(true);
    }
}
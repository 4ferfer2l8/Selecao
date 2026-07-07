using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class TutorialManager : MonoBehaviour {
    public static TutorialManager instance;

    [Header("UI")]
    public GameObject painelTutorial;
    public TextMeshProUGUI textoMensagem;
    public Button botaoProximo;

    [Header("Foco Gamepad (desliga durante o tutorial pra não carimbar sozinho)")]
    public FocoGamepadDocumento focoDocumento;

    private int etapaAtual = 0;
    private bool tutorialAtivo = false;
    private bool tutorialJaFeito = false;

    void Awake() {
        instance = this;
    }

    void Start() {
        painelTutorial.SetActive(false);
        botaoProximo.onClick.AddListener(ProximaEtapa);
    }

    void Update() {
        if (!tutorialAtivo) return;

        var gp = Gamepad.current;
        if (gp != null && gp.buttonEast.wasPressedThisFrame) // B
        {
            ProximaEtapa();
        }
    }

    public void IniciarTutorial() {
        if (tutorialJaFeito)
            return;
        tutorialAtivo = true;
        etapaAtual = 0;
        painelTutorial.SetActive(true);

        // desliga o foco do documento enquanto o tutorial estiver rolando
        if (focoDocumento != null)
            focoDocumento.enabled = false;

        MostrarEtapa();
    }

    void MostrarEtapa() {
        switch (etapaAtual)
        {
            case 0:
                textoMensagem.text = "Este é o documento do candidato. " +
                    "Leia as informações com atenção!";
                break;
            case 1:
                textoMensagem.text = "Selecione um carimbo à direita  Aprovado ou Reprovado" +
                    "  e clique no documento para carimbar!";
                break;
            case 2:
                textoMensagem.text = "Perfeito! Após carimbar o candidato será liberado automaticamente.";
                break;
            case 3:
                textoMensagem.text = "Pressione Espaço para chamar o próximo candidato!";
                break;
            case 4:
                EncerrarTutorial();
                break;
        }
    }

    public void ProximaEtapa() {
        etapaAtual++;
        MostrarEtapa();
    }

    void EncerrarTutorial() {
        painelTutorial.SetActive(false);
        tutorialAtivo = false;
        tutorialJaFeito = true;

        // religa o foco do documento quando o tutorial termina
        if (focoDocumento != null)
            focoDocumento.enabled = true;
    }

    public void EsconderPainel() {
        painelTutorial.SetActive(false);
    }

    public void MostrarPainel() {
        if (tutorialAtivo)
            painelTutorial.SetActive(true);
    }

    public bool TutorialAtivo() {
        return tutorialAtivo;
    }
}
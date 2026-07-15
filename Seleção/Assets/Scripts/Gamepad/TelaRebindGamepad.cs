using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelaRebindGamepad : MonoBehaviour {

    [System.Serializable]
    public class LinhaBind {
        public AcaoGamepad acao;
        public TextMeshProUGUI textoBotaoAtual;
        public Button botaoRemapear;
    }

    [Header("Uma linha pra cada ação remapeável")]
    public LinhaBind[] linhas;

    [Header("Painel 'Pressione um botão...'")]
    public GameObject overlayEspera;

    private AcaoGamepad? acaoEmRemapeamento = null;

    void OnEnable() {
        AtualizarTodosOsTextos();

        foreach (var linha in linhas)
        {
            AcaoGamepad acaoCapturada = linha.acao; // evita bug de closure
            linha.botaoRemapear.onClick.RemoveAllListeners();
            linha.botaoRemapear.onClick.AddListener(() => IniciarRemapeamento(acaoCapturada));
        }

        if (overlayEspera != null)
            overlayEspera.SetActive(false);
    }

    void Update() {
        if (acaoEmRemapeamento == null) return;

        string botaoDetectado = GamepadBindings.DetectarBotaoApertado();
        if (botaoDetectado != null)
        {
            GamepadBindings.Remapear(acaoEmRemapeamento.Value, botaoDetectado);
            acaoEmRemapeamento = null;

            if (overlayEspera != null)
                overlayEspera.SetActive(false);

            AtualizarTodosOsTextos();
        }
    }

    void IniciarRemapeamento(AcaoGamepad acao) {
        acaoEmRemapeamento = acao;

        if (overlayEspera != null)
            overlayEspera.SetActive(true);
    }

    void AtualizarTodosOsTextos() {
        foreach (var linha in linhas)
        {
            string nomeBotao = GamepadBindings.NomeBotaoAtual(linha.acao);
            linha.textoBotaoAtual.text = GamepadBindings.NomeAmigavel(nomeBotao);
        }
    }

    // Chama isso num botão "Restaurar Padrão" se quiser
    public void RestaurarPadrao() {
        GamepadBindings.RestaurarPadrao();
        AtualizarTodosOsTextos();
    }
}
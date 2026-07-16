using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class TelaRebindTeclado : MonoBehaviour
{

    [System.Serializable]
    public class LinhaBind
    {
        public AcaoTeclado acao;
        public TextMeshProUGUI textoTeclaAtual;
        public Button botaoRemapear;
    }

    [Header("Uma linha pra cada ação remapeável")]
    public LinhaBind[] linhas;

    [Header("Painel 'Pressione uma tecla...'")]
    public GameObject overlayEspera;

    private AcaoTeclado? acaoEmRemapeamento = null;
    private float timeScaleAnterior = 1f;

    void OnEnable()
    {
        AtualizarTodosOsTextos();

        foreach (var linha in linhas)
        {
            AcaoTeclado acaoCapturada = linha.acao; // evita bug de closure
            linha.botaoRemapear.onClick.RemoveAllListeners();
            linha.botaoRemapear.onClick.AddListener(() => IniciarRemapeamento(acaoCapturada));
        }

        if (overlayEspera != null)
            overlayEspera.SetActive(false);
    }

    void OnDisable()
    {
        // Segurança: se a tela for desativada no meio de um remapeamento,
        // garante que o jogo não fique travado pausado pra sempre.
        if (acaoEmRemapeamento != null)
        {
            FinalizarRemapeamento();
        }
    }

    void Update()
    {
        if (acaoEmRemapeamento == null) return;

        Key? teclaDetectada = KeyboardBindings.DetectarTeclaApertada();
        if (teclaDetectada != null)
        {
            KeyboardBindings.Remapear(acaoEmRemapeamento.Value, teclaDetectada.Value);
            FinalizarRemapeamento();
        }
    }

    void IniciarRemapeamento(AcaoTeclado acao)
    {
        acaoEmRemapeamento = acao;

        // Bloqueia o pause (e qualquer outro sistema) de reagir ao input
        KeyboardBindings.EmRemapeamento = true;

        // Pausa o jogo enquanto espera a tecla
        timeScaleAnterior = Time.timeScale;
        Time.timeScale = 0f;

        if (overlayEspera != null)
            overlayEspera.SetActive(true);
    }

    void FinalizarRemapeamento()
    {
        acaoEmRemapeamento = null;

        KeyboardBindings.EmRemapeamento = false;
        Time.timeScale = timeScaleAnterior;

        if (overlayEspera != null)
            overlayEspera.SetActive(false);

        AtualizarTodosOsTextos();
    }

    void AtualizarTodosOsTextos()
    {
        foreach (var linha in linhas)
        {
            Key tecla = KeyboardBindings.TeclaAtual(linha.acao);
            linha.textoTeclaAtual.text = KeyboardBindings.NomeAmigavel(tecla);
        }
    }

    public void RestaurarPadrao()
    {
        KeyboardBindings.RestaurarPadrao();
        AtualizarTodosOsTextos();
    }
}
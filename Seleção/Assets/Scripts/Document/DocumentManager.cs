using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DocumentManager : MonoBehaviour
{
    public static DocumentManager Instance { get; private set; }

    // ─── Listas de documentos ────────────────────────────────────────────────
    [Header("Documentos Positivos")]
    [SerializeField] private List<DocumentData> documentosPositivos = new List<DocumentData>();

    [Header("Documentos Negativos")]
    [SerializeField] private List<DocumentData> documentosNegativos = new List<DocumentData>();

    [Header("Configurações")]
    [SerializeField] private DocumentDisplay documentDisplay;

    // ─── Eventos ─────────────────────────────────────────────────────────────
    [Header("Eventos")]
    public UnityEvent<DocumentData> onDocumentoSelecionado;
    public UnityEvent onDocumentoLimpo;

    // ─── Estado ──────────────────────────────────────────────────────────────
    private DocumentData _documentoAtual;
    private Individuo    _individuoAtual;

    public DocumentData DocumentoAtual => _documentoAtual;
    public Individuo    IndividuoAtual  => _individuoAtual;

    // ─── Unity ───────────────────────────────────────────────────────────────
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }


    public void OnNovoIndividuo(Individuo individuo)
    {
        _individuoAtual  = individuo;
        _documentoAtual  = SortearDocumento();

        if (_documentoAtual == null)
        {
            Debug.LogWarning("[DocumentManager] Nenhum documento disponível para sortear!");
            return;
        }

        // Vincula o documento ao indivíduo
        individuo.documentoSorteado = _documentoAtual;

        if (documentDisplay != null)
            documentDisplay.SetDocument(_documentoAtual);

        onDocumentoSelecionado?.Invoke(_documentoAtual);

        Debug.Log($"[DocumentManager] Documento sorteado: {_documentoAtual.documentID} " +
                  $"| Categoria: {_documentoAtual.category} " +
                  $"| Indivíduo: {individuo.codigo}");
    }

    public void LimparDocumento()
    {
        _documentoAtual = null;
        _individuoAtual = null;

        if (documentDisplay != null)
            documentDisplay.ClearDocument();

        onDocumentoLimpo?.Invoke();
    }

    // ─── Lógica de Sorteio ───────────────────────────────────────────────────

    private DocumentData SortearDocumento()
    {
        bool sortearPositivo;

        // pergunta ao sistema adaptativo se ele quer forçar um tipo
        if (SistemaAdaptativo.instance != null &&
            SistemaAdaptativo.instance.DeveForcarTipo(out bool forcarPositivo))
        {
            sortearPositivo = forcarPositivo;
        }
        else
        {
            // comportamento normal: 50/50
            sortearPositivo = Random.value >= 0.5f;
        }

        if (sortearPositivo && documentosPositivos.Count > 0)
            return Aleatorio(documentosPositivos);

        if (!sortearPositivo && documentosNegativos.Count > 0)
            return Aleatorio(documentosNegativos);

        // fallback se uma das listas estiver vazia
        if (documentosPositivos.Count > 0) return Aleatorio(documentosPositivos);
        if (documentosNegativos.Count > 0) return Aleatorio(documentosNegativos);

        return null;
    }

    private DocumentData Aleatorio(List<DocumentData> lista)
    {
        return lista[Random.Range(0, lista.Count)];
    }

    private void OnValidate()
    {
        if (documentosPositivos.Count == 0)
            Debug.LogWarning("[DocumentManager] Lista de documentos POSITIVOS está vazia!");
        if (documentosNegativos.Count == 0)
            Debug.LogWarning("[DocumentManager] Lista de documentos NEGATIVOS está vazia!");
    }
}
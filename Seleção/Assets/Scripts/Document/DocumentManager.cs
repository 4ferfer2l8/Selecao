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

    [Header("Documentos Especiais (Email)")]
    [SerializeField] private List<DocumentData> documentosEspeciais = new List<DocumentData>();


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

    private int contadorNPC = 0;
    private Dictionary<int, DocumentData> slotsEspeciais = new Dictionary<int, DocumentData>();

    // ─── Unity ───────────────────────────────────────────────────────────────
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }


    public void OnNovoIndividuo(Individuo individuo)
    {
        contadorNPC++; 

        _individuoAtual  = individuo;
        _documentoAtual  = SortearDocumento();

        if (_documentoAtual == null)
        {
            Debug.LogWarning("[DocumentManager] Nenhum documento disponível para sortear!");
            return;
        }

        individuo.documentoSorteado = _documentoAtual;

        if (documentDisplay != null)
            documentDisplay.SetDocument(_documentoAtual);

        onDocumentoSelecionado?.Invoke(_documentoAtual);

        Debug.Log($"[DocumentManager] Slot {contadorNPC} | Documento: {_documentoAtual.documentID} " +
                $"| Categoria: {_documentoAtual.category}");
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
        // 1º — tem um especial agendado pra este slot? ele tem prioridade absoluta
        if (slotsEspeciais.TryGetValue(contadorNPC, out DocumentData especial))
        {
            Debug.Log($"[DocumentManager] >>> ESPECIAL no slot {contadorNPC}: {especial.documentID}");
            return especial;
        }

        // 2º — o sistema adaptativo quer intervir?
        bool sortearPositivo;
        if (SistemaAdaptativo.instance != null &&
            SistemaAdaptativo.instance.DeveForcarTipo(out bool forcarPositivo))
        {
            sortearPositivo = forcarPositivo;
        }
        else
        {
            sortearPositivo = Random.value >= 0.5f;
        }

        if (sortearPositivo && documentosPositivos.Count > 0)
            return Aleatorio(documentosPositivos);

        if (!sortearPositivo && documentosNegativos.Count > 0)
            return Aleatorio(documentosNegativos);

        if (documentosPositivos.Count > 0) return Aleatorio(documentosPositivos);
        if (documentosNegativos.Count > 0) return Aleatorio(documentosNegativos);

        return null;
    }

    /// <summary>
    /// Sorteia em quais slots restantes os 5 especiais vão cair.
    /// Chamado pelo GerenciadorDeProgressao quando o email dispara.
    /// </summary>
    public void AgendarEspeciais(int totalNPCs)
    {
        slotsEspeciais.Clear();

        List<int> disponiveis = new List<int>();
        for (int i = contadorNPC + 1; i <= totalNPCs; i++)
            disponiveis.Add(i);

        if (disponiveis.Count < documentosEspeciais.Count)
        {
            Debug.LogError($"[DocumentManager] Só há {disponiveis.Count} slots restantes " +
                        $"para {documentosEspeciais.Count} especiais! Ajuste o prazoEmail.");
        }

        // embaralha os slots disponíveis (Fisher-Yates)
        for (int i = 0; i < disponiveis.Count; i++)
        {
            int j = Random.Range(i, disponiveis.Count);
            int temp = disponiveis[i];
            disponiveis[i] = disponiveis[j];
            disponiveis[j] = temp;
        }

        int qtd = Mathf.Min(documentosEspeciais.Count, disponiveis.Count);
        for (int i = 0; i < qtd; i++)
            slotsEspeciais[disponiveis[i]] = documentosEspeciais[i];

        Debug.Log($"[DocumentManager] {qtd} especiais agendados nos slots: " +
                string.Join(", ", slotsEspeciais.Keys));
    }

    /// <summary>Retorna true se o documento é um dos 5 do email.</summary>
    public bool EhEspecial(DocumentData doc)
    {
        return documentosEspeciais.Contains(doc);
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
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Cérebro da fase única. Conta NPCs e atos de desobediência,
/// dispara o email e decide o final.
/// </summary>
public class GerenciadorDeProgressao : MonoBehaviour
{
    public static GerenciadorDeProgressao instance;

    [Header("Estrutura da Fase")]
    [SerializeField] private int totalNPCs = 15;

    [Tooltip("Último NPC em que o email ainda pode ser disparado. " +
             "Precisa ser totalNPCs menos a quantidade de documentos especiais.")]
    [SerializeField] private int prazoEmail = 10;

    [SerializeField] private int desobedienciasParaEmail = 5;
    [SerializeField] private int especiaisNecessarios = 5;

    [Header("Cenas de Final")]
    [SerializeField] private string cenaFinalBom = "FinalBom";
    [SerializeField] private string cenaFinalRuim = "FinalRuim";

    [Header("Email")]
    [SerializeField] private ComputerUIManager computador;
    [Tooltip("Espera antes do email aparecer, pra não colidir com a saída do NPC")]
    [SerializeField] private float delayEmail = 1.5f;

    // ─── Estado ───
    private int npcAtual = 0;
    private int desobediencias = 0;
    private int especiaisAprovados = 0;
    private bool emailEnviado = false;
    private bool janelaFechada = false;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Chamado pelo Document no momento do carimbo.
    /// </summary>
    public void RegistrarDecisao(bool aprovou, DocumentData doc)
    {
        npcAtual++;

        bool eraPositivo = doc.category == DocumentCategory.Positive;

        // desobediência = aprovar um negativo OU rejeitar um positivo
        bool desobedeceu = (aprovou && !eraPositivo) || (!aprovou && eraPositivo);

        if (desobedeceu)
        {
            desobediencias++;
            Debug.Log($"[Progressão] Ato de desobediência #{desobediencias} (NPC {npcAtual})");
        }

        // se era um dos 5 especiais e o jogador aprovou, conta
        if (DocumentManager.Instance != null && DocumentManager.Instance.EhEspecial(doc))
        {
            if (aprovou)
            {
                especiaisAprovados++;
                Debug.Log($"[Progressão] ESPECIAL aprovado! ({especiaisAprovados}/{especiaisNecessarios})");
            }
            else
            {
                Debug.Log($"[Progressão] ESPECIAL rejeitado — final bom perdido.");
            }
        }

        // dispara o email?
        if (!emailEnviado && !janelaFechada && desobediencias >= desobedienciasParaEmail)
            DispararEmail();

        // a janela fechou?
        if (!emailEnviado && npcAtual >= prazoEmail)
        {
            janelaFechada = true;
            Debug.Log("[Progressão] Janela de resistência FECHADA. Final ruim travado. " +
                      "O jogo continua normalmente.");
        }

        Debug.Log($"─── NPC {npcAtual}/{totalNPCs} | desobediências: {desobediencias} ───");
    }

    private void DispararEmail()
    {
        emailEnviado = true;
        Debug.Log("═══ EMAIL DISPARADO ═══");

        // agenda os especiais imediatamente (isso não pode esperar)
        if (DocumentManager.Instance != null)
            DocumentManager.Instance.AgendarEspeciais(totalNPCs);

        // mas a aparição na tela espera a mesa limpar
        StartCoroutine(MostrarEmailComDelay());
    }

    private System.Collections.IEnumerator MostrarEmailComDelay()
    {
        yield return new WaitForSeconds(delayEmail);

        if (computador != null)
            computador.DesbloquearEmail();
    }

    /// <summary>Chamado pelo NPCControle depois que o NPC sai de cena.</summary>
    public bool FaseAcabou()
    {
        return npcAtual >= totalNPCs;
    }

    /// <summary>Chamado pelo NPCControle quando a fase acaba.</summary>
    public void FinalizarFase()
    {
        bool finalBom = emailEnviado && especiaisAprovados >= especiaisNecessarios;

        Debug.Log("═══════════════════════════════");
        Debug.Log($"FIM DO JOGO — Final: {(finalBom ? "BOM" : "RUIM")}");
        Debug.Log($"Email enviado: {emailEnviado} | Especiais aprovados: {especiaisAprovados}/{especiaisNecessarios}");
        Debug.Log("═══════════════════════════════");

        SceneManager.LoadScene(finalBom ? cenaFinalBom : cenaFinalRuim);
    }

    private void OnValidate()
    {
        // avisa se os números não fecham
        if (totalNPCs - prazoEmail < especiaisNecessarios)
            Debug.LogWarning($"[Progressão] Os números não fecham! Sobram apenas " +
                             $"{totalNPCs - prazoEmail} slots para {especiaisNecessarios} especiais. " +
                             $"Diminua o prazoEmail ou aumente o totalNPCs.");
    }
}
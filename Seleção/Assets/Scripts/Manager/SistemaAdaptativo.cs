using UnityEngine;

/// <summary>
/// Observa o desempenho do jogador e influencia o próximo documento.
/// Quando o jogador acerta demais seguido, força vir um documento que
/// quebra o padrão, pra tirá-lo do "piloto automático".
/// </summary>
public class SistemaAdaptativo : MonoBehaviour
{
    public static SistemaAdaptativo instance;

    [Header("Configuração")]
    [Tooltip("Quantos acertos seguidos até o sistema intervir")]
    [SerializeField] private int limiteSequencia = 3;

    [Tooltip("Chance de intervir quando o limite é atingido (0 a 1)")]
    [Range(0f, 1f)]
    [SerializeField] private float chanceDeIntervir = 0.7f;

    // estado interno
    private int acertosSeguidos = 0;
    private bool ultimoDocEraPositivo = true;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Chamado a cada decisão do jogador, pra atualizar a sequência.
    /// </summary>
    public void RegistrarResultado(bool acertou, bool documentoEraPositivo)
    {
        if (acertou)
            acertosSeguidos++;
        else
            acertosSeguidos = 0; // errou, zera a maré

        ultimoDocEraPositivo = documentoEraPositivo;

        Debug.Log($"[Adaptativo] Acertos seguidos: {acertosSeguidos}");
    }

    /// <summary>
    /// O DocumentManager pergunta: devo forçar um tipo específico agora?
    /// Retorna true se o sistema quer intervir, e devolve o tipo via 'out'.
    /// </summary>
    public bool DeveForcarTipo(out bool forcarPositivo)
    {
        forcarPositivo = true;

        // só intervém se o jogador está numa sequência boa
        if (acertosSeguidos < limiteSequencia)
            return false;

        // e mesmo assim, só às vezes (pra não ser previsível)
        if (Random.value > chanceDeIntervir)
            return false;

        // quebra o padrão: manda o OPOSTO do último documento
        // se ele vinha lidando com positivos, manda um negativo, e vice-versa
        forcarPositivo = !ultimoDocEraPositivo;

        Debug.Log($"[Adaptativo] INTERVINDO! Forçando documento " +
                  $"{(forcarPositivo ? "POSITIVO" : "NEGATIVO")} pra quebrar o padrão.");

        // depois de intervir, reseta a contagem pra dar um respiro
        acertosSeguidos = 0;

        return true;
    }
}
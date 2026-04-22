using UnityEngine;

public class SistemaAdaptativo : MonoBehaviour
{
    [Header("Histórico de Decisões")]
    private int totalDecisoes = 0;
    private int descartes = 0;
    private int reproducoes = 0;
    private int reprocessamentos = 0;

    [Header("Configurações")]
    [Range(0f, 1f)] public float sensibilidade = 0.3f;
    // quanto o sistema reage às suas decisões

    private GeradorDeIndividuos gerador;

    private void Start()
    {
        gerador = GetComponent<GeradorDeIndividuos>();
    }

    public void RegistrarDecisao(string destino)
    {
        totalDecisoes++;

        switch (destino)
        {
            case "Descarte":        descartes++;        break;
            case "Reproducao":      reproducoes++;      break;
            case "Reprocessamento": reprocessamentos++; break;
        }

        AtualizarPesos();
    }

    private void AtualizarPesos()
    {
        if (totalDecisoes < 3) return;
        // espera pelo menos 3 decisões antes de adaptar

        float taxaDescarte = (float)descartes / totalDecisoes;
        float taxaReproducao = (float)reproducoes / totalDecisoes;

        // se jogador tá descartando muito - gera mais casos ambíguos
        // pra forçar ele a pensar antes de descartar
        if (taxaDescarte > 0.6f)
        {
            gerador.pesoAmbiguidade = Mathf.Min(
                gerador.pesoAmbiguidade + sensibilidade * 0.1f, 
                0.9f
            );
            Debug.Log("Sistema: aumentando ambiguidade. Taxa de descarte alta.");
        }

        // se jogador tá sendo muito generoso com reprodução - pressiona com regras
        else if (taxaReproducao > 0.6f)
        {
            gerador.pesoAmbiguidade = Mathf.Min(
                gerador.pesoAmbiguidade + sensibilidade * 0.1f,
                0.9f
            );
            Debug.Log("Sistema: aumentando ambiguidade. Taxa de reprodução alta.");
        }

        // se jogador tá equilibrado - reduz ambiguidade levemente
        else
        {
            gerador.pesoAmbiguidade = Mathf.Max(
                gerador.pesoAmbiguidade - sensibilidade * 0.05f,
                0.1f
            );
            Debug.Log("Sistema: reduzindo ambiguidade. Jogador equilibrado.");
        }
    }

    public float GetTaxaDescarte()
    {
        if (totalDecisoes == 0) return 0f;
        return (float)descartes / totalDecisoes;
    }

    public float GetTaxaReproducao()
    {
        if (totalDecisoes == 0) return 0f;
        return (float)reproducoes / totalDecisoes;
    }

    public int GetTotalDecisoes() => totalDecisoes;
}
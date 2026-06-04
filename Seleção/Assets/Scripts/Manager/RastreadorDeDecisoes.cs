using UnityEngine;

public class RastreadorDeDecisoes : MonoBehaviour
{
    [Header("Contadores")]
    private int totalDecisoes = 0;
    private int decisoesCorretas = 0;
    private int decisoesIncorretas = 0;
    private int hesitacoes = 0;
    // hesitação = demorou mais que o tempo limite pra decidir

    [Header("Configurações")]
    public float tempoLimiteDecisao = 30f; // segundos
    private float tempoAtualDecisao = 0f;
    private bool aguardandoDecisao = false;

    private SistemaAdaptativo sistemaAdaptativo;

    private void Start()
    {
        sistemaAdaptativo = GetComponent<SistemaAdaptativo>();
    }

    private void Update()
    {
        if (!aguardandoDecisao) return;

        tempoAtualDecisao += Time.deltaTime;

        if (tempoAtualDecisao >= tempoLimiteDecisao)
        {
            hesitacoes++;
            Debug.Log("Hesitação registrada.");
            // aqui futuramente pode disparar um evento sonoro
        }
    }

    public void IniciarContagem()
    {
        tempoAtualDecisao = 0f;
        aguardandoDecisao = true;
    }

    public void RegistrarDecisao(string destinoEscolhido, string destinoCorreto)
    {
        aguardandoDecisao = false;
        totalDecisoes++;

        bool correta = destinoEscolhido == destinoCorreto;

        if (correta)
            decisoesCorretas++;
        else
            decisoesIncorretas++;

        // repassa pro sistema adaptativo
        sistemaAdaptativo.RegistrarDecisao(destinoEscolhido);

        Debug.Log($"Decisão: {destinoEscolhido} | Correto: {destinoCorreto} | Acertou: {correta}");
    }

    public TipoFinal CalcularFinal()
    {
        if (totalDecisoes == 0) return TipoFinal.Neutro;

        float taxaAcerto = (float)decisoesCorretas / totalDecisoes;
        float taxaHesitacao = (float)hesitacoes / totalDecisoes;

        // jogador obedeceu o sistema quase sempre
        if (taxaAcerto >= 0.7f && taxaHesitacao < 0.3f)
            return TipoFinal.Conformista;

        // jogador errou muito ou hesitou muito — resistiu de alguma forma
        if (taxaAcerto < 0.4f || taxaHesitacao >= 0.5f)
            return TipoFinal.Resistente;

        // ficou no meio — nem obedeceu nem resistiu de verdade
        return TipoFinal.Neutro;
    }

    // getters pro relatório de eficiência
    public int GetTotalDecisoes()      => totalDecisoes;
    public int GetDecisoesCorretas()   => decisoesCorretas;
    public int GetDecisoesIncorretas() => decisoesIncorretas;
    public int GetHesitacoes()         => hesitacoes;
}

public enum TipoFinal
{
    Conformista, // obedeceu o sistema
    Resistente,  // resistiu de alguma forma
    Neutro       // ficou no meio
}
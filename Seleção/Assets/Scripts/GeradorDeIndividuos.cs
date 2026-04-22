using UnityEngine;
using System.Collections.Generic;

public class GeradorDeIndividuos : MonoBehaviour
{
    [Header("Configurações de Geração")]
    public List<Sprite> rostos; // lista vazia, a gnt preenche qnd tiver os assets

    [Header("Pesos Adaptativos (0 a 1)")]
    [Range(0f, 1f)] public float pesoAmbiguidade = 0.3f;
    // quanto maior, mais casos ambíguos o sistema vai gerar

    public Individuo GerarIndividuo()
    {
        Individuo novo = new Individuo();

        // gera código único
        novo.codigo = "UN-" + Random.Range(1000, 9999);

        // gera atributos com ou sem ambiguidade
        novo.indiceCrescimento    = GerarAtributo();
        novo.eficienciaMetabolica = GerarAtributo();
        novo.taxaCooperacao       = GerarAtributo();
        novo.potencialProteico    = GerarAtributo();

        // rosto aleatório (só funciona quando tiver assets)
        if (rostos != null && rostos.Count > 0)
            novo.rosto = rostos[Random.Range(0, rostos.Count)];

        return novo;
    }

    private float GerarAtributo()
    {
        // chance de gerar valor ambíguo (próximo dos limites das regras)
        if (Random.value < pesoAmbiguidade)
            return GerarValorAmbiguo();

        return Mathf.Round(Random.Range(0f, 10f) * 10f) / 10f;
    }

    private float GerarValorAmbiguo()
    {
        // gera valor próximo de 5.0 (zona de fronteira)
        float base_ = 5f;
        float variacao = Random.Range(-0.5f, 0.5f);
        return Mathf.Round((base_ + variacao) * 10f) / 10f;
    }
}
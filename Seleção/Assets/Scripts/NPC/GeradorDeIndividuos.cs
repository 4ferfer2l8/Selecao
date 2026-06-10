using UnityEngine;
using System.Collections.Generic;

public class GeradorDeIndividuos : MonoBehaviour
{
    [Header("Configurações de Geração")]
    public List<Sprite> rostos;
    public List<Sprite> documentos; // arrasta os 6 sprites aqui no Inspector

    [Header("Pesos Adaptativos (0 a 1)")]
    [Range(0f, 1f)] public float pesoAmbiguidade = 0.3f;

    public Individuo GerarIndividuo()
    {
        Individuo novo = new Individuo();

        novo.codigo = "UN-" + Random.Range(1000, 9999);

        novo.indiceCrescimento    = GerarAtributo();
        novo.eficienciaMetabolica = GerarAtributo();
        novo.taxaCooperacao       = GerarAtributo();
        novo.potencialProteico    = GerarAtributo();

        if (rostos != null && rostos.Count > 0)
            novo.rosto = rostos[Random.Range(0, rostos.Count)];

        // sorteia documento junto com o indivíduo
        if (documentos != null && documentos.Count > 0)
            novo.documento = documentos[Random.Range(0, documentos.Count)];

        return novo;
    }

    private float GerarAtributo()
    {
        if (Random.value < pesoAmbiguidade)
            return GerarValorAmbiguo();

        return Mathf.Round(Random.Range(0f, 10f) * 10f) / 10f;
    }

    private float GerarValorAmbiguo()
    {
        float base_ = 5f;
        float variacao = Random.Range(-0.5f, 0.5f);
        return Mathf.Round((base_ + variacao) * 10f) / 10f;
    }
}
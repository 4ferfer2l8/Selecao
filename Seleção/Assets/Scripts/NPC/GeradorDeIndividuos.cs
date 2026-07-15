using UnityEngine;
using System.Collections.Generic;

public class GeradorDeIndividuos : MonoBehaviour
{
    [Header("Configurações de Geração")]
    public List<Sprite> rostos;

    [Header("Pesos Adaptativos (0 a 1)")]
    [Range(0f, 1f)] public float pesoAmbiguidade = 0.3f;

    public Individuo GerarIndividuo()
    {
        Individuo novo = new Individuo();

        novo.codigo = "UN-" + Random.Range(1000, 9999);

        if (rostos != null && rostos.Count > 0)
            novo.rosto = rostos[Random.Range(0, rostos.Count)];

        return novo;
    }
}
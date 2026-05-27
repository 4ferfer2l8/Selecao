using UnityEngine;

[System.Serializable]
public class Individuo
{
    public string codigo;
    public Sprite rosto;

    public Sprite documento;
    [Range(0f, 10f)] public float indiceCrescimento;
    [Range(0f, 10f)] public float eficienciaMetabolica;
    [Range(0f, 10f)] public float taxaCooperacao;
    [Range(0f, 10f)] public float potencialProteico;

    public string destino; // "Reproducao", "Descarte", "Reprocessamento"
    
    
    [HideInInspector] public DocumentData documentoSorteado;
}

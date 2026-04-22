using UnityEngine;

[System.Serializable]
public class Individuo : MonoBehaviour
{
    public string codigo;
    public Sprite rosto;

    [Range(0f, 10f)] public float indiceCrescimento;
    [Range(0f, 10f)] public float eficienciaMetabolica;
    [Range(0f, 10f)] public float taxaCooperacao;
    [Range(0f, 10f)] public float potencialProteico;

    public string destino; // "Reproducao", "Descarte", "Reprocessamento"
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

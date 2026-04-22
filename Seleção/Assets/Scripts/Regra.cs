using UnityEngine;

[System.Serializable]
public class Regra : MonoBehaviour
{

    public string atributo; // ex: "taxaCooperacao"
    public string operador; // "maior", "menor", "entre", "igual"
    public float valorMin;
    public float valorMax; // só usado quanto operador for "entre"
    public string destino; // destino resultante se a regra for satisfeita
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

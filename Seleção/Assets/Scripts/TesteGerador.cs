using UnityEngine;
using UnityEngine.InputSystem;

public class TesteGerador : MonoBehaviour
{
    private GeradorDeAparencia geradorDeAparencia;
    private GeradorDeIndividuos geradorDeIndividuos;

    private void Start()
    {
        geradorDeAparencia  = GetComponent<GeradorDeAparencia>();
        geradorDeIndividuos = GetComponent<GeradorDeIndividuos>();

        GerarNovoNPC();
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            GerarNovoNPC();
    }

    private void GerarNovoNPC()
    {
        // gera dados
        Individuo individuo = geradorDeIndividuos.GerarIndividuo();

        // gera aparência
        geradorDeAparencia.GerarAparenciaAleatoria();

        // loga os dados no console
        Debug.Log("─────────────────────────────");
        Debug.Log("NOVO INDIVÍDUO GERADO");
        Debug.Log("─────────────────────────────");
        Debug.Log($"Código:                {individuo.codigo}");
        Debug.Log($"Índice de Crescimento: {individuo.indiceCrescimento}");
        Debug.Log($"Efic. Metabólica:      {individuo.eficienciaMetabolica}");
        Debug.Log($"Taxa de Cooperação:    {individuo.taxaCooperacao}");
        Debug.Log($"Potencial Proteico:    {individuo.potencialProteico}");
        Debug.Log("─────────────────────────────");
    }
}
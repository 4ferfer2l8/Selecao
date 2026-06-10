using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ListaDeRegras
{
    public List<Regra> regras;
}

public class GerenciadorDeRegras : MonoBehaviour
{
    private List<Regra> regrasAtivas = new List<Regra>();

    public void CarregarFase(int fase)
    {
        string caminho = "Regras/fase" + fase;
        TextAsset arquivo = Resources.Load<TextAsset>(caminho);

        if (arquivo == null)
        {
            Debug.LogError("Arquivo de regras não encontrado: " + caminho);
            return;
        }

        ListaDeRegras lista = JsonUtility.FromJson<ListaDeRegras>(arquivo.text);
        regrasAtivas = lista.regras;

        Debug.Log("Fase " + fase + " carregada com " + regrasAtivas.Count + " regras.");
    }

    public string AvaliarIndividuo(Individuo individuo)
    {
        foreach (Regra regra in regrasAtivas)
        {
            float valor = PegarAtributo(individuo, regra.atributo);

            bool satisfeita = false;

            switch (regra.operador)
            {
                case "maior":
                    satisfeita = valor > regra.valorMin;
                    break;
                case "menor":
                    satisfeita = valor < regra.valorMin;
                    break;
                case "entre":
                    satisfeita = valor >= regra.valorMin && valor <= regra.valorMax;
                    break;
            }

            if (satisfeita)
                return regra.destino;
        }

        return "Descarte"; // destino padrão se nenhuma regra for satisfeita
    }

    private float PegarAtributo(Individuo individuo, string atributo)
    {
        switch (atributo)
        {
            case "indiceCrescimento":    return individuo.indiceCrescimento;
            case "eficienciaMetabolica": return individuo.eficienciaMetabolica;
            case "taxaCooperacao":       return individuo.taxaCooperacao;
            case "potencialProteico":    return individuo.potencialProteico;
            default:
                Debug.LogWarning("Atributo desconhecido: " + atributo);
                return 0f;
        }
    }
}
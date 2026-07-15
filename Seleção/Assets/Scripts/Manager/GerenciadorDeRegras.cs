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

}
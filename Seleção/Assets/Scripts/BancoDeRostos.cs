using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BancoDeRostos", menuName = "Selecao/Banco de Rostos")]
public class BancoDeRostos : ScriptableObject
{
    [Header("Sprites por Categoria")]
    public List<Sprite> corpos;
    public List<Sprite> olhos;
    public List<Sprite> narizes;
    public List<Sprite> bocas;

    public Sprite GetAleatorio(List<Sprite> categoria)
    {
        if (categoria == null || categoria.Count == 0)
        {
            Debug.LogWarning("Categoria vazia no BancoDeRostos!");
            return null;
        }

        return categoria[Random.Range(0, categoria.Count)];
    }
}
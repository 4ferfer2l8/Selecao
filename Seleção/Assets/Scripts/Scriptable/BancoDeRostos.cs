using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BancoDeRostos", menuName = "Selecao/Banco de Rostos")]
public class BancoDeRostos : ScriptableObject
{
    [Header("Partes Masculinas")]
    public List<Sprite> corposMasculinos;
    public List<Sprite> olhosMasculinos;
    public List<Sprite> narizesMasculinos;
    public List<Sprite> bocasMasculinas;

    [Header("Partes Femininas")]
    public List<Sprite> corposFemininos;
    public List<Sprite> olhosFemininos;
    public List<Sprite> narizesFemininos;
    public List<Sprite> bocasFemininas;

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
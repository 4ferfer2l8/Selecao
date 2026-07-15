using UnityEngine;

[System.Serializable]
public class Individuo
{
    public string codigo;
    public Sprite rosto;    
    
    [HideInInspector] public DocumentData documentoSorteado;
}

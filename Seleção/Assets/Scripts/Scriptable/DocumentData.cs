using UnityEngine;

[CreateAssetMenu(fileName = "NewDocument", menuName = "Documentos/Document Data")]
public class DocumentData : ScriptableObject
{
    [Header("Identificação")]
    public string documentID;

    [Header("Visual")]
    public Sprite documentSprite;       // PNG do documento expandido

    [Header("Classificação")]
    public DocumentCategory category;
}


public enum DocumentCategory
{
    Positive,   // Documentos que aprovam o indivíduo
    Negative   // Documentos que reprovam / alertam
}
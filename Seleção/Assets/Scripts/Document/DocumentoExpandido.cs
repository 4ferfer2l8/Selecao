using UnityEngine;

public class DocumentoExpandido : MonoBehaviour
{
    [Header("Referência ao popup real")]
    [SerializeField] private DocumentPopup documentPopup;

    public bool EstaAberto => documentPopup != null && documentPopup.IsOpen;
}
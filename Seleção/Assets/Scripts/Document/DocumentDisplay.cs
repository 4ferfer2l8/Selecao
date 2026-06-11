using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Image))]
public class DocumentDisplay : MonoBehaviour
{
    // ─── Referências ────────────────────────────────────────────────────────
    [Header("Visual — Documento na Mesa")]
    [SerializeField] private Image closedDocumentImage;
    [SerializeField] private Sprite defaultClosedSprite;

    [Header("Popup")]
    [SerializeField] private DocumentPopup documentPopup;

    [Header("Comportamento")]
    [SerializeField] private bool interactable = true;

    // ─── Estado ─────────────────────────────────────────────────────────────
    private DocumentData _assignedDocument;
    private bool _hasDocument = false;

    // ─── Unity ──────────────────────────────────────────────────────────────
    private void Awake()
    {
        if (closedDocumentImage == null)
            closedDocumentImage = GetComponent<Image>();
    }


    public void SetDocument(DocumentData data)
    {
        _assignedDocument = data;
        _hasDocument = true;

        if (closedDocumentImage != null && defaultClosedSprite != null)
            closedDocumentImage.sprite = defaultClosedSprite;

        // Pré-carrega o popup sem abri-lo — Open() será instantâneo
        if (documentPopup != null)
            documentPopup.PrepareDocument(data);
    }


    public void ClearDocument()
    {
        _assignedDocument = null;
        _hasDocument = false;

        if (documentPopup != null && documentPopup.IsOpen)
            documentPopup.Close();

        if (documentPopup != null)
            documentPopup.ClearDocument();
    }

    // ─── Interação ──────────────────────────────────────────────────────────


    public void OnDocumentClicked()
    {
        if (!interactable || !_hasDocument || documentPopup == null) return;

        if (!documentPopup.IsOpen)
            documentPopup.Open();
        else
            documentPopup.Close();
    }

    // ─── Getters ────────────────────────────────────────────────────────────
    public DocumentData GetAssignedDocument() => _assignedDocument;
    public bool HasDocument => _hasDocument;
    public void SetInteractable(bool value) => interactable = value;
}
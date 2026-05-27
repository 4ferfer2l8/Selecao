using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Coloque este script no GameObject do documento fechado na mesa.
/// 
/// IMPORTANTE — convivência com SomDePapel:
/// Este script NÃO implementa IPointerClickHandler para não conflitar com
/// SomDePapel (que já trata o clique). Em vez disso, exponha OnDocumentClicked()
/// e chame-o de dentro do SomDePapel (veja instrução abaixo).
/// </summary>
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

    // ─── API Pública ────────────────────────────────────────────────────────

    /// <summary>
    /// Chamado pelo DocumentManager ao sortear um novo documento.
    /// O visual do documento FECHADO não muda — apenas armazena o dado.
    /// </summary>
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

    /// <summary>
    /// Remove o documento atual (fim de atendimento).
    /// </summary>
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

    /// <summary>
    /// Chame este método de dentro do SomDePapel.OnPointerClick(),
    /// logo após a lógica de som existente.
    ///
    /// Em SomDePapel, adicione:
    ///   [SerializeField] private DocumentDisplay documentDisplay;
    ///   // no OnPointerClick(), após o RuntimeManager.PlayOneShot():
    ///   documentDisplay?.OnDocumentClicked();
    /// </summary>
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
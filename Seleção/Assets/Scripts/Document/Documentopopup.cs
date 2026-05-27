using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla o popup expandido do documento.
/// Coloque no GameObject raiz do painel de popup.
/// </summary>
public class DocumentPopup : MonoBehaviour
{
    [Header("Imagem Principal")]
    [SerializeField] private Image documentImage;

    [Header("Botão Fechar")]
    [SerializeField] private Button closeButton;

    [Header("Animação")]
    [SerializeField] private Animator popupAnimator;
    [SerializeField] private string openTrigger  = "Open";
    [SerializeField] private string closeTrigger = "Close";

    [Header("Overlay")]
    [SerializeField] private GameObject overlay;

    // ─── Estado ─────────────────────────────────────────────────────────────
    private DocumentData _preparedDocument;
    private bool _isOpen = false;

    public bool IsOpen => _isOpen;

    // ─── Unity ──────────────────────────────────────────────────────────────
    private void Awake()
    {
        gameObject.SetActive(false);
        if (overlay != null) overlay.SetActive(false);
        if (closeButton != null) closeButton.onClick.AddListener(Close);
    }

    private void Update()
    {
        if (_isOpen && Input.GetKeyDown(KeyCode.Escape))
            Close();
    }

    // ─── API Pública ────────────────────────────────────────────────────────

    /// <summary>
    /// Pré-carrega o sprite sem abrir o popup.
    /// Chamado pelo DocumentDisplay quando um novo indivíduo chega.
    /// </summary>
    public void PrepareDocument(DocumentData data)
    {
        _preparedDocument = data;

        if (data == null) return;

        if (documentImage != null)
            documentImage.sprite = data.documentSprite;
    }

    public void Open()
    {
        if (_preparedDocument == null)
        {
            Debug.LogWarning("[DocumentPopup] Nenhum documento preparado.");
            return;
        }

        _isOpen = true;
        gameObject.SetActive(true);
        if (overlay != null) overlay.SetActive(true);
        if (popupAnimator != null) popupAnimator.SetTrigger(openTrigger);
    }

    public void Close()
    {
        if (!_isOpen) return;

        _isOpen = false;

        if (popupAnimator != null)
            popupAnimator.SetTrigger(closeTrigger);
        else
            gameObject.SetActive(false);

        if (overlay != null) overlay.SetActive(false);
    }

    public void ClearDocument()
    {
        _preparedDocument = null;
        if (documentImage != null) documentImage.sprite = null;
    }
}
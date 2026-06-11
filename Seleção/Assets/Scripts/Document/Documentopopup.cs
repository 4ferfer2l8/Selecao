using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

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

    [Header("Fechar ao Carimbar")]
    [SerializeField] private float delayParaFechar = 0.8f; // espera antes de fechar sozinho

    private DocumentData _preparedDocument;
    private bool _isOpen = false;
    private bool _fechandoPorCarimbo = false;

    public bool IsOpen => _isOpen;

    private void Awake()
    {
        gameObject.SetActive(false);
        if (overlay != null) overlay.SetActive(false);
        if (closeButton != null) closeButton.onClick.AddListener(Close);
    }

    private void Update()
    {
        if (_isOpen && Keyboard.current.escapeKey.wasPressedThisFrame)
            Close();

        // fecha sozinho depois de carimbar (aprovar ou reprovar)
        if (_isOpen && !_fechandoPorCarimbo &&
            StampManager.instance != null && StampManager.instance.jaCarimbou)
        {
            _fechandoPorCarimbo = true;
            StartCoroutine(FecharComDelay());
        }
    }

    private IEnumerator FecharComDelay()
    {
        yield return new WaitForSeconds(delayParaFechar);
        Close();
        _fechandoPorCarimbo = false;
    }

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
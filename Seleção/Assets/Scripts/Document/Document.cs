using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class Document : MonoBehaviour, IPointerClickHandler {
    [Header("Selos Visuais")]
    public GameObject seloAprovado;
    public GameObject seloRejeitado;

    private DocumentData doc;

    [Header("Som do Carimbo")]
    [SerializeField] private string eventoCarimbo = "event:/Audios Jogo/SFX/Mouse_Papel_Cart�o_Teclado/Carimbo_Click";

    public void OnPointerClick(PointerEventData eventData) {
        if (!StampManager.instance.jaCarimbou)
        {
            AplicarCarimbo();
        }
    }

    void AplicarCarimbo() {
    StampManager.instance.jaCarimbou = true;

        if (StampManager.instance.currentStamp == StampType.Approved)
        {
            seloAprovado.SetActive(true);
            seloRejeitado.SetActive(false);
        }
        else
        {
            seloRejeitado.SetActive(true);
            seloAprovado.SetActive(false);
        }
    bool aprovou = StampManager.instance.currentStamp == StampType.Approved;

    if (aprovou) {
        seloAprovado.SetActive(true);
        seloRejeitado.SetActive(false);
    } else {
        seloRejeitado.SetActive(true);
        seloAprovado.SetActive(false);
    }

    // ─── Registra a decisão na progressão ───
    doc = DocumentManager.Instance.DocumentoAtual;
    if (doc != null && GerenciadorDeProgressao.instance != null)
    {
        bool ehPositivo = doc.category == DocumentCategory.Positive;
        GerenciadorDeProgressao.instance.RegistrarDecisao(aprovou, ehPositivo);
    }

    RuntimeManager.PlayOneShot(eventoCarimbo);
    Debug.Log("Carimbou com: " + StampManager.instance.currentStamp);
}

    public void ResetarCarimbo() {
        seloAprovado.SetActive(false);
        seloRejeitado.SetActive(false);
        StampManager.instance.jaCarimbou = false;
    }
}
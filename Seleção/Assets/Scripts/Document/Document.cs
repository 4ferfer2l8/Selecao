using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class Document : MonoBehaviour, IPointerClickHandler, IAcaoGamepad {
    [Header("Selos Visuais")]
    public GameObject seloAprovado;
    public GameObject seloRejeitado;

    private DocumentData doc;

    [Header("Som do Carimbo")]
    [SerializeField] private string eventoCarimbo = "event:/Audios Jogo/SFX/Mouse_Papel_Cartão_Teclado/Carimbo_Click";

    public void OnPointerClick(PointerEventData eventData) {
        if (!StampManager.instance.jaCarimbou)
        {
            AplicarCarimbo();
        }
    }

    public void AcionarGamepad() {
        if (!StampManager.instance.jaCarimbou) {
            AplicarCarimbo();
        }
    }

    void AplicarCarimbo() {
        StampManager.instance.jaCarimbou = true;

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

        // alimenta o sistema adaptativo
        if (doc != null && SistemaAdaptativo.instance != null)
        {
            bool acertou = (aprovou && doc.category == DocumentCategory.Positive) ||
                        (!aprovou && doc.category == DocumentCategory.Negative);
            bool eraPositivo = doc.category == DocumentCategory.Positive;
            SistemaAdaptativo.instance.RegistrarResultado(acertou, eraPositivo);
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
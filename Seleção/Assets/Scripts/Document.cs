using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class Document : MonoBehaviour, IPointerClickHandler {
    [Header("Selos Visuais")]
    public GameObject seloAprovado;
    public GameObject seloRejeitado;

    [Header("Som do Carimbo")]
    [SerializeField] private string eventoCarimbo = "event:/Seleção_Audios/SFX/Papel_Cartão_Mouse/Papel_Pegar_1";

    public void OnPointerClick(PointerEventData eventData) {
        if (!StampManager.instance.jaCarimbou) {
            AplicarCarimbo();
        }
    }

    void AplicarCarimbo() {
        StampManager.instance.jaCarimbou = true;

        if (StampManager.instance.currentStamp == StampType.Approved) {
            seloAprovado.SetActive(true);
            seloRejeitado.SetActive(false);
        } else {
            seloRejeitado.SetActive(true);
            seloAprovado.SetActive(false);
        }

        RuntimeManager.PlayOneShot(eventoCarimbo);
        Debug.Log("Carimbou com: " + StampManager.instance.currentStamp);
    }
}
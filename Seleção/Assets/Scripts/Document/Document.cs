using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class Document : MonoBehaviour, IPointerClickHandler {
    [Header("Selos Visuais")]
    public GameObject seloAprovado;
    public GameObject seloRejeitado;

    [Header("Som do Carimbo")]
    [SerializeField] private string eventoCarimbo = "event:/Audios Jogo/SFX/Mouse_Papel_Cartão_Teclado/Carimbo_Click";

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

        RuntimeManager.PlayOneShot(eventoCarimbo);
        Debug.Log("Carimbou com: " + StampManager.instance.currentStamp);
    }

    public void ResetarCarimbo() {
        seloAprovado.SetActive(false);
        seloRejeitado.SetActive(false);
        StampManager.instance.jaCarimbou = false;
    }
}
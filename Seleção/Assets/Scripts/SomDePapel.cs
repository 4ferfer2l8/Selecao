using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class SomDePapel : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {
    [Header("Eventos FMOD")]
    [SerializeField] private string eventoHover = "event:/Seleção_Audios/SFX/Papel_Cartão_Mouse/Papel_Pegar_1";
    [SerializeField] private string eventoAbrir = "event:/Seleção_Audios/SFX/Papel_Cartão_Mouse/Papel_Pegar_2";
    [SerializeField] private string eventoFechar = "event:/Seleção_Audios/SFX/Papel_Cartão_Mouse/Cartão_Pegar_1";

    [Header("Documento Expandido")]
    [SerializeField] private GameObject painelDocumento;

    private bool estaAberto = false;

    public void OnPointerEnter(PointerEventData eventData) {
        RuntimeManager.PlayOneShot(eventoHover);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!estaAberto) {
            estaAberto = true;
            RuntimeManager.PlayOneShot(eventoAbrir);
            AbrirDocumento();
        }
        // fechamento só pelo botão de fechar, não pelo clique no papel
    }

    public void FecharDocumentoExterno() {
        estaAberto = false;
        painelDocumento.SetActive(false);
        Debug.Log("Documento fechado");
    }

    private void AbrirDocumento() {
        painelDocumento.SetActive(true);
        Debug.Log("Documento aberto");
    }
}
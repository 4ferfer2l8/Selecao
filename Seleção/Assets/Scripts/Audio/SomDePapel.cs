using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;
using UnityEngine.UI;

public class SomDePapel : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IAcaoGamepad {
    [Header("Eventos FMOD")]
    [SerializeField] private string eventoHover = "event:/Seleção_Audios/SFX/Papel_Cartão_Mouse/Papel_Pegar_1";
    [SerializeField] private string eventoAbrir = "event:/Seleção_Audios/SFX/Papel_Cartão_Mouse/Papel_Pegar_2";
    [SerializeField] private string eventoFechar = "event:/Seleção_Audios/SFX/Papel_Cartão_Mouse/Cartão_Pegar_1";

    [Header("Documento Expandido")]
    [SerializeField] private GameObject painelDocumento;
    [SerializeField] private Image imagemDocumento;

    private bool estaAberto = false;
    private Individuo individuoAtual;

    public void DefinirIndividuo(Individuo individuo) {
        individuoAtual = individuo;
        estaAberto = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        RuntimeManager.PlayOneShot(eventoHover);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!estaAberto)
        {
            estaAberto = true;
            RuntimeManager.PlayOneShot(eventoAbrir);
            AbrirDocumento();
            eventData.Use();

            if (TutorialManager.instance != null)
                TutorialManager.instance.IniciarTutorial();
        }
    }

    public void AcionarGamepad() {
        if (!estaAberto)
        {
            estaAberto = true;
            RuntimeManager.PlayOneShot(eventoAbrir);
            AbrirDocumento();

            if (TutorialManager.instance != null)
                TutorialManager.instance.IniciarTutorial();
        }
    }

    private void AbrirDocumento() {
        if (individuoAtual == null)
        {
            Debug.LogWarning("Nenhum indivíduo definido ainda!");
            return;
        }

        if (individuoAtual.documento != null)
            imagemDocumento.sprite = individuoAtual.documento;

        painelDocumento.SetActive(true);
        Debug.Log("Documento aberto: " + individuoAtual.codigo);
    }

    public void NotificarFechamento() {
        estaAberto = false;
        RuntimeManager.PlayOneShot(eventoFechar);
        Debug.Log("Documento fechado");
    }
}
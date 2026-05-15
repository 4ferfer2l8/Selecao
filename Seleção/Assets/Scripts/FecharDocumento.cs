using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class FecharDocumento : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string eventoFechar = "event:/Seleção_Audios/SFX/Papel_Cartão_Mouse/Cartão_Pegar_1";
    [SerializeField] private SomDePapel somDePapel;

    public void OnPointerClick(PointerEventData eventData)
    {
        RuntimeManager.PlayOneShot(eventoFechar);
        somDePapel.FecharDocumentoExterno();
        gameObject.SetActive(false);
    }
}
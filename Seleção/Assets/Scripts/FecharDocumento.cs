using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class FecharDocumento : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string eventoFechar = "event:/Cartao_Pegar_1";
    [SerializeField] private SomDePapel somDePapel;
    [SerializeField] private GameObject painelDocumento; // arrasta o PainelDocumento aqui

    public void OnPointerClick(PointerEventData eventData)
    {
        RuntimeManager.PlayOneShot(eventoFechar);
        painelDocumento.SetActive(false); // desativa o painel pai, não o botão
        somDePapel.NotificarFechamento();
    }
}
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;
using UnityEngine.InputSystem;

public class SomDeCarimbo : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Eventos FMOD")]
    [SerializeField] private string eventoPegar    = "event:/Seleção_Audios/SFX/Carimbo/Carimbo_Pegar_1";
    [SerializeField] private string eventoCarimbar = "event:/Seleção_Audios/SFX/Carimbo/Carimbo_Click_1.1";
    [SerializeField] private string eventoPosar    = "event:/Seleção_Audios/SFX/Carimbo/Carimbo_Colocar_1";


    private bool estaSendoSeguro = false;
    private Vector3 posicaoOriginal;
    private Camera cam;

    private void Start()
    {
        posicaoOriginal = transform.position;
        cam = Camera.main;
    }

    private void Update()
    {
    // so carimba se estiver segurando o carimbo
    if (!estaSendoSeguro) return;

    if (Mouse.current.rightButton.wasPressedThisFrame)
    {
        RuntimeManager.PlayOneShot(eventoCarimbar);
        Debug.Log("Carimbo aplicado!");
    }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse em cima do carimbo");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) return;

        estaSendoSeguro = true;
        RuntimeManager.PlayOneShot(eventoPegar);
        Debug.Log("Carimbo pegado");
    }

    public void OnDrag(PointerEventData eventData)
{
    if (!estaSendoSeguro) return;

    Vector3 posicaoMouse = cam.ScreenToWorldPoint(
        new Vector3(eventData.position.x, eventData.position.y, 
        Mathf.Abs(cam.transform.position.z))
    );
    posicaoMouse.z = transform.position.z;
    transform.position = posicaoMouse;
}

    public void OnPointerUp(PointerEventData eventData)
    {
        
        if (eventData.button == PointerEventData.InputButton.Right) return;
    
        if (!estaSendoSeguro) return;

        estaSendoSeguro = false;
        RuntimeManager.PlayOneShot(eventoPosar);
        Debug.Log("Carimbo pousado na mesa");

        transform.position = posicaoOriginal;

        if (SoltoEmCimaDeDocumento())
        {
            RuntimeManager.PlayOneShot(eventoCarimbar);
            Debug.Log("Carimbo aplicado no documento");
        }
        else
        {
            RuntimeManager.PlayOneShot(eventoPosar);
            Debug.Log("Carimbo pousado na mesa");
        }

        transform.position = posicaoOriginal;
    }

    private bool SoltoEmCimaDeDocumento()
    {
        return false;
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderVolumeSetup : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private Slider slider;

    void OnEnable()
    {
        float volumeSalvo = PlayerPrefs.GetFloat("VolumeGeral", 1f);
        slider.SetValueWithoutNotify(volumeSalvo);

        if (ControleDeAudio.instance != null)
            ControleDeAudio.instance.DefinirVolumeGeral(volumeSalvo);
    }

    // Só aplica o volume quando o jogador SOLTA o clique,
    // evitando qualquer valor intermediário problemático (incluindo o "zero fantasma")
    public void OnPointerUp(PointerEventData eventData)
    {
        AplicarVolumeAtual();
    }

    private void AplicarVolumeAtual()
    {
        float valor = slider.value;

        if (ControleDeAudio.instance != null)
            ControleDeAudio.instance.DefinirVolumeGeral(valor);

        PlayerPrefs.SetFloat("VolumeGeral", valor);
    }
}
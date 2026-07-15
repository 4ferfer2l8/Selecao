using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FocoOpcoesGamepad : MonoBehaviour {
    [Header("Itens do painel: 0 = Fechar (X), 1 = Slider de Volume")]
    public Button botaoFechar;
    public SliderVolumeSetup sliderVolume;

    [Header("Indicador visual (seta) que acompanha o foco")]
    public GameObject indicadorFoco;

    [Header("Quanto o volume muda por aperto de D-pad")]
    public float passoVolume = 0.1f;

    private int indiceFoco = 0;

    void OnEnable() {
        indiceFoco = 0;
        if (indicadorFoco != null)
            indicadorFoco.SetActive(false);
    }

    void OnDisable() {
        if (indicadorFoco != null)
            indicadorFoco.SetActive(false);
    }

    void Update() {
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        var gp = Gamepad.current;
        if (gp == null) return;

        if (GamepadBindings.WasPressed(AcaoGamepad.AvancarFoco) || GamepadBindings.WasPressed(AcaoGamepad.VoltarFoco))
{
    indiceFoco = (indiceFoco == 0) ? 1 : 0;
    AtualizarIndicador();
}

if (indiceFoco == 0)
{
    if (GamepadBindings.WasPressed(AcaoGamepad.Confirmar))
    {
        botaoFechar.onClick.Invoke();
    }
}
else
{
    if (GamepadBindings.WasPressed(AcaoGamepad.VolumeCima))
    {
        sliderVolume.AjustarVolumeGamepad(passoVolume);
        AtualizarIndicador();
    }
    else if (GamepadBindings.WasPressed(AcaoGamepad.VolumeBaixo))
    {
        sliderVolume.AjustarVolumeGamepad(-passoVolume);
        AtualizarIndicador();
    }
}

    void AtualizarIndicador() {
        if (indicadorFoco == null) return;
        indicadorFoco.SetActive(true);
        Transform alvo = (indiceFoco == 0) ? botaoFechar.transform : sliderVolume.transform;
        indicadorFoco.transform.position = alvo.position;
    }
}}
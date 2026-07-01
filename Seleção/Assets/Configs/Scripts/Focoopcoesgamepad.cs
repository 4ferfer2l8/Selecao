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
        AtualizarIndicador();
    }

    void Update() {
        // impede o EventSystem padrão do Unity de "selecionar" botões sozinho
        // e disparar clique fantasma quando a gente aperta B
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        var gp = Gamepad.current;
        if (gp == null) return;

        if (gp.rightShoulder.wasPressedThisFrame || gp.rightTrigger.wasPressedThisFrame)
        {
            indiceFoco = (indiceFoco == 0) ? 1 : 0;
            AtualizarIndicador();
        }

        if (indiceFoco == 0)
        {
            if (gp.buttonEast.wasPressedThisFrame)
            {
                botaoFechar.onClick.Invoke();
            }
        }
        else
        {
            if (gp.dpad.right.wasPressedThisFrame)
            {
                sliderVolume.AjustarVolumeGamepad(passoVolume);
            }
            else if (gp.dpad.left.wasPressedThisFrame)
            {
                sliderVolume.AjustarVolumeGamepad(-passoVolume);
            }
        }
    }

    void AtualizarIndicador() {
        if (indicadorFoco == null) return;
        indicadorFoco.SetActive(true);
        Transform alvo = (indiceFoco == 0) ? botaoFechar.transform : sliderVolume.transform;
        indicadorFoco.transform.position = alvo.position;
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class FocoGamepadDocumento : MonoBehaviour {
    [Header("Ordem: Documento -> Carimbo Aprovar -> Carimbo Rejeitar")]
    public Transform[] alvosFoco;

    [Header("Indicador visual que passeia entre documento e carimbos")]
    public GameObject indicadorFoco;

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
        

        if (GamepadBindings.WasPressed(AcaoGamepad.AvancarFoco))
{
    indiceFoco = (indiceFoco + 1) % alvosFoco.Length;
    AtualizarIndicador();
}
else if (GamepadBindings.WasPressed(AcaoGamepad.VoltarFoco))
{
    indiceFoco = (indiceFoco - 1 + alvosFoco.Length) % alvosFoco.Length;
    AtualizarIndicador();
}

if (GamepadBindings.WasPressed(AcaoGamepad.Confirmar))
{
    var alvo = alvosFoco[indiceFoco];
    var acao = alvo.GetComponent<IAcaoGamepad>();
    if (acao != null)
    {
        acao.AcionarGamepad();
    }
}

    void AtualizarIndicador() {
        if (indicadorFoco == null) return;
        indicadorFoco.SetActive(true);
        indicadorFoco.transform.position = alvosFoco[indiceFoco].position;
    }
}}
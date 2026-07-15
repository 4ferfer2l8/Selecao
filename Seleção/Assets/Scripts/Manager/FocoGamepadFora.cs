using UnityEngine;
using UnityEngine.InputSystem;

public class FocoGamepadFora : MonoBehaviour {
    [Header("O papel da mesa (objeto com o SomDePapel)")]
    public Transform papelMesa;

    [Header("Indicador visual que fica sempre em cima do papel")]
    public GameObject indicadorFoco;

    private float tempoAtivado;

    void OnEnable() {
        if (indicadorFoco != null)
            indicadorFoco.SetActive(false);

        tempoAtivado = Time.unscaledTime;
    }

    void OnDisable() {
        if (indicadorFoco != null)
            indicadorFoco.SetActive(false);
    }

    void Update() {
        if (Time.unscaledTime - tempoAtivado < 0.2f) return;

        
        if (GamepadBindings.WasPressed(AcaoGamepad.AvancarFoco) || GamepadBindings.WasPressed(AcaoGamepad.VoltarFoco))
{
    AtualizarIndicador();
}

if (GamepadBindings.WasPressed(AcaoGamepad.Confirmar))
{
    var acao = papelMesa.GetComponent<IAcaoGamepad>();
    if (acao != null)
    {
        acao.AcionarGamepad();
    }
}
    }

    void AtualizarIndicador() {
        if (indicadorFoco == null || papelMesa == null) return;
        indicadorFoco.SetActive(true);
        Vector3 posTela = Camera.main.WorldToScreenPoint(papelMesa.position);
        indicadorFoco.transform.position = posTela;
    }
}
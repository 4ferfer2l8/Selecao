using UnityEngine;
using UnityEngine.InputSystem;

public class FocoGamepad : MonoBehaviour {
    [Header("Ordem do foco: Papel -> Documento -> Carimbo de cima -> Carimbo de baixo")]
    public Transform[] alvosFoco;

    [Header("Indicador visual (luz/partícula) que acompanha o foco")]
    public GameObject indicadorFoco;

    private int indiceFoco = 0;

    void Start() {
        AtualizarIndicador();
    }

    void Update() {
        var gp = Gamepad.current;
        if (gp == null || alvosFoco.Length == 0) return;

        if (gp.rightShoulder.wasPressedThisFrame)
        {
            indiceFoco = (indiceFoco + 1) % alvosFoco.Length;
            AtualizarIndicador();
        }
        else if (gp.rightTrigger.wasPressedThisFrame)
        {
            indiceFoco = (indiceFoco - 1 + alvosFoco.Length) % alvosFoco.Length;
            AtualizarIndicador();
        }

        if (gp.buttonEast.wasPressedThisFrame)
        {
            var alvo = alvosFoco[indiceFoco];
            var acao = alvo.GetComponent<IAcaoGamepad>();
            if (acao != null)
            {
                acao.AcionarGamepad();
            }
        }
    }

    void AtualizarIndicador() {
        if (indicadorFoco == null) return;
        indicadorFoco.SetActive(true);

        Transform alvo = alvosFoco[indiceFoco];

        if (alvo.GetComponent<RectTransform>() != null)
        {
            indicadorFoco.transform.position = alvo.position;
        }
        else
        {
            Vector3 posTela = Camera.main.WorldToScreenPoint(alvo.position);
            indicadorFoco.transform.position = posTela;
        }
    }
}
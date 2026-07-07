using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FocoMenuGamepad : MonoBehaviour {
    [Header("Botões do menu, na ordem que quer navegar (ex: Jogar, Opções, Sair)")]
    public Button[] botoes;

    [Header("Indicador visual (seta) que acompanha o foco")]
    public GameObject indicadorFoco;

    private int indiceFoco = 0;

    void OnEnable() {
        if (indicadorFoco != null)
            indicadorFoco.SetActive(false);
    }

    void Update() {
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
            EventSystem.current.SetSelectedGameObject(null);

        var gp = Gamepad.current;
        if (gp == null || botoes.Length == 0) return;

        if (gp.rightShoulder.wasPressedThisFrame) // R1
        {
            indiceFoco = (indiceFoco + 1) % botoes.Length;
            AtualizarIndicador();
        }
        else if (gp.rightTrigger.wasPressedThisFrame) // R2
        {
            indiceFoco = (indiceFoco - 1 + botoes.Length) % botoes.Length;
            AtualizarIndicador();
        }

        if (gp.buttonEast.wasPressedThisFrame) // B
        {
            botoes[indiceFoco].onClick.Invoke();
        }
    }

    void AtualizarIndicador() {
        if (indicadorFoco == null) return;
        indicadorFoco.SetActive(true);
        indicadorFoco.transform.SetAsLastSibling(); // garante que renderiza por cima de tudo
        indicadorFoco.transform.position = botoes[indiceFoco].transform.position;
    }
}
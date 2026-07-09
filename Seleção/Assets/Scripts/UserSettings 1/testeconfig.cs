using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script temporário pra testar as configurações pelo teclado.
/// Remover antes da build final.
/// </summary>
public class TesteConfiguracoes : MonoBehaviour
{
    private GameSettingsManager manager;

    private void Start()
    {
        manager = GameSettingsManager.Instance;
    }

    private void Update()
    {
        if (manager == null) return;

        // V → liga/desliga Vsync
        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            bool novo = !manager.Dados.vsync;
            manager.AplicarVsync(novo);
        }

        // F → liga/desliga Full Screen
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            bool novo = !manager.Dados.fullScreen;
            manager.AplicarFullScreen(novo);
        }

        // seta direita → próxima resolução
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            int novo = manager.Dados.indiceResolucao + 1;
            if (novo >= manager.GetResolucoesDisponiveis().Length) novo = 0;
            manager.AplicarResolucao(novo);
        }

        // seta esquerda → resolução anterior
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            int novo = manager.Dados.indiceResolucao - 1;
            if (novo < 0) novo = manager.GetResolucoesDisponiveis().Length - 1;
            manager.AplicarResolucao(novo);
        }

        // S → salva
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            manager.SalvarConfiguracoes();
        }
    }
}
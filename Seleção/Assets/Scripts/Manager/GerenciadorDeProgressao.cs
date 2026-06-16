using UnityEngine;

/// <summary>
/// Rastreia a balança moral do jogador ao longo do jogo.
/// Valores positivos pesam pro final bom, negativos pro final ruim.
/// </summary>
public class GerenciadorDeProgressao : MonoBehaviour
{
    public static GerenciadorDeProgressao instance;

    [Header("Balança Moral")]
    [SerializeField] private int balanca = 0; // sobe = bom, desce = ruim

    [Header("Contadores")]
    [SerializeField] private int acertos = 0;
    [SerializeField] private int erros = 0;
    [SerializeField] private int totalDecisoes = 0;

    void Awake()
    {
        instance = this;
    }


    /// <param name="aprovou">true se aprovou, false se reprovou</param>
    /// <param name="documentoPositivo">true se o documento era positivo</param>
    public void RegistrarDecisao(bool aprovou, bool documentoPositivo)
    {
        totalDecisoes++;

        // acerto = aprovar positivo OU reprovar negativo
        bool acertou = (aprovou && documentoPositivo) || (!aprovou && !documentoPositivo);

        if (acertou)
        {
            balanca++;
            acertos++;
            Debug.Log($"✓ Decisão CERTA — balança subiu pra {balanca}");
        }
        else
        {
            balanca--;
            erros++;
            Debug.Log($"✗ Decisão ERRADA — balança desceu pra {balanca}");
        }

        MostrarEstadoAtual();
    }

    private void MostrarEstadoAtual()
    {
        string tendencia;

        if (balanca > 2)        tendencia = "tendendo ao FINAL BOM";
        else if (balanca < -2)  tendencia = "tendendo ao FINAL RUIM";
        else                    tendencia = "MEIO TERMO";

        Debug.Log($"─── Progressão: {tendencia} (balança={balanca}, acertos={acertos}, erros={erros}, total={totalDecisoes}) ───");
    }

    /// <summary>
    /// Calcula qual final o jogador vai ver. Chamado no fim do jogo.
    /// </summary>
    public TipoFinal CalcularFinal()
    {
        if (balanca > 2)       return TipoFinal.Bom;
        else if (balanca < -2) return TipoFinal.Ruim;
        else                   return TipoFinal.Neutro;
    }
}

public enum TipoFinal { Bom, Neutro, Ruim }
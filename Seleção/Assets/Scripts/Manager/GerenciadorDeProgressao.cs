using UnityEngine;
using UnityEngine.SceneManagement;


public class GerenciadorDeProgressao : MonoBehaviour
{
    public static GerenciadorDeProgressao instance;

    [Header("Balança Moral")]
    [SerializeField] private int balanca = 0;

    [Header("Contadores")]
    [SerializeField] private int acertos = 0;
    [SerializeField] private int erros = 0;
    [SerializeField] private int totalDecisoes = 0;

    [Header("Limite da Fase")]
    [SerializeField] private int limiteDecisoes = 5; // quantos NPCs até acabar a fase

    [Header("Cena Final")]
    [SerializeField] private string SegundaFase;

    void Awake()
    {
        instance = this;
    }

    public void RegistrarDecisao(bool aprovou, bool documentoPositivo)
    {
        totalDecisoes++;

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

    public bool FaseAcabou()
    {
        return totalDecisoes >= limiteDecisoes;
    }

    private void MostrarEstadoAtual()
    {
        string tendencia;

        if (balanca > 2)        tendencia = "tendendo ao FINAL BOM";
        else if (balanca < -2)  tendencia = "tendendo ao FINAL RUIM";
        else                    tendencia = "MEIO TERMO";

        Debug.Log($"─── Progressão: {tendencia} (balança={balanca}, acertos={acertos}, erros={erros}, total={totalDecisoes}) ───");
    }

    public void FinalizarFase()
    {
        TipoFinal final = CalcularFinal();

        Debug.Log($"═══════════════════════════════");
        Debug.Log($"FIM DA FASE — Final alcançado: {final}");
        Debug.Log($"═══════════════════════════════");

        if (!string.IsNullOrEmpty(SegundaFase))
            SceneManager.LoadScene(SegundaFase);
    }

    public TipoFinal CalcularFinal()
    {
        if (balanca > 2)       return TipoFinal.Bom;
        else if (balanca < -2) return TipoFinal.Ruim;
        else                   return TipoFinal.Neutro;
    }
}

public enum TipoFinal { Bom, Neutro, Ruim }
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDeFases : MonoBehaviour
{
    public static ControladorDeFases instance;

    [Header("Cenas das Fases (na ordem)")]
    [SerializeField] private string[] cenasFases;

    [Header("Cenas de Final")]
    [SerializeField] private string cenaFinalBom = "FinalBom";
    [SerializeField] private string cenaFinalNeutro = "FinalNeutro";
    [SerializeField] private string cenaFinalRuim = "FinalRuim";

    [Header("Limites do Final (balança acumulada)")]
    [SerializeField] private int limiteFinalBom = 3;   // acima disso = bom
    [SerializeField] private int limiteFinalRuim = -3; // abaixo disso = ruim

    private int faseAtual = 0;

    private int balancaTotal = 0;
    private int acertosTotais = 0;
    private int errosTotais = 0;
    private int decisoesTotais = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void RegistrarDecisao(bool acertou)
    {
        decisoesTotais++;

        if (acertou)
        {
            balancaTotal++;
            acertosTotais++;
        }
        else
        {
            balancaTotal--;
            errosTotais++;
        }

        Debug.Log($"[Total acumulado] balança={balancaTotal}, acertos={acertosTotais}, erros={errosTotais}, decisões={decisoesTotais}");
    }

    public void ConcluirFase()
    {
        faseAtual++;

        if (faseAtual < cenasFases.Length)
        {
            Debug.Log($"Avançando para a fase {faseAtual + 1}");
            SceneManager.LoadScene(cenasFases[faseAtual]);
        }
        else
        {
            TipoFinal final = CalcularFinalTotal();
            Debug.Log($"Jogo concluído. Final: {final} (balança total={balancaTotal})");
            CarregarFinal(final);
        }
    }

    private TipoFinal CalcularFinalTotal()
    {
        if (balancaTotal >= limiteFinalBom)  return TipoFinal.Bom;
        if (balancaTotal <= limiteFinalRuim) return TipoFinal.Ruim;
        return TipoFinal.Neutro;
    }

    private void CarregarFinal(TipoFinal final)
    {
        switch (final)
        {
            case TipoFinal.Bom:    SceneManager.LoadScene(cenaFinalBom);    break;
            case TipoFinal.Ruim:   SceneManager.LoadScene(cenaFinalRuim);   break;
            default:               SceneManager.LoadScene(cenaFinalNeutro); break;
        }
    }

    public enum TipoFinal { Bom, Neutro, Ruim }
}
using UnityEngine;

public class GerenciadorDeProgressao : MonoBehaviour
{
    public static GerenciadorDeProgressao instance;

    [Header("Limite da Fase")]
    [SerializeField] private int limiteDecisoes = 5;

    private int decisoesNaFase = 0;

    void Awake()
    {
        instance = this;
    }

    public void RegistrarDecisao(bool aprovou, bool documentoPositivo)
    {
        decisoesNaFase++;

        bool acertou = (aprovou && documentoPositivo) || (!aprovou && !documentoPositivo);

        // repassa pro placar acumulado que sobrevive entre fases
        if (ControladorDeFases.instance != null)
            ControladorDeFases.instance.RegistrarDecisao(acertou);

        Debug.Log(acertou ? "✓ Decisão CERTA nesta fase" : "✗ Decisão ERRADA nesta fase");
    }

    public bool FaseAcabou()
    {
        return decisoesNaFase >= limiteDecisoes;
    }

    public void FinalizarFase()
    {
        Debug.Log("─── Fim da fase, entregando ao controlador ───");
        if (ControladorDeFases.instance != null)
            ControladorDeFases.instance.ConcluirFase();
    }

    public enum TipoFinal { Bom, Neutro, Ruim }
}
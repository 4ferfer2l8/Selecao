using UnityEngine;

public class GeradorDeAparencia : MonoBehaviour
{
    [Header("Banco de Dados")]
    public BancoDeRostos bancoDeRostos;

    [Header("Camadas do Rosto")]
    public SpriteRenderer camadaCorpo;
    public SpriteRenderer camadaOlhos;
    public SpriteRenderer camadaNariz;
    public SpriteRenderer camadaBoca;

    public bool UltimoEhMulher { get; private set; }

    public void GerarAparenciaAleatoria()
    {
        if (bancoDeRostos == null)
        {
            Debug.LogError("BancoDeRostos não atribuído no Inspector!");
            return;
        }

        // sorteia o gênero
        bool ehMulher = Random.value < 0.5f;
        UltimoEhMulher = ehMulher;

        // seleciona as listas do gênero sorteado
        var corpos  = ehMulher ? bancoDeRostos.corposFemininos   : bancoDeRostos.corposMasculinos;
        var olhos   = ehMulher ? bancoDeRostos.olhosFemininos    : bancoDeRostos.olhosMasculinos;
        var narizes = ehMulher ? bancoDeRostos.narizesFemininos  : bancoDeRostos.narizesMasculinos;
        var bocas   = ehMulher ? bancoDeRostos.bocasFemininas    : bancoDeRostos.bocasMasculinas;

        camadaCorpo.sprite = bancoDeRostos.GetAleatorio(corpos);
        camadaOlhos.sprite = bancoDeRostos.GetAleatorio(olhos);
        camadaNariz.sprite = bancoDeRostos.GetAleatorio(narizes);
        camadaBoca.sprite  = bancoDeRostos.GetAleatorio(bocas);
    }
}
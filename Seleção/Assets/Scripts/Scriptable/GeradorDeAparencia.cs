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

    public void GerarAparenciaAleatoria()
    {
        if (bancoDeRostos == null)
        {
            Debug.LogError("BancoDeRostos não atribuído no Inspector!");
            return;
        }

        camadaCorpo.sprite = bancoDeRostos.GetAleatorio(bancoDeRostos.corpos);
        camadaOlhos.sprite = bancoDeRostos.GetAleatorio(bancoDeRostos.olhos);
        camadaNariz.sprite = bancoDeRostos.GetAleatorio(bancoDeRostos.narizes);
        camadaBoca.sprite  = bancoDeRostos.GetAleatorio(bancoDeRostos.bocas);
    }
}
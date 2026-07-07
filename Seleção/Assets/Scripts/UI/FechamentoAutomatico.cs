using UnityEngine;
using System.Collections;

/// <summary>
/// Fecha o painel do documento automaticamente depois que o jogador carimba.
/// </summary>
public class FechamentoAutomatico : MonoBehaviour
{
    [Header("Delay (segundos)")]
    [SerializeField] private float delayParaFechar = 0.8f;

    [Header("Referência pro papel da mesa (avisa que fechou)")]
    [SerializeField] private SomDePapel somDePapel;

    private bool fechando = false;

    private void OnEnable()
    {
        // toda vez que o painel abre, reseta o controle
        fechando = false;
    }

    private void Update()
    {
        // só age se o painel estiver aberto (ativo) e o jogador tiver carimbado
        if (!fechando &&
            StampManager.instance != null &&
            StampManager.instance.jaCarimbou)
        {
            fechando = true;
            StartCoroutine(FecharComDelay());
        }
    }

    private IEnumerator FecharComDelay()
    {
        yield return new WaitForSeconds(delayParaFechar);

        // avisa o papel da mesa que o documento fechou (reseta estaAberto e religa o foco de fora)
        if (somDePapel != null)
            somDePapel.NotificarFechamento();

        gameObject.SetActive(false); // fecha o próprio painel
    }
}
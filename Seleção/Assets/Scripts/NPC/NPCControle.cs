using UnityEngine;
using System.Collections;

public class NPCControle : MonoBehaviour {
    public float velocidade = 2f;
    public Transform pontoParada;
    public Transform pontoSaida;

    [Header("Delay (segundos)")]
    [SerializeField] private float delayAntesDeSair = 1f; // espera antes do NPC ir embora

    private bool liberado = false;
    private bool chegouNaParada = false;
    private bool processandoSaida = false;
    private Document documento;

    void Update() {
        if (!chegouNaParada)
        {
            MoverPara(pontoParada.position);
            if (Vector2.Distance(transform.position, pontoParada.position) < 0.1f)
            {
                chegouNaParada = true;
                documento.ResetarCarimbo();
            }
        }
        else if (!liberado)
        {
            // assim que carimbar (aprovar OU reprovar), libera sozinho — sem tecla E
            if (!processandoSaida && StampManager.instance.jaCarimbou)
            {
                processandoSaida = true;
                StartCoroutine(SequenciaDeSaida());
            }
        }
        else
        {
            MoverPara(pontoSaida.position);
        }
    }

    private IEnumerator SequenciaDeSaida()
    {
        yield return new WaitForSeconds(delayAntesDeSair);
        Liberar();

        // espera o NPC chegar no ponto de saída antes de checar o final
        yield return new WaitForSeconds(5f);

        // se esse era o último NPC da fase, dispara o final
        if (GerenciadorDeProgressao.instance != null &&
            GerenciadorDeProgressao.instance.FaseAcabou())
        {
            GerenciadorDeProgressao.instance.FinalizarFase();
        }

        Destroy(gameObject);
    }

    void MoverPara(Vector2 destino) {
        transform.position = Vector2.MoveTowards(
            transform.position,
            destino,
            velocidade * Time.deltaTime
        );
    }

    public void Liberar() {
        liberado = true;
    }

    void Awake() {
        pontoParada = GameObject.Find("PontoParada").transform;
        pontoSaida = GameObject.Find("PontoSaida").transform;
        documento = FindObjectOfType<Document>(true);
    }
}
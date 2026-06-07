using UnityEngine;
using UnityEngine.InputSystem;

public class NPCControle : MonoBehaviour {
    public float velocidade = 2f;
    public Transform pontoParada;
    public Transform pontoSaida;
    private bool liberado = false;
    private bool chegouNaParada = false;
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
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (StampManager.instance.jaCarimbou)
                {
                    Liberar();
                }
                else
                {
                    NotificacaoUI.instance.Mostrar("Carimbe o documento primeiro!");
                }
            }
        }
        else
        {
            MoverPara(pontoSaida.position);
            Destroy(gameObject, 8f);
        }
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
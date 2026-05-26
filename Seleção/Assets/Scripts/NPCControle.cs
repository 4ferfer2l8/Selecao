using UnityEngine;
using UnityEngine.InputSystem;

public class NPCControle : MonoBehaviour {
    public float velocidade = 2f;

    public Transform pontoParada;
    public Transform pontoSaida;

    private bool liberado = false;
    private bool chegouNaParada = false;

    void Update() {
        if (!chegouNaParada) {
            MoverPara(pontoParada.position);

            if (Vector2.Distance(transform.position, pontoParada.position) < 0.1f) {
                chegouNaParada = true;
            }
        } else if (!liberado) {
            // 👇 INPUT NOVO
            if (Keyboard.current.eKey.wasPressedThisFrame) {
                Liberar();
            }
        } else {
            MoverPara(pontoSaida.position);
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
}
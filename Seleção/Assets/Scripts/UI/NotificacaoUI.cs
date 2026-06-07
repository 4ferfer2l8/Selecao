using UnityEngine;
using TMPro;
using System.Collections;

public class NotificacaoUI : MonoBehaviour {
    public static NotificacaoUI instance;

    public GameObject painel;
    public TextMeshProUGUI texto;
    private Coroutine coroutineAtual;

    void Awake() {
        instance = this;
        painel.SetActive(false);
    }

    public void Mostrar(string msg, float duracao = 3f) {
        if (coroutineAtual != null)
            StopCoroutine(coroutineAtual);
        texto.text = msg;
        painel.SetActive(true);
        coroutineAtual = StartCoroutine(Esconder(duracao));
    }

    IEnumerator Esconder(float duracao) {
        yield return new WaitForSeconds(duracao);
        painel.SetActive(false);
    }
}
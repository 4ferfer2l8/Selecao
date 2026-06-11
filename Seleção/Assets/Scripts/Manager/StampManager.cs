using UnityEngine;

public enum StampType { Approved, Rejected }

public class StampManager : MonoBehaviour {
    public static StampManager instance;
    public StampType currentStamp;
    public bool jaCarimbou = false;

    void Awake() { instance = this; }

    public void SelectStamp(StampType type) {
        currentStamp = type;
        Debug.Log("Selecionou: " + type);
    }

    // chamado quando o jogador efetivamente carimba o documento
    public void Carimbar() {
        jaCarimbou = true;
        Debug.Log("Documento carimbado: " + currentStamp);
    }

    // chamado quando um novo NPC chega, pra resetar o estado
    public void ResetarCarimbo() {
        jaCarimbou = false;
        Debug.Log("Carimbo resetado para o próximo indivíduo.");
    }
}
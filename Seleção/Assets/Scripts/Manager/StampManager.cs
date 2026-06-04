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
}
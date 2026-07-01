using UnityEngine;
using UnityEngine.EventSystems;

public class StampSelector : MonoBehaviour, IPointerClickHandler, IAcaoGamepad {
    public StampType stampType;

    public void AcionarGamepad() {
        StampManager.instance.SelectStamp(stampType);
        Debug.Log("Carimbo selecionado (gamepad): " + stampType);
    }

    public void OnPointerClick(PointerEventData eventData) {
        StampManager.instance.SelectStamp(stampType);
        Debug.Log("Carimbo selecionado: " + stampType);
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class StampSelector : MonoBehaviour, IPointerClickHandler {
    public StampType stampType;

    public void OnPointerClick(PointerEventData eventData) {
        StampManager.instance.SelectStamp(stampType);
        Debug.Log("Carimbo selecionado: " + stampType);
    }
}
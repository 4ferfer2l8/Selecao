using UnityEngine;
using UnityEngine.InputSystem;

public class Document : MonoBehaviour {
    void Update() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == gameObject) {
                Debug.Log("Carimbou com: " + StampManager.instance.currentStamp);
            }
        }
    }
}
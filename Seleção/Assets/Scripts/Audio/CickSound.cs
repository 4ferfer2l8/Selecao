using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class CickSound : MonoBehaviour {
    [SerializeField] private EventReference soundEvent;

    void Update() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject) {
                RuntimeManager.PlayOneShot(soundEvent, transform.position);
            }
        }
    }
}
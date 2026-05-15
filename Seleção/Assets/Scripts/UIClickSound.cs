using UnityEngine;
using FMODUnity;

public class UIClickSound : MonoBehaviour {
    [SerializeField] private EventReference soundEvent;

    public void PlaySound() {
        RuntimeManager.PlayOneShot(soundEvent);
    }
}
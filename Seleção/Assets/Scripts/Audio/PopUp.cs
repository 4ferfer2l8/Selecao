using UnityEngine;
using FMODUnity;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField]
    private EventReference clickSound;

    public void PlayClickSound()
    {
        RuntimeManager.PlayOneShot(clickSound);
    }
}
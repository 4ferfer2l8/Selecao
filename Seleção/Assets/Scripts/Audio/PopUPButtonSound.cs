using UnityEngine;
using FMODUnity;

public class PopUPButtonSound : MonoBehaviour
{
    [SerializeField]
    private EventReference clickSound;

    public void PlayClickSound()
    {
        RuntimeManager.PlayOneShot(clickSound);
    }
}
using UnityEngine;
using FMODUnity;

public class PopUpButtonSound : MonoBehaviour
{
    [SerializeField]
    private EventReference clickSound;

    public void PlayClickSound()
    {
        RuntimeManager.PlayOneShot(clickSound);
    }
}
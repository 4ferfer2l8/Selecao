using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class ControleDeAudio : MonoBehaviour
{
    public static ControleDeAudio instance;

    private Bus busMaster;

    private const string CHAVE_VOLUME_GERAL = "VolumeGeral";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        busMaster = RuntimeManager.GetBus("bus:/");

        float geral = PlayerPrefs.GetFloat(CHAVE_VOLUME_GERAL, 1f);
        busMaster.setVolume(geral);
    }

    public void DefinirVolumeGeral(float valor)
    {
        busMaster.setVolume(valor);
        PlayerPrefs.SetFloat(CHAVE_VOLUME_GERAL, valor);
    }

    public float ObterVolumeSalvo()
    {
        return PlayerPrefs.GetFloat(CHAVE_VOLUME_GERAL, 1f);
    }
}
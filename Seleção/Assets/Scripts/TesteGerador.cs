using UnityEngine;
using UnityEngine.InputSystem;

public class TesteGerador : MonoBehaviour {
    public GameObject npcPrefab;
    public Transform pontoSpawn;

    private GeradorDeIndividuos geradorDeIndividuos;

    void Start() {
        geradorDeIndividuos = GetComponent<GeradorDeIndividuos>();
        GerarNovoNPC();
    }

    void Update() {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            GerarNovoNPC();
    }

    void GerarNovoNPC() {
        GameObject npc = Instantiate(npcPrefab, pontoSpawn.position, Quaternion.identity);

        // 👇 AQUI É O LUGAR CERTO
        NPCControle controle = npc.GetComponent<NPCControle>();
        controle.pontoParada = GameObject.Find("PontoParada").transform;
        controle.pontoSaida = GameObject.Find("PontoSaida").transform;

        GeradorDeAparencia aparencia = npc.GetComponent<GeradorDeAparencia>();
        aparencia.GerarAparenciaAleatoria();

        Individuo individuo = geradorDeIndividuos.GerarIndividuo();

        Debug.Log($"NPC criado: {individuo.codigo}");
    }
}
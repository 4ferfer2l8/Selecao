using UnityEngine;
using UnityEngine.InputSystem;

public class TesteGerador : MonoBehaviour {
    public GameObject npcPrefab;
    public Transform pontoSpawn;
    public SomDePapel somDePapel; // arrasta o PapelMesaPH_0 aqui no Inspector

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

        NPCControle controle = npc.GetComponent<NPCControle>();
        controle.pontoParada = GameObject.Find("PontoParada").transform;
        controle.pontoSaida = GameObject.Find("PontoSaida").transform;

        GeradorDeAparencia aparencia = npc.GetComponent<GeradorDeAparencia>();
        aparencia.GerarAparenciaAleatoria();

        Individuo individuo = geradorDeIndividuos.GerarIndividuo();
        somDePapel.DefinirIndividuo(individuo); // passa o documento sorteado pro papel

        Debug.Log($"NPC criado: {individuo.codigo}");
    }
}
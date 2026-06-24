using UnityEngine;
using UnityEngine.InputSystem;

public class TesteGerador : MonoBehaviour {
    public GameObject npcPrefab;
    public Transform pontoSpawn;
    public SomDePapel somDePapel;

    private GeradorDeIndividuos geradorDeIndividuos;
    private GameObject npcAtual;

    void Start() {
        geradorDeIndividuos = GetComponent<GeradorDeIndividuos>();
        GerarNovoNPC();
    }

    void Update() {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (npcAtual == null)
            {
                GerarNovoNPC();
            }
            else
            {
                NotificacaoUI.instance.Mostrar("Aguarde o candidato atual sair!");
            }
        }
    }

    void GerarNovoNPC() {
    npcAtual = Instantiate(npcPrefab, pontoSpawn.position, Quaternion.identity);

    NPCControle controle = npcAtual.GetComponent<NPCControle>();
    controle.pontoParada = GameObject.Find("PontoParada").transform;
    controle.pontoSaida = GameObject.Find("PontoSaida").transform;

    GeradorDeAparencia aparencia = npcAtual.GetComponent<GeradorDeAparencia>();
    aparencia.GerarAparenciaAleatoria();

    Individuo individuo = geradorDeIndividuos.GerarIndividuo();
    somDePapel.DefinirIndividuo(individuo);

    DocumentManager.Instance.OnNovoIndividuo(individuo);

    Debug.Log($"NPC criado: {individuo.codigo}");
}
}
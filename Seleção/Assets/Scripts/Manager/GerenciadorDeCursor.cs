using UnityEngine;

public class GerenciadorDeCursor : MonoBehaviour
{
    public static GerenciadorDeCursor instance;

    [Header("Conjuntos de Cor (0=Normal, 1=Verde, 2=Rosa)")]
    [SerializeField] private ConjuntoDeCor[] cores;

    [Header("Hotspot")]
    [SerializeField] private Vector2 hotspot = Vector2.zero;

    // estado da interação
    private bool estaArrastando = false;
    private bool sobreInterativo = false;

    // preferências do jogador (vêm das configs)
    private int corAtual = 0;
    private bool tamanhoGrande = false; // false = default, true = grande

    void Awake()
    {
        if (instance != null && instance != this)
            Debug.LogError($"[Cursor] DUPLICADO! Já havia um em '{instance.gameObject.name}'. " +
                        $"Este é '{gameObject.name}'.");

        instance = this;
        Debug.Log($"[Cursor] instance registrado em: '{gameObject.name}'");
    }

    void Start()
    {
        AtualizarCursor();
    }

    // ─── API de interação ───

    public void EntrouEmInterativo()
    {
        sobreInterativo = true;
        AtualizarCursor();
    }

    public void SaiuDeInterativo()
    {
        sobreInterativo = false;
        AtualizarCursor();
    }

    public void ComecouArrastar()
    {
        estaArrastando = true;
        AtualizarCursor();
    }

    public void ParouArrastar()
    {
        estaArrastando = false;
        AtualizarCursor();
    }

    // ─── API de configuração ───

    public void DefinirCor(int indiceCor)
    {
        Debug.Log($"[Cursor] DefinirCor({indiceCor}) chegou em '{gameObject.name}' — " +
                $"cores.Length = {(cores == null ? "NULO" : cores.Length.ToString())}");

        corAtual = indiceCor;
        AtualizarCursor();
    }

    public void DefinirTamanho(bool grande)
    {
        tamanhoGrande = grande;
        AtualizarCursor();
        Debug.Log($"Tamanho do cursor: {(grande ? "Grande" : "Default")}");
    }

    // ─── Lógica interna ───

    private void AtualizarCursor()
    {
        if (cores == null || cores.Length == 0)
        {
            Debug.LogWarning("[Cursor] Array 'cores' VAZIO neste GerenciadorDeCursor!");
            return;
        }

        int cor = Mathf.Clamp(corAtual, 0, cores.Length - 1);
        ConjuntoDeCor conjunto = cores[cor];

        ConjuntoDeMaos maos = tamanhoGrande ? conjunto.tamanhoGrande : conjunto.tamanhoDefault;

        if (maos == null)
        {
            Debug.LogWarning($"[Cursor] Conjunto de mãos nulo — cor {cor}, tamanho {(tamanhoGrande ? "grande" : "default")}");
            return;
        }

        Texture2D textura;
        if (estaArrastando)       textura = maos.fechado;
        else if (sobreInterativo) textura = maos.aberto;
        else                      textura = maos.apontando;

        if (textura == null)
        {
            Debug.LogWarning($"[Cursor] Textura NULA — cor {cor}, tamanho {(tamanhoGrande ? "grande" : "default")}. Slot vazio no Inspector?");
            return;
        }

        Cursor.SetCursor(textura, hotspot, CursorMode.Auto);
    }
}

[System.Serializable]
public class ConjuntoDeMaos
{
    public Texture2D apontando;
    public Texture2D aberto;
    public Texture2D fechado;
}

[System.Serializable]
public class ConjuntoDeCor
{
    public string nomeCor; // "Normal", "Verde", "Rosa"
    public ConjuntoDeMaos tamanhoDefault; // as 3 mãos no tamanho normal
    public ConjuntoDeMaos tamanhoGrande;  // as 3 mãos no tamanho grande
}
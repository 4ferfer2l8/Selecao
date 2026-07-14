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
        instance = this;
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
        corAtual = indiceCor;
        AtualizarCursor();
        Debug.Log($"Realce do cursor: índice {indiceCor}");
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
        if (cores == null || cores.Length == 0) return;

        int cor = Mathf.Clamp(corAtual, 0, cores.Length - 1);
        ConjuntoDeCor conjunto = cores[cor];

        // escolhe o conjunto de mãos do tamanho certo
        ConjuntoDeMaos maos = tamanhoGrande
            ? conjunto.tamanhoGrande
            : conjunto.tamanhoDefault;

        if (maos == null) return;

        // dentro do tamanho, escolhe a mão pelo estado
        // prioridade: arrastando > interativo > padrão
        Texture2D textura;
        if (estaArrastando)
            textura = maos.fechado;
        else if (sobreInterativo)
            textura = maos.aberto;
        else
            textura = maos.apontando;

        if (textura != null)
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
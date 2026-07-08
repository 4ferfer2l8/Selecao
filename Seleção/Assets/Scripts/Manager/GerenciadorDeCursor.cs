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

    // cor escolhida pelo jogador (vem da config)
    private int corAtual = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        AtualizarCursor();
    }

    // ─── API de interação (chamada pelos objetos) ───

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

    // ─── API de cor (chamada pela config de acessibilidade) ───

    public void DefinirCor(int indiceCor)
    {
        corAtual = indiceCor;
        AtualizarCursor();
        Debug.Log($"Realce do cursor: índice {indiceCor}");
    }

    // ─── Lógica interna ───

    private void AtualizarCursor()
    {
        if (cores == null || cores.Length == 0) return;

        // garante que a cor escolhida existe
        int cor = Mathf.Clamp(corAtual, 0, cores.Length - 1);
        ConjuntoDeCor conjunto = cores[cor];

        // escolhe a mão conforme o estado — prioridade: arrastando > interativo > padrão
        Texture2D textura;
        if (estaArrastando)
            textura = conjunto.fechado;
        else if (sobreInterativo)
            textura = conjunto.aberto;
        else
            textura = conjunto.apontando;

        if (textura != null)
            Cursor.SetCursor(textura, hotspot, CursorMode.Auto);
    }
}
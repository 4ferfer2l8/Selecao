using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager Instance { get; private set; }

    public GameSettingsData Dados { get; private set; } = new GameSettingsData();

    // resoluções disponíveis no dispositivo
    private Resolution[] resolucoesDisponiveis;

    // chaves do PlayerPrefs
    private const string CHAVE_VSYNC       = "vsync";
    private const string CHAVE_FULLSCREEN  = "fullScreen";
    private const string CHAVE_RESOLUCAO   = "indiceResolucao";

    [Header("Tamanho da Interface")]
    [SerializeField] private CanvasScaler canvasScaler;
    [SerializeField] private float[] escalasUI = { 0.8f, 1f, 1.2f };

    [Header("Cursor")]
    [SerializeField] private ConjuntoCursor[] cursores; // um por cor
    [SerializeField] private Vector2 hotspotCursor = Vector2.zero;

    private const string CHAVE_TAM_UI      = "indiceTamanhoUI";
    private const string CHAVE_TAM_CURSOR  = "indiceTamanhoCursor";
    private const string CHAVE_COR_CURSOR  = "indiceCorCursor";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        resolucoesDisponiveis = Screen.resolutions;
        CarregarConfiguracoes();
    }

    // ─── CARREGAR / SALVAR ───

    public void CarregarConfiguracoes()
    {
        Dados.vsync          = PlayerPrefs.GetInt(CHAVE_VSYNC, 1) == 1;
        Dados.fullScreen     = PlayerPrefs.GetInt(CHAVE_FULLSCREEN, 1) == 1;
        Dados.indiceResolucao = PlayerPrefs.GetInt(CHAVE_RESOLUCAO, resolucoesDisponiveis.Length - 1);

        AplicarTodasConfiguracoes();
    }

    public void SalvarConfiguracoes()
    {
        PlayerPrefs.SetInt(CHAVE_VSYNC,      Dados.vsync ? 1 : 0);
        PlayerPrefs.SetInt(CHAVE_FULLSCREEN, Dados.fullScreen ? 1 : 0);
        PlayerPrefs.SetInt(CHAVE_RESOLUCAO,  Dados.indiceResolucao);
        PlayerPrefs.Save();
        Debug.Log("Configurações salvas.");
    }

    public void AplicarTodasConfiguracoes()
    {
        AplicarVsync(Dados.vsync);
        AplicarFullScreen(Dados.fullScreen);
        AplicarResolucao(Dados.indiceResolucao);
    }

    // ─── MÉTODOS DE APLICAR ───

    public void AplicarVsync(bool ativo)
    {
        Dados.vsync = ativo;
        QualitySettings.vSyncCount = ativo ? 1 : 0;
        Debug.Log($"Vsync: {ativo}");
    }

    public void AplicarFullScreen(bool ativo)
    {
        Dados.fullScreen = ativo;
        Screen.fullScreen = ativo;
        Debug.Log($"Full Screen: {ativo}");
    }

    public void AplicarResolucao(int indice)
    {
        if (resolucoesDisponiveis == null || resolucoesDisponiveis.Length == 0) return;
        if (indice < 0 || indice >= resolucoesDisponiveis.Length) return;

        Dados.indiceResolucao = indice;
        Resolution r = resolucoesDisponiveis[indice];
        Screen.SetResolution(r.width, r.height, Dados.fullScreen);
        Debug.Log($"Resolução: {r.width}x{r.height}");
    }

    // getter pra UI montar o dropdown de resoluções
    public Resolution[] GetResolucoesDisponiveis() => resolucoesDisponiveis;

    public void AplicarTamanhoUI(int indice)
    {
        if (canvasScaler == null) return;
        if (indice < 0 || indice >= escalasUI.Length) return;

        Dados.indiceTamanhoUI = indice;

        canvasScaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize;
        canvasScaler.scaleFactor = escalasUI[indice];

        Debug.Log($"Tamanho da UI: {escalasUI[indice]}");
    }

    public void AplicarCorCursor(int indice)
    {
        Debug.Log($">>> AplicarCorCursor RECEBEU o índice: {indice}");
        Dados.indiceCorCursor = indice;

        if (GerenciadorDeCursor.instance != null)
            GerenciadorDeCursor.instance.DefinirCor(indice);
    }

    public void AplicarTamanhoCursor(int indice)
    {
        Dados.indiceTamanhoCursor = indice;
        AtualizarCursor();
        Debug.Log($"Tamanho do cursor: índice {indice}");
    }

    /// <summary>
    /// Combina cor + tamanho pra achar a textura certa e aplica.
    /// </summary>
    private void AtualizarCursor()
    {
        if (cursores == null || cursores.Length == 0) return;

        int cor = Mathf.Clamp(Dados.indiceCorCursor, 0, cursores.Length - 1);
        ConjuntoCursor conjunto = cursores[cor];

        if (conjunto.tamanhos == null || conjunto.tamanhos.Length == 0) return;

        int tam = Mathf.Clamp(Dados.indiceTamanhoCursor, 0, conjunto.tamanhos.Length - 1);
        Texture2D textura = conjunto.tamanhos[tam];

        if (textura != null)
            Cursor.SetCursor(textura, hotspotCursor, CursorMode.Auto);
    }
}

[System.Serializable]
public class ConjuntoCursor
{
    public string nomeCor; // só pra identificar no Inspector (ex: "Normal", "Verde", "Rosa")
    public Texture2D[] tamanhos; // 0=pequeno, 1=normal, 2=grande
}

[System.Serializable]
public class ConjuntoDeCor
{
    public string nomeCor; // "Normal", "Verde", "Rosa" — só pra identificar no Inspector
    public Texture2D apontando;
    public Texture2D aberto;
    public Texture2D fechado;
}
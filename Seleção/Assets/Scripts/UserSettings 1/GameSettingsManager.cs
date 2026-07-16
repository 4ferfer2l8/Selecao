using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("Brilho")]
    [SerializeField] private UnityEngine.UI.Image overlayBrilho;

    private const string CHAVE_TAM_UI      = "indiceTamanhoUI";
    private const string CHAVE_TAM_CURSOR  = "indiceTamanhoCursor";
    private const string CHAVE_COR_CURSOR  = "indiceCorCursor";
    private const string CHAVE_BRILHO = "brilho";

    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        MontarListaDeResolucoes();
        EncontrarCanvas();
        CarregarConfiguracoes();
    }

    // ─── CARREGAR / SALVAR ───

    public void CarregarConfiguracoes()
    {
        Dados.vsync           = PlayerPrefs.GetInt(CHAVE_VSYNC, 1) == 1;
        Dados.fullScreen      = PlayerPrefs.GetInt(CHAVE_FULLSCREEN, 1) == 1;
        Dados.indiceResolucao = PlayerPrefs.GetInt(CHAVE_RESOLUCAO, resolucoesDisponiveis.Length - 1);
        Dados.brilho          = PlayerPrefs.GetFloat(CHAVE_BRILHO, 1f);

        // as que faltavam:
        Dados.indiceTamanhoUI     = PlayerPrefs.GetInt(CHAVE_TAM_UI, 1);
        Dados.indiceCorCursor     = PlayerPrefs.GetInt(CHAVE_COR_CURSOR, 0);
        Dados.indiceTamanhoCursor = PlayerPrefs.GetInt(CHAVE_TAM_CURSOR, 0);

        AplicarTodasConfiguracoes();
    }

    public void AplicarTodasConfiguracoes()
    {
        AplicarVsync(Dados.vsync);
        AplicarFullScreen(Dados.fullScreen);
        AplicarResolucao(Dados.indiceResolucao);
        AplicarBrilho(Dados.brilho);

        // as que faltavam:
        AplicarTamanhoUI(Dados.indiceTamanhoUI);
        AplicarCorCursor(Dados.indiceCorCursor);
        AplicarTamanhoCursor(Dados.indiceTamanhoCursor);
    }

    // ─── MÉTODOS DE APLICAR ───

    public void AplicarVsync(bool ativo)
    {
        Dados.vsync = ativo;
        QualitySettings.vSyncCount = ativo ? 1 : 0;
        Debug.Log($"Vsync: {ativo}");
        PlayerPrefs.SetInt(CHAVE_VSYNC, ativo ? 1 : 0);
    }

    public void AplicarFullScreen(bool ativo)
    {
        Dados.fullScreen = ativo;
        Screen.fullScreen = ativo;
        Debug.Log($"Full Screen: {ativo}");
        PlayerPrefs.SetInt(CHAVE_VSYNC, ativo ? 1 : 0);
    }

    public void AplicarResolucao(int indice)
    {
        if (resolucoesDisponiveis == null || resolucoesDisponiveis.Length == 0) return;
        if (indice < 0 || indice >= resolucoesDisponiveis.Length) return;

        Dados.indiceResolucao = indice;
        Resolution r = resolucoesDisponiveis[indice];
        Screen.SetResolution(r.width, r.height, Dados.fullScreen);
        Debug.Log($"Resolução: {r.width}x{r.height}");
        PlayerPrefs.SetInt(CHAVE_RESOLUCAO, indice);

    }

    // getter pra UI montar o dropdown de resoluções
    public Resolution[] GetResolucoesDisponiveis() => resolucoesDisponiveis;

    private void MontarListaDeResolucoes()
    {
        Resolution[] todas = Screen.resolutions;
        List<Resolution> unicas = new List<Resolution>();

        foreach (Resolution r in todas)
        {
            int existente = unicas.FindIndex(x => x.width == r.width && x.height == r.height);

            if (existente == -1)
            {
                unicas.Add(r);
            }
            else if (r.refreshRateRatio.value > unicas[existente].refreshRateRatio.value)
            {
                // mesma resolução, mas com taxa melhor — substitui
                unicas[existente] = r;
            }
        }

        resolucoesDisponiveis = unicas.ToArray();
        Debug.Log($"Resoluções: {todas.Length} brutas → {unicas.Count} únicas");
    }

    public void AplicarTamanhoUI(int indice)
    {
        if (indice < 0 || indice >= escalasUI.Length) return;

        Dados.indiceTamanhoUI = indice;
        PlayerPrefs.SetInt(CHAVE_TAM_UI, indice);
        Debug.Log($"[Config] Tamanho da UI: índice {indice} → escala {escalasUI[indice]}");

        if (canvasScaler == null)
        {
            Debug.LogWarning("[Config] canvasScaler NULO — nada pra escalar nesta cena!");
            return;
        }

        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
        canvasScaler.scaleFactor = escalasUI[indice];
    }

    public void AplicarCorCursor(int indice)
    {
        Dados.indiceCorCursor = indice;
        PlayerPrefs.SetInt(CHAVE_COR_CURSOR, indice);
        Debug.Log($"[Config] Cor do cursor: índice {indice}");

        if (GerenciadorDeCursor.instance == null)
        {
            Debug.LogWarning("[Config] GerenciadorDeCursor NULO nesta cena!");
            return;
        }
        GerenciadorDeCursor.instance.DefinirCor(indice);
    }

    public void AplicarTamanhoCursor(int indice)
    {
        Dados.indiceTamanhoCursor = indice;
        PlayerPrefs.SetInt(CHAVE_TAM_CURSOR, indice);
        Debug.Log($"[Config] Tamanho do cursor: índice {indice}");

        if (GerenciadorDeCursor.instance == null)
        {
            Debug.LogWarning("[Config] GerenciadorDeCursor NULO nesta cena!");
            return;
        }
        GerenciadorDeCursor.instance.DefinirTamanho(indice == 1);
    }

    public void AplicarBrilho(float valor)
    {
        Dados.brilho = valor;

        if (overlayBrilho != null)
        {
            // valor 1 = transparente (tela normal), valor baixo = mais escuro
            // invertendo: quanto menor o brilho, maior o alpha do preto
            float alpha = 1f - valor;
            Color c = overlayBrilho.color;
            c.a = Mathf.Clamp01(alpha);
            overlayBrilho.color = c;
        }

        Debug.Log($"Brilho: {valor}");
        PlayerPrefs.SetFloat(CHAVE_BRILHO, valor);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += AoCarregarCena;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    private void AoCarregarCena(Scene cena, LoadSceneMode modo)
    {
        EncontrarCanvas();
        AplicarTodasConfiguracoes();

        Debug.Log($"[Config] Reaplicadas em {cena.name} | Canvas: " +
                (canvasScaler != null ? canvasScaler.name : "NENHUM"));
    }

    private void EncontrarCanvas()
    {
        CanvasPrincipal marcador = FindFirstObjectByType<CanvasPrincipal>();

        canvasScaler = marcador != null
            ? marcador.GetComponent<CanvasScaler>()
            : null;

        if (canvasScaler == null)
            Debug.LogWarning("[Config] Nenhum canvas marcado com CanvasPrincipal nesta cena!");
    }
}

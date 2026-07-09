using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Controla a UI do menu de configurações.
/// Só lida com a interface — a lógica fica no GameSettingsManager.
/// </summary>
public class SettingsUI : MonoBehaviour
{
    [Header("Vídeo")]
    public TMP_Dropdown dropdownResolucao;
    public Toggle toggleVsync;
    public Toggle toggleFullScreen;

    [Header("Acessibilidade")]
    public TMP_Dropdown dropdownTamanhoUI;
    public TMP_Dropdown dropdownCorCursor;

    private GameSettingsManager manager;

    private void Start()
    {
        manager = GameSettingsManager.Instance;

        if (manager == null)
        {
            Debug.LogError("GameSettingsManager não encontrado na cena!");
            return;
        }

        PopularDropdownResolucoes();
        CarregarValoresNaUI();
        RegistrarListeners();
    }

    private void PopularDropdownResolucoes()
    {
        dropdownResolucao.ClearOptions();

        Resolution[] resolucoes = manager.GetResolucoesDisponiveis();
        var opcoes = new List<string>();

        foreach (Resolution r in resolucoes)
            opcoes.Add($"{r.width} x {r.height}");

        dropdownResolucao.AddOptions(opcoes);
    }

    private void CarregarValoresNaUI()
    {
        var dados = manager.Dados;

        // vídeo
        toggleVsync.isOn        = dados.vsync;
        toggleFullScreen.isOn   = dados.fullScreen;
        dropdownResolucao.value = dados.indiceResolucao;
        dropdownResolucao.RefreshShownValue();

        // acessibilidade
        dropdownTamanhoUI.value = dados.indiceTamanhoUI;
        dropdownTamanhoUI.RefreshShownValue();

        dropdownCorCursor.value = dados.indiceCorCursor;
        dropdownCorCursor.RefreshShownValue();
    }

    private void RegistrarListeners()
    {
        // vídeo
        toggleVsync.onValueChanged.AddListener(manager.AplicarVsync);
        toggleFullScreen.onValueChanged.AddListener(manager.AplicarFullScreen);
        dropdownResolucao.onValueChanged.AddListener(manager.AplicarResolucao);

        // acessibilidade
        dropdownTamanhoUI.onValueChanged.AddListener(manager.AplicarTamanhoUI);
        dropdownCorCursor.onValueChanged.AddListener(manager.AplicarCorCursor);
    }
}
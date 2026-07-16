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
    public TMP_Dropdown dropdownTamanhoCursor;

    private GameSettingsManager manager;

    [Header("Brilho")]
    public Slider sliderBrilho;

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

        if (toggleVsync != null)        toggleVsync.isOn = dados.vsync;
        if (toggleFullScreen != null)   toggleFullScreen.isOn = dados.fullScreen;

        if (dropdownResolucao != null)
        {
            dropdownResolucao.value = dados.indiceResolucao;
            dropdownResolucao.RefreshShownValue();
        }
        if (dropdownTamanhoUI != null)
        {
            dropdownTamanhoUI.value = dados.indiceTamanhoUI;
            dropdownTamanhoUI.RefreshShownValue();
        }
        if (dropdownCorCursor != null)
        {
            dropdownCorCursor.value = dados.indiceCorCursor;
            dropdownCorCursor.RefreshShownValue();
        }
        if (dropdownTamanhoCursor != null)
        {
            dropdownTamanhoCursor.value = dados.indiceTamanhoCursor;
            dropdownTamanhoCursor.RefreshShownValue();
        }
        if (sliderBrilho != null) sliderBrilho.value = dados.brilho;
    }

    private void RegistrarListeners()
    {
        if (toggleVsync != null)      toggleVsync.onValueChanged.AddListener(manager.AplicarVsync);
        if (toggleFullScreen != null) toggleFullScreen.onValueChanged.AddListener(manager.AplicarFullScreen);
        if (dropdownResolucao != null)     dropdownResolucao.onValueChanged.AddListener(manager.AplicarResolucao);
        if (dropdownTamanhoUI != null)     dropdownTamanhoUI.onValueChanged.AddListener(manager.AplicarTamanhoUI);
        if (dropdownCorCursor != null)     dropdownCorCursor.onValueChanged.AddListener(manager.AplicarCorCursor);
        if (dropdownTamanhoCursor != null) dropdownTamanhoCursor.onValueChanged.AddListener(manager.AplicarTamanhoCursor);
        if (sliderBrilho != null)          sliderBrilho.onValueChanged.AddListener(manager.AplicarBrilho);
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public static class GamepadBindings {

    // Mapa padrão: ação lógica -> nome do botão no Gamepad
    private static readonly Dictionary<AcaoGamepad, string> padrao = new Dictionary<AcaoGamepad, string> {
        { AcaoGamepad.NovoNPC,      "leftShoulder" },
        { AcaoGamepad.AvancarFoco,  "rightShoulder" },
        { AcaoGamepad.VoltarFoco,   "rightTrigger" },
        { AcaoGamepad.Confirmar,    "buttonEast" },
        { AcaoGamepad.VolumeCima,   "dpad/right" },
        { AcaoGamepad.VolumeBaixo,  "dpad/left" },
    };

    // Mapa atual (pode ter sido remapeado pelo player)
    private static Dictionary<AcaoGamepad, string> atual;

    private const string PREF_PREFIX = "gamepadBind_";

    // Lista de todos os botões que aceitamos remapear
    private static readonly string[] botoesRemapaveis = {
        "leftShoulder", "rightShoulder", "leftTrigger", "rightTrigger",
        "buttonNorth", "buttonSouth", "buttonEast", "buttonWest",
        "dpad/up", "dpad/down", "dpad/left", "dpad/right",
        "leftStickButton", "rightStickButton", "start", "select"
    };

    private static void GarantirCarregado() {
        if (atual != null) return;

        atual = new Dictionary<AcaoGamepad, string>();
        foreach (var par in padrao)
        {
            string chave = PREF_PREFIX + par.Key;
            string valorSalvo = PlayerPrefs.GetString(chave, par.Value);
            atual[par.Key] = valorSalvo;
        }
    }

    // Pergunta: "esse botão foi apertado agora?"
   public static bool WasPressed(AcaoGamepad acao) {
    var gp = Gamepad.current;
    if (gp == null) return false;

    GarantirCarregado();
    string nomeBotao = atual[acao];

    ButtonControl controle = gp.TryGetChildControl<ButtonControl>(nomeBotao);
    if (controle == null) return false;

    return controle.wasPressedThisFrame;
}

    // Troca o botão de uma ação (usado na tela de rebind)
    public static void Remapear(AcaoGamepad acao, string novoNomeBotao) {
        GarantirCarregado();
        atual[acao] = novoNomeBotao;
        PlayerPrefs.SetString(PREF_PREFIX + acao, novoNomeBotao);
        PlayerPrefs.Save();
    }

    // Pra mostrar na tela de opções qual botão está mapeado agora
    public static string NomeBotaoAtual(AcaoGamepad acao) {
        GarantirCarregado();
        return atual[acao];
    }

    // Restaura tudo pro padrão de fábrica
    public static void RestaurarPadrao() {
        atual = new Dictionary<AcaoGamepad, string>(padrao);
        foreach (var acao in atual.Keys)
        {
            PlayerPrefs.DeleteKey(PREF_PREFIX + acao);
        }
        PlayerPrefs.Save();
    }

    // Retorna o nome do primeiro botão apertado agora, ou null se nenhum foi
    public static string DetectarBotaoApertado() {
    var gp = Gamepad.current;
    if (gp == null) return null;

    foreach (string nomeBotao in botoesRemapaveis)
    {
        ButtonControl controle = gp.TryGetChildControl<ButtonControl>(nomeBotao);
        if (controle != null && controle.wasPressedThisFrame)
        {
            return nomeBotao;
        }
    }

    return null;
}

    // Nome amigável pra mostrar na UI (em vez do nome técnico)
    public static string NomeAmigavel(string nomeBotao) {
        switch (nomeBotao)
        {
            case "leftShoulder": return "L1";
            case "rightShoulder": return "R1";
            case "leftTrigger": return "L2";
            case "rightTrigger": return "R2";
            case "buttonNorth": return "Y/Triângulo";
            case "buttonSouth": return "A/X";
            case "buttonEast": return "B/Círculo";
            case "buttonWest": return "X/Quadrado";
            case "dpad/up": return "D-Pad Cima";
            case "dpad/down": return "D-Pad Baixo";
            case "dpad/left": return "D-Pad Esquerda";
            case "dpad/right": return "D-Pad Direita";
            case "leftStickButton": return "L3";
            case "rightStickButton": return "R3";
            case "start": return "Start";
            case "select": return "Select";
            default: return nomeBotao;
        }
    }
}
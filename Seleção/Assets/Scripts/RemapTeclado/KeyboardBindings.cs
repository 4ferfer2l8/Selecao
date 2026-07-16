using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public static class KeyboardBindings
{

    // Mapa padrão: ação lógica -> tecla no Keyboard
    private static readonly Dictionary<AcaoTeclado, Key> padrao = new Dictionary<AcaoTeclado, Key> {
        { AcaoTeclado.PausarJogo,     Key.Escape },
        { AcaoTeclado.NovoCandidato,  Key.Space },
    };

    // Mapa atual (pode ter sido remapeado pelo player)
    private static Dictionary<AcaoTeclado, Key> atual;

    private const string PREF_PREFIX = "teclaBind_";

    // Lista de todas as teclas que aceitamos remapear
    private static readonly Key[] teclasRemapaveis = {
        Key.Escape, Key.Space, Key.Enter, Key.Tab, Key.Backspace,
        Key.LeftShift, Key.RightShift, Key.LeftCtrl, Key.RightCtrl,
        Key.LeftAlt, Key.RightAlt,
        Key.A, Key.B, Key.C, Key.D, Key.E, Key.F, Key.G, Key.H, Key.I, Key.J,
        Key.K, Key.L, Key.M, Key.N, Key.O, Key.P, Key.Q, Key.R, Key.S, Key.T,
        Key.U, Key.V, Key.W, Key.X, Key.Y, Key.Z,
        Key.Digit0, Key.Digit1, Key.Digit2, Key.Digit3, Key.Digit4,
        Key.Digit5, Key.Digit6, Key.Digit7, Key.Digit8, Key.Digit9,
        Key.UpArrow, Key.DownArrow, Key.LeftArrow, Key.RightArrow,
        Key.F1, Key.F2, Key.F3, Key.F4, Key.F5
    };

    // Flag global: true enquanto a tela de rebind está esperando uma tecla.
    // Outros scripts (pause, etc.) devem checar isso e ignorar o input nesse período.
    public static bool EmRemapeamento { get; set; } = false;

    private static void GarantirCarregado()
    {
        if (atual != null) return;

        atual = new Dictionary<AcaoTeclado, Key>();
        foreach (var par in padrao)
        {
            string chave = PREF_PREFIX + par.Key;
            int valorSalvo = PlayerPrefs.GetInt(chave, (int)par.Value);
            atual[par.Key] = (Key)valorSalvo;
        }
    }

    // Pergunta: "essa tecla foi apertada agora?"
    public static bool WasPressed(AcaoTeclado acao)
    {
        // Enquanto está remapeando, nenhuma ação de gameplay deve disparar
        if (EmRemapeamento) return false;

        var kb = Keyboard.current;
        if (kb == null) return false;

        GarantirCarregado();
        Key tecla = atual[acao];

        KeyControl controle = kb[tecla];
        if (controle == null) return false;

        return controle.wasPressedThisFrame;
    }

    // Troca a tecla de uma ação (usado na tela de rebind)
    public static void Remapear(AcaoTeclado acao, Key novaTecla)
    {
        GarantirCarregado();
        atual[acao] = novaTecla;
        PlayerPrefs.SetInt(PREF_PREFIX + acao, (int)novaTecla);
        PlayerPrefs.Save();
    }

    // Pra mostrar na tela de opções qual tecla está mapeada agora
    public static Key TeclaAtual(AcaoTeclado acao)
    {
        GarantirCarregado();
        return atual[acao];
    }

    // Restaura tudo pro padrão de fábrica
    public static void RestaurarPadrao()
    {
        atual = new Dictionary<AcaoTeclado, Key>(padrao);
        foreach (var acao in atual.Keys)
        {
            PlayerPrefs.DeleteKey(PREF_PREFIX + acao);
        }
        PlayerPrefs.Save();
    }

    // Retorna a primeira tecla apertada agora, ou null se nenhuma foi
    public static Key? DetectarTeclaApertada()
    {
        var kb = Keyboard.current;
        if (kb == null) return null;

        foreach (Key tecla in teclasRemapaveis)
        {
            KeyControl controle = kb[tecla];
            if (controle != null && controle.wasPressedThisFrame)
            {
                return tecla;
            }
        }

        return null;
    }

    // Nome amigável pra mostrar na UI (em vez do nome técnico)
    public static string NomeAmigavel(Key tecla)
    {
        switch (tecla)
        {
            case Key.Escape: return "ESC";
            case Key.Space: return "Espaço";
            case Key.Enter: return "Enter";
            case Key.Tab: return "Tab";
            case Key.Backspace: return "Backspace";
            case Key.LeftShift: return "Shift Esq";
            case Key.RightShift: return "Shift Dir";
            case Key.LeftCtrl: return "Ctrl Esq";
            case Key.RightCtrl: return "Ctrl Dir";
            case Key.LeftAlt: return "Alt Esq";
            case Key.RightAlt: return "Alt Dir";
            case Key.UpArrow: return "Seta Cima";
            case Key.DownArrow: return "Seta Baixo";
            case Key.LeftArrow: return "Seta Esquerda";
            case Key.RightArrow: return "Seta Direita";
            default: return tecla.ToString();
        }
    }
}
using UnityEngine;

/// <summary>
/// Contém todos os dados de configuração do jogo.
/// </summary>
[System.Serializable]
public class GameSettingsData
{
    // Vídeo
    public bool vsync            = true;
    public bool fullScreen       = true;
    public int indiceResolucao   = 0;   // índice na lista de resoluções disponíveis

    
    // Acessibilidade
    public int indiceTamanhoUI   = 1;   // 0=pequeno, 1=normal, 2=grande
    public int indiceTamanhoCursor = 1; // 0=pequeno, 1=normal, 2=grande
    public int indiceCorCursor   = 0;   // índice na lista de cores
    
}
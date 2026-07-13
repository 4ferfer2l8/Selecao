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
    public int indiceResolucao   = 0;  
    public float brilho           = 1.0f; // 0.0 a 1.0

    
    // Acessibilidade
    public int indiceTamanhoUI   = 1;   // 0=pequeno, 1=normal, 2=grande
    public int indiceTamanhoCursor = 1; // 0=pequeno, 1=normal, 2=grande
    public int indiceCorCursor   = 0;   // índice na lista de cores
    
}
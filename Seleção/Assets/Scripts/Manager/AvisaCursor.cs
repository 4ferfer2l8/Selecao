using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Avisa o gerenciador de cursor quando o mouse entra e sai.
/// Funciona tanto pra UI quanto pra objetos com Collider2D.
/// </summary>
public class AvisaCursor : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GerenciadorDeCursor.instance != null)
            GerenciadorDeCursor.instance.EntrouEmInterativo();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GerenciadorDeCursor.instance != null)
            GerenciadorDeCursor.instance.SaiuDeInterativo();
    }
}
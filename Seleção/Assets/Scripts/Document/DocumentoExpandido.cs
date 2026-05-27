using UnityEngine;

/// <summary>
/// Adaptador entre SomDePapel (existente) e DocumentPopup (novo sistema).
/// Coloque este script no mesmo GameObject que SomDePapel — o DocumentoNaMesa.
///
/// SomDePapel chama GetComponent<DocumentoExpandido>() e lê EstaAberto.
/// Este script redireciona tudo para o DocumentPopup real.
/// </summary>
public class DocumentoExpandido : MonoBehaviour
{
    [Header("Referência ao popup real")]
    [SerializeField] private DocumentPopup documentPopup;

    /// <summary>
    /// Propriedade lida pelo SomDePapel para decidir qual som tocar.
    /// Retorna o estado real do DocumentPopup.
    /// </summary>
    public bool EstaAberto => documentPopup != null && documentPopup.IsOpen;
}
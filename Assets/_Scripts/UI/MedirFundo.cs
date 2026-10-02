using UnityEngine;

// Script TEMPORARIO so para descobrir a altura exata do sprite em unidades do mundo.
// Anexe num dos fundos, de Play, olhe o Console, depois REMOVA este script.
public class MedirFundo : MonoBehaviour
{
    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Debug.Log($"Largura do fundo: {sr.bounds.size.x} | Altura do fundo: {sr.bounds.size.y}");
        }
    }
}

using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject prefabProjetil;
    [SerializeField] private Transform pontoDeDisparo;

    [Header("Configurações")]
    [SerializeField] private float cadenciaDeTiro = 0.3f;
    [SerializeField] private KeyCode teclaDeTiro = KeyCode.Space;

    private bool tiroDuploAtivo = false;
    [SerializeField] private float offsetTiroDuplo = 0.3f;

    private float proximoTiroPermitido = 0f;

    void Update()
    {
        if (Input.GetKey(teclaDeTiro) && Time.time >= proximoTiroPermitido)
        {
            Atirar();
            proximoTiroPermitido = Time.time + cadenciaDeTiro;
        }
    }

    private void Atirar()
    {
        if (prefabProjetil == null)
        {
            Debug.LogWarning("PlayerShooting: Prefab do projétil não foi atribuído no Inspector!");
            return;
        }

        Vector3 posicaoDisparo = pontoDeDisparo != null ? pontoDeDisparo.position : transform.position;

        if (!tiroDuploAtivo)
        {
            Instantiate(prefabProjetil, posicaoDisparo, Quaternion.identity);
        }
        else
        {
            Vector3 posEsquerda = posicaoDisparo + Vector3.left * offsetTiroDuplo;
            Vector3 posDireita = posicaoDisparo + Vector3.right * offsetTiroDuplo;
            Instantiate(prefabProjetil, posEsquerda, Quaternion.identity);
            Instantiate(prefabProjetil, posDireita, Quaternion.identity);
        }

        // Toca o som de disparo (laser)
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.sfxLaser);
        }
    }

    public void AtivarTiroDuplo(float duracao)
    {
        tiroDuploAtivo = true;
        CancelInvoke(nameof(DesativarTiroDuplo));
        Invoke(nameof(DesativarTiroDuplo), duracao);
    }

    private void DesativarTiroDuplo()
    {
        tiroDuploAtivo = false;
    }
}

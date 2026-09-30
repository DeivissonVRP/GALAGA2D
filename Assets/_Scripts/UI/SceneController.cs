using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Chame esta função no botão "Iniciar Jogo" do Menu
    // Agora leva ao video de explicacao antes do gameplay
    public void LoadGameplay()
    {
        SceneManager.LoadScene("VideoExplicacao");
    }

    // Chame esta função no botão "Menu Principal"
     public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // Chame esta função no botão "Sair"
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Jogo fechado!");
    }
}

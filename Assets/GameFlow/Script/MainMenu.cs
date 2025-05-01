using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator logo;

    public void OnClickStartGame()
    {
        Animator animator = logo.GetComponent<Animator>();
        animator.SetTrigger("Start");
        
        SceneManager.LoadScene("Town");
    }

    public void OnClickExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
# else
        Application.Quit();
#endif
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{ 
    public void PlayGame()
    {

        //SceneManager.LoadScene("GameScene");
    }


    public void OpenOptions()
    {

        Debug.Log("Options menu opened.");

    }

    public void QuitGame()
    {
        Debug.Log("Quit game.");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

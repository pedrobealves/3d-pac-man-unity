using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.instance.OnGameOver += GameOver;
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode (will only be executed in the editor)
#endif
    }
}

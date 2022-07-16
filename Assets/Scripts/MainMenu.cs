using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Text bestScore;
    [SerializeField] private InputField name;

    // Start is called before the first frame update
    void Start()
    {
        if(GameInfo.Instance != null)
        {
            UpdateInfo();
        }
    }

    public void SetNewPlayer()
    {
        GameInfo.Instance.Data.ResetCurrentPlayer(name.text);
        UpdateInfo();
    }

    void UpdateInfo()
    {
        bestScore.text = "Best Score: " + GameInfo.Instance.Data.BestName + " " + GameInfo.Instance.Data.BestScore;
        name.text = GameInfo.Instance.Data.BestName.Length == 0 ? "Enter Name .." : GameInfo.Instance.Data.BestName;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

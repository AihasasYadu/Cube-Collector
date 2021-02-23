using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    private Button startButton;
    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(LoadGame);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

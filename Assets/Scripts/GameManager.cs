using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingletonGeneric<GameManager>
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private RectTransform gameOverPanel;
    [SerializeField] private Transform minX;
    [SerializeField] private Transform maxX;
    [SerializeField] private Transform minZ;
    [SerializeField] private Transform maxZ;

    private int secondsLeft;
    void Start()
    { 
        secondsLeft = 60;
        StartCoroutine(UpdateTimer());
        EventManager.GameOver += DisplayGameOverPanel;
    }

    private IEnumerator UpdateTimer()
    {
        yield return null;
        while (secondsLeft != 0)
        {
            yield return new WaitForSeconds(1);
            secondsLeft--;
            timerText.SetText(secondsLeft.ToString());
        }
        EventManager.Instance.GameOverEvent();
    }

    private void DisplayGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
        StartCoroutine(LoadIntroScene());
    }

    private IEnumerator LoadIntroScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        Destroy(gameObject);
    }

    public Transform GetRandomCoordinates()
    {
        float x1 = minX.position.x;
        float x2 = maxX.position.x;
        float z1 = minZ.position.z;
        float z2 = maxZ.position.z;
        Transform temp = gameObject.transform;
        temp.position = new Vector3(UnityEngine.Random.Range(x1, x2), 0, UnityEngine.Random.Range(z1, z2));
        return temp;
    }

    private void OnDestroy()
    {
        EventManager.GameOver -= DisplayGameOverPanel;
    }
}

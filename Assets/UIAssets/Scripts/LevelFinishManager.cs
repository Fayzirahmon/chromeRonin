using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class LevelFinishManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject finishPanel;      
    public TextMeshProUGUI timeText;    
    public TextMeshProUGUI deathText;   
    public TextMeshProUGUI gradeText;   

    [Header("Grading Criteria (For Rank A)")]
    public float targetTime = 60f;
    public int maxDeaths = 3;

    void Start()
    {
        finishPanel.SetActive(false);
    }
    
    public void LevelComplete(float finalTime, int deathCount)
    {
        finishPanel.SetActive(true);
        Time.timeScale = 0f;

        timeText.text = "TIME: " + finalTime.ToString("F2") + "s";
        deathText.text = "DEATHS: " + deathCount;

        string rank = "C";
        
        if (finalTime <= targetTime && deathCount == 0) 
            rank = "S";
        else if (finalTime <= targetTime && deathCount <= maxDeaths) 
            rank = "A";
        else if (finalTime <= targetTime * 1.5f) 
            rank = "B";

        gradeText.text = "RANK: " + rank;
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public TextMeshProUGUI txtScore;
    public TMP_Text scoreText;

    void Start()
    {

        // GameDataManager에서 점수 불러오기
        if (GameDataManager.instance != null)
        {
            int score = GameDataManager.instance.lastScore;
            txtScore.text = score.ToString();
        }
        else
        {
            txtScore.text = "0";
        }
        scoreText.text = GameManager.FinalScore.ToString();
    }
}

using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    public int lastScore;

    void Awake()
    {
        // 씬이 바뀌어도 파괴되지 않게 유지
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 점수 저장 메서드
    public void SaveScore(int score)
    {
        lastScore = score;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartSceneChange()
    {
        SceneManager.LoadScene("Lobby");
        Time.timeScale = 1f;
    }

    public void LobbySceneChange()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ResultSceneChanger()
    {
        SceneManager.LoadScene("fall");
    }
    // 게임을 종료하는 함수
    public void QuitGame()
    {
        // 에디터에서 실행 중일 때는 플레이 모드를 중지
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 빌드된 게임에서는 애플리케이션 종료
            Application.Quit();
#endif

        // 디버그 로그 출력 (선택사항)
        Debug.Log("게임이 종료됩니다.");
    }

    public void ToMap()
    {
        SceneManager.LoadScene("Map");
    }

    public void GoGame()
    {
        SceneManager.LoadScene("JL-shooting");
        Time.timeScale = 1f;
    }

    public void InfoSceneChanger()
    {
        SceneManager.LoadScene(6);
    }

    public void InfoNextChanger()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        SceneManager.LoadScene(nextIndex);
    }

    public void InfoBackChanger()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int previousIndex = currentIndex - 1;

        SceneManager.LoadScene(previousIndex);
    }
}

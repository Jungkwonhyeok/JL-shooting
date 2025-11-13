
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameManager gameManager;
    private void Start()
    {
        // 이미 BGM이 재생 중인지 확인
        bool isBgmPlaying = false;

        if (AudioManager.instance != null)
        {
            foreach (var player in AudioManager.instance.GetComponentsInChildren<AudioSource>())
            {
                if (player.isPlaying)
                {
                    isBgmPlaying = true;
                    break;
                }
            }

            // 만약 아무 BGM도 안 나오고 있다면 로비 BGM 재생
            if (!isBgmPlaying)
            {
                AudioManager.instance.PlayBgm(AudioManager.Bgm.Lobby);
            }
        }
    }
    public void StartSceneChange()
    {
        ButtonAudio();
        SceneManager.LoadScene("Lobby");
        Time.timeScale = 1f;
    }

    public void LobbySceneChange()
    {
        ButtonAudio();
        SceneManager.LoadScene("StartScene");
    }

    public void ResultSceneChanger()
    {
        AudioManager.instance.PlayBgm(AudioManager.Bgm.Lobby);
        ButtonAudio();
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
        ButtonAudio();
        SceneManager.LoadScene("Map");
    }

    public void GoGame()
    {
        AudioManager.instance.PlayBgm(AudioManager.Bgm.Game);
        ButtonAudio();
        SceneManager.LoadScene("JL-shooting");
        Time.timeScale = 1f;
    }

    public void InfoSceneChanger()
    {
        ButtonAudio();
        SceneManager.LoadScene(6);
    }

    public void InfoNextChanger()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        ButtonAudio();
        SceneManager.LoadScene(nextIndex);
    }

    public void InfoBackChanger()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int previousIndex = currentIndex - 1;

        ButtonAudio();
        SceneManager.LoadScene(previousIndex);
    }

    public void BackMap()
    {
        AudioManager.instance.PlayBgm(AudioManager.Bgm.Lobby);
        ButtonAudio();
        SceneManager.LoadScene("Map");
    }

    void ButtonAudio()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
    }
}

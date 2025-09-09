
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
    }

    public void LobbySceneChange()
    {
        SceneManager.LoadScene("StartScene");
    }

    // ������ �����ϴ� �Լ�
    public void QuitGame()
    {
        // �����Ϳ��� ���� ���� ���� �÷��� ��带 ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // ����� ���ӿ����� ���ø����̼� ����
            Application.Quit();
#endif

        // ����� �α� ��� (���û���)
        Debug.Log("������ ����˴ϴ�.");
    }

    public void ToMap()
    {
        SceneManager.LoadScene("Map");
    }

    public void GoGame()
    {
        SceneManager.LoadScene("JL-shooting");
    }
}

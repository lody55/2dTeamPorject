using UnityEngine;
using UnityEngine.SceneManagement;

//������ �������Ʈ�� �÷������� ��
public class AutoSceneSaver : MonoBehaviour
{
    private void Start()
    {
        // ���� ���� ���θ޴��� �������� �ƴ� ��쿡�� ����
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene != "MainMenu" && currentScene != "SettingsScene")
        {
            SaveCurrentScene();
        }
    }

    private void SaveCurrentScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastScene", currentScene);
        PlayerPrefs.SetInt("GameSaved", 1);
        PlayerPrefs.Save();

        Debug.Log($"���� �� �����: {currentScene}");
    }

    // ���ø����̼� ����ÿ��� ����
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveCurrentScene();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveCurrentScene();
        }
    }
}
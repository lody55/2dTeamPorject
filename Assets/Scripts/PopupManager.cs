using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPanel;   //�˾� �г�

    public Button gameStartBtn; //���� ���� ��ư
    public Button yesBtn; //�� ��ư
    public Button noBtn; //�ƴϿ� ��ư

    private void Start()
    {
        //ó���� �˾� �����
        popupPanel.SetActive(false);
    }
    


    public void OnClickYes()
    {
        //���� ���嵥���� ���� �� �� ���� ����
        DeleteSaveData();
        StartNewGame();
    }
    public void OnClickNo()
    {
        //�˾� �ݱ�
        popupPanel.SetActive(false);
    }

    

    
    private void DeleteSaveData()
    {
        //playerPrefs�� ����� ������ ����(�ʿ��� ��츸 ����)
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    private void StartNewGame()
    {
        //�� ��ư : ù ������ �̵�
        SceneManager.LoadScene("Mainmenu");
    }
}



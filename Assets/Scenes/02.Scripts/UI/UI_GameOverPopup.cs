using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOverPopup : MonoBehaviour
{
    public void Open()
    {
        // ���� ȿ�����̶����
        // �ʱ�ȭ �Լ�
        gameObject.SetActive(true);
    }

    public void Close()
    {
        // ���� ȿ�����̶����...
        // ���� ����
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OnClickResumeButton()
    {
        //���Ŵ�����.(���� ���� �ִ� ��)�ε��ض�
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Debug.Log("�ٽ��ϱ�");
    }

    public void OnClickExitButton()
    {
        Debug.Log("��������");
        //���� �� �������� ��� �����ϴ� ���
        Application.Quit();
#if UNITY_EDITOR
        //����Ƽ �����Ϳ��� �������� ��� �����ϴ� ���
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}

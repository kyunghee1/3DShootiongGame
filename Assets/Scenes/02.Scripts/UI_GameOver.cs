using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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

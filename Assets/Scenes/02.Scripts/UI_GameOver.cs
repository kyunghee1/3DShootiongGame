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
        //씬매니저야.(현재 열려 있는 씬)로드해라
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Debug.Log("다시하기");
    }

    public void OnClickExitButton()
    {
        Debug.Log("게임종료");
        //빌드 후 실행했을 경우 종료하는 방법
        Application.Quit();
#if UNITY_EDITOR
        //유니티 에디터에서 실행했을 경우 종료하는 방법
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}

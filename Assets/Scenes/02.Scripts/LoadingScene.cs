using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//역할 : 다음 씬을 비동기 방식으로 로드한다.
// 그리고 로딩 진행률을 실시간으로 표현한다.
public class LoadingScene : MonoBehaviour
{
    public SceneNames NextScene;
    public Slider LoadingSliderUI;
    public TextMesh LoadingTextUI;
    // Start is called before the first frame update
    void Start()
    {
        
     StartCoroutine(LoadNextScene_Coroutine()); 
    }
    public  IEnumerator LoadNextScene_Coroutine()
    {
        //지정한 씬을 "비동기" 방식으로 로드한다.
        AsyncOperation ao =SceneManager.LoadSceneAsync((int)NextScene); // 20초가 걸린다고 가정_동기방식

        //로드되는 씬의 모습이 화면에 보이지 않게 한다.
        ao.allowSceneActivation = false;

        //로딩이 완료될 때까지... 반복
        while(!ao.isDone)
        {
            //로딩바도 이동시키고,
            // 로딩 텍스도 갱신하고,
            LoadingSliderUI.value = ao.progress; // 0~1;
            LoadingTextUI.text = $"{ao.progress * 100f}%";

            /** 
             서버통신을 해서 데이터를 받아오기도 한다.
            - 기획 데이터
            - 밸런스 데이터
            - 번역 데이터
            - 설정 데이터
            **/

            if(ao.progress >= 0.9f)
            {
                ao.allowSceneActivation |= true;
            }
            yield return null;
        }

    }
    
  
}

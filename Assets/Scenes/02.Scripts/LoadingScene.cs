using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//���� : ���� ���� �񵿱� ������� �ε��Ѵ�.
// �׸��� �ε� ������� �ǽð����� ǥ���Ѵ�.
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
        //������ ���� "�񵿱�" ������� �ε��Ѵ�.
        AsyncOperation ao =SceneManager.LoadSceneAsync((int)NextScene); // 20�ʰ� �ɸ��ٰ� ����_������

        //�ε�Ǵ� ���� ����� ȭ�鿡 ������ �ʰ� �Ѵ�.
        ao.allowSceneActivation = false;

        //�ε��� �Ϸ�� ������... �ݺ�
        while(!ao.isDone)
        {
            //�ε��ٵ� �̵���Ű��,
            // �ε� �ؽ��� �����ϰ�,
            LoadingSliderUI.value = ao.progress; // 0~1;
            LoadingTextUI.text = $"{ao.progress * 100f}%";

            /** 
             ��������� �ؼ� �����͸� �޾ƿ��⵵ �Ѵ�.
            - ��ȹ ������
            - �뷱�� ������
            - ���� ������
            - ���� ������
            **/

            if(ao.progress >= 0.9f)
            {
                ao.allowSceneActivation |= true;
            }
            yield return null;
        }

    }
    
  
}

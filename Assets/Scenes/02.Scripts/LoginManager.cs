using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneNames
{
    Loady, //0
    Loading,//1
    Main,  //2
}
public class LoginManager : MonoBehaviour
{
    //사용자 계정을 새로 저장하거나(회원가입), 저장된 데이터를 읽어
    //사용자 입력과 일치하는자 검사(로그인)한다.

    public TMP_InputField IDInputfield; // 아이디 입력창
    public TMP_InputField PasswordInputfield; //비밀번호 입력창
    public TextMeshProUGUI NotifyTextUI; //알림 텍스트

    private void Start()
    {
        IDInputfield.text = string.Empty;
        PasswordInputfield.text = string.Empty;
        NotifyTextUI.text = string.Empty;

       
    }
    public void OnClickRegisterButton()
    {
        string id = IDInputfield.text;
        string pw = PasswordInputfield.text;
        if (id == string.Empty || pw == string.Empty)
        {
            NotifyTextUI.text = " 아이디와 비밀번호를 정확하게 입력해주세요";
            return;
        }
        //1 이미 같은 계정으로 회원가입이 되어있는 경우
        if (PlayerPrefs.HasKey(id))
        {
            NotifyTextUI.text = "이미 존재하는 계정입니다.";
        }
        //2. 회원가입에 성공하는 경우
        else
        {
            NotifyTextUI.text = "회원가입을 완료했습니다.";
            PlayerPrefs.SetString(id, pw);
        }
        IDInputfield.text = string.Empty;
        PasswordInputfield.text = string.Empty;
    }
    public void OnClickLoginButton()
    {
        //0. 아이디 또는 비밀번호 입력 X -> "아이디와 비밀번호를 입력해주세요."
        string id = IDInputfield.text;
        string pw = PasswordInputfield.text;
        if (id == string.Empty || pw == string.Empty)
        {
            NotifyTextUI.text = " 아이디와 비밀번호를 정확하게 입력해주세요";
            return;
        }

       
        if (PlayerPrefs.HasKey(id))
        { 
            //1.없는 아이디                 -> "아이디를 입력해 주세요"
            NotifyTextUI.text = " 아이디와 비밀번호를 확인해주세요";
            
            }
        else if(PlayerPrefs.GetString(id) != pw)
        {
            //2. 틀린 비밀번호               -> "비밀번호를 확인해주세요"
            NotifyTextUI.text = "아이디와 비밀번호를 확인해주세요.";
        }
      else
        {
            //3. 로그인 성공                 -> 메인 씬으로 이동
            SceneManager.LoadScene((int)SceneNames.Main);
        } 
    }     
}


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
    //����� ������ ���� �����ϰų�(ȸ������), ����� �����͸� �о�
    //����� �Է°� ��ġ�ϴ��� �˻�(�α���)�Ѵ�.

    public TMP_InputField IDInputfield; // ���̵� �Է�â
    public TMP_InputField PasswordInputfield; //��й�ȣ �Է�â
    public TextMeshProUGUI NotifyTextUI; //�˸� �ؽ�Ʈ

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
            NotifyTextUI.text = " ���̵�� ��й�ȣ�� ��Ȯ�ϰ� �Է����ּ���";
            return;
        }
        //1 �̹� ���� �������� ȸ�������� �Ǿ��ִ� ���
        if (PlayerPrefs.HasKey(id))
        {
            NotifyTextUI.text = "�̹� �����ϴ� �����Դϴ�.";
        }
        //2. ȸ�����Կ� �����ϴ� ���
        else
        {
            NotifyTextUI.text = "ȸ�������� �Ϸ��߽��ϴ�.";
            PlayerPrefs.SetString(id, pw);
        }
        IDInputfield.text = string.Empty;
        PasswordInputfield.text = string.Empty;
    }
    public void OnClickLoginButton()
    {
        //0. ���̵� �Ǵ� ��й�ȣ �Է� X -> "���̵�� ��й�ȣ�� �Է����ּ���."
        string id = IDInputfield.text;
        string pw = PasswordInputfield.text;
        if (id == string.Empty || pw == string.Empty)
        {
            NotifyTextUI.text = " ���̵�� ��й�ȣ�� ��Ȯ�ϰ� �Է����ּ���";
            return;
        }

       
        if (PlayerPrefs.HasKey(id))
        { 
            //1.���� ���̵�                 -> "���̵� �Է��� �ּ���"
            NotifyTextUI.text = " ���̵�� ��й�ȣ�� Ȯ�����ּ���";
            
            }
        else if(PlayerPrefs.GetString(id) != pw)
        {
            //2. Ʋ�� ��й�ȣ               -> "��й�ȣ�� Ȯ�����ּ���"
            NotifyTextUI.text = "���̵�� ��й�ȣ�� Ȯ�����ּ���.";
        }
      else
        {
            //3. �α��� ����                 -> ���� ������ �̵�
            SceneManager.LoadScene((int)SceneNames.Main);
        } 
    }     
}


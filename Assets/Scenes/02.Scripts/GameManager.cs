using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ����: ���� ������
// -> ���� ��ü�� ���¸� �˸���, ���۰� ���� �ؽ�Ʈ�� ��Ÿ����.
public enum GameState
{
    Ready, // �غ�
    Go, // ����
    Pause,  // �Ͻ�����
    Over,  // ����
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // ������ ���´� ó���� "�غ�" ����
    public GameState State { get; private set; } = GameState.Ready;

    public TextMeshProUGUI StateTextUI;

    public Color GoStateColor;

    public UI_OptionPopup OptionUI;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        Time.timeScale = 1f;

        StartCoroutine(Start_Coroutine());
    }

    private IEnumerator Start_Coroutine()
    {
        // ���� ����
        // 1. ���� "�غ�" ���� (Ready...)
        State = GameState.Ready;
        StateTextUI.gameObject.SetActive(true);
        Refresh();

        // 2. 1.6�� �Ŀ� ���� "����" ���� (Start!)
        yield return new WaitForSeconds(1.6f);
        State = GameState.Go;
        Refresh();

        // 3. 0.4�� �Ŀ� �ؽ�Ʈ �������...
        yield return new WaitForSeconds(0.4f);
        StateTextUI.gameObject.SetActive(false);
    }

    // 4. �÷��̸� �ϴٰ�
    // 5. �÷��̾� ü���� 0�� �Ǹ� "���� ����" ����
    public void GameOver()
    {
        State = GameState.Over;
        StateTextUI.gameObject.SetActive(true);
        Refresh();
    }

    public void Pause()
    {
        State = GameState.Pause;
        Time.timeScale = 0f;
    }
    public void Continue()
    {
        State = GameState.Go;
        Time.timeScale = 1f;
    }

    public void OnOptionButtonClicked()
    {
        Debug.Log("�ɼ� ��ư Ŭ��");

        Pause();

        OptionUI.Open();
    }


    public void Refresh()
    {
        switch (State)
        {
            case GameState.Ready:
            {
                StateTextUI.text = "Ready...";
                StateTextUI.color = new Color32(0, 253, 181, 255);
                break;
            }

            case GameState.Go:
            {
                StateTextUI.text = "Go!";
                StateTextUI.color = GoStateColor;
                break;
            }

            case GameState.Over:
            {
                StateTextUI.text = "Game Over";
                StateTextUI.color = Color.red;
                break;
            }

        }
    }
}